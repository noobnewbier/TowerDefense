using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityChan
{
    public class FaceUpdate : MonoBehaviour
    {
        private Animator _anim;
        public AnimationClip[] animations;

        private float current;
        public float delayWeight;
        public bool isKeepFace;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                current = 1;
            }
            else if (!isKeepFace)
            {
                current = Mathf.Lerp(current, 0, delayWeight);
            }

            _anim.SetLayerWeight(1, current);
        }


        //アニメーションEvents側につける表情切り替え用イベントコール
        public void OnCallChangeFace(string str)
        {
            var ichecked = 0;
            foreach (var animation in animations)
                if (str == animation.name)
                {
                    ChangeFace(str);
                    break;
                }
                else if (ichecked <= animations.Length)
                {
                    ichecked++;
                }
                else
                {
                    //str指定が間違っている時にはデフォルトで
                    str = "default@unitychan";
                    ChangeFace(str);
                }
        }

        private void ChangeFace(string str)
        {
            isKeepFace = true;
            current = 1;
            _anim.CrossFade(str, 0);
        }
    }
}