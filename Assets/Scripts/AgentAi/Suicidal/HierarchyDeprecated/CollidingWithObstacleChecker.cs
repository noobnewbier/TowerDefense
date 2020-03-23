using Common.Constant;
using UnityEngine;
using UnityUtils;
using UnityUtils.BooleanProviders;

namespace AgentAi.Suicidal.Hierarchy
{
    public class CollidingWithObstacleChecker : BooleanProvider, ICollisionEnterDelegate, ICollisionExitDelegate
    {
        private bool _isColliding;

        public void OnCollisionEnterCalled(Collision collision)
        {
            if (collision.collider.CompareTag(ObjectTags.Wall)) _isColliding = true;
        }

        public void OnCollisionExitCalled(Collision collision)
        {
            if (collision.collider.CompareTag(ObjectTags.Wall)) _isColliding = false;
        }

        public override bool ProvideBoolean()
        {
            return _isColliding;
        }
    }
}