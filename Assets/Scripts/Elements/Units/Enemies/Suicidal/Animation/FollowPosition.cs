using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation
{
    public class FollowPosition : MonoBehaviour
    {
        [SerializeField] private Transform followedTransform;

        private void Update()
        {
            transform.position = followedTransform.position;
        }
    }
}