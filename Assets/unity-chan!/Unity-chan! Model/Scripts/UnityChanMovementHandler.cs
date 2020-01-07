using Movement.Handler;
using UnityEngine;

// ReSharper disable once CheckNamespace
// Doesn't matter how good this shit is, you didn't write it, the only thing you did is to make it compile and strip out jumping code
// You probably need to write a new one sooner or later, but for now lets stick with this crap
namespace UnityChan
{
// 必要なコンポーネントの列記
    [RequireComponent(typeof(Animator))]
    public class UnityChanMovementHandler : MovementHandler
    {
        private static readonly int RestState = Animator.StringToHash("Base Layer.Rest");
        private Animator _anim; // キャラにアタッチされるアニメーターへの参照

        public float animSpeed = 1.5f; // アニメーション再生速度設定

        // 後退速度
        public float backwardSpeed = 2.0f;

        private GameObject _cameraObject; // メインカメラへの参照
        
        private AnimatorStateInfo _currentBaseState; // base layerで使われる、アニメーターの現在の状態の参照

        // 以下キャラクターコントローラ用パラメタ
        // 前進速度
        public float forwardSpeed = 7.0f;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform transformToMove;

        // 旋回速度
        public float rotateSpeed = 2.0f;

        // キャラクターコントローラ（カプセルコライダ）の移動量
        private Vector3 _velocity;
        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Rest = Animator.StringToHash("Rest");

        // 初期化
        private void Start()
        {
            // Animatorコンポーネントを取得する
            _anim = GetComponent<Animator>();
            //メインカメラを取得する
            _cameraObject = GameObject.FindWithTag("MainCamera");
        }


        // 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
        private void FixedUpdate()
        {
            var h = inputSource.Horizontal(); // 入力デバイスの水平軸をhで定義
            var v = inputSource.Vertical(); // 入力デバイスの垂直軸をvで定義
            _anim.SetFloat(Speed, v); // Animator側で設定している"Speed"パラメタにvを渡す
            _anim.SetFloat(Direction, h); // Animator側で設定している"Direction"パラメタにhを渡す
            _anim.speed = animSpeed; // Animatorのモーション再生速度に animSpeedを設定する
            _currentBaseState = _anim.GetCurrentAnimatorStateInfo(0); // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
            rb.useGravity = true; //ジャンプ中に重力を切るので、それ以外は重力の影響を受けるようにする


            // 以下、キャラクターの移動処理
            _velocity = new Vector3(0, 0, v); // 上下のキー入力からZ軸方向の移動量を取得
            // キャラクターのローカル空間での方向に変換
            _velocity = transformToMove.TransformDirection(_velocity);
            //以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
            if (v > 0.1)
            {
                _velocity *= forwardSpeed; // 移動速度を掛ける
            }
            else if (v < -0.1)
            {
                _velocity *= backwardSpeed; // 移動速度を掛ける
            }

            // 上下のキー入力でキャラクターを移動させる
            transformToMove.localPosition += _velocity * Time.fixedDeltaTime;

            // 左右のキー入力でキャラクタをY軸で旋回させる
            transformToMove.Rotate(0, h * rotateSpeed, 0);

            if (_currentBaseState.fullPathHash == RestState)
            {
                //cameraObject.SendMessage("setCameraPositionFrontView");		// カメラを正面に切り替える
                // ステートが遷移中でない場合、Rest bool値をリセットする（ループしないようにする）
                if (!_anim.IsInTransition(0))
                {
                    _anim.SetBool(Rest, false);
                }
            }
        }
    }
}