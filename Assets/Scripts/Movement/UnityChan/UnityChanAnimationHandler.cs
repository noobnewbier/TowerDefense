using Movement.InputSource;
using UnityEngine;

namespace Movement.UnityChan
{
    [RequireComponent(typeof(Animator))]
    public class UnityChanAnimationHandler : MonoBehaviour
    {
        [SerializeField] private MovementInputSource inputSource;

        private static readonly int RestState = Animator.StringToHash("Base Layer.Rest");
        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Rest = Animator.StringToHash("Rest");
        private Animator _anim; // キャラにアタッチされるアニメーターへの参照
        private AnimatorStateInfo _currentBaseState; // base layerで使われる、アニメーターの現在の状態の参照
        public float animSpeed = 1.5f; // アニメーション再生速度設定

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            var h = inputSource.Horizontal(); // 入力デバイスの水平軸をhで定義
            var v = inputSource.Vertical(); // 入力デバイスの垂直軸をvで定義
            _anim.SetFloat(Speed, v); // Animator側で設定している"Speed"パラメタにvを渡す
            _anim.SetFloat(Direction, h); // Animator側で設定している"Direction"パラメタにhを渡す
            _anim.speed = animSpeed; // Animatorのモーション再生速度に animSpeedを設定する
            _currentBaseState = _anim.GetCurrentAnimatorStateInfo(0); // 参照用のステート変数にBase Layer (0)の現在のステートを設定する

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