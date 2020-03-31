using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.BooleanProviders;

namespace TrainingSpecific.DynamicObjectController
{
    public abstract class DynamicObjectController : MonoBehaviour
    {
        [FormerlySerializedAs("activityProvider")] [SerializeField]
        private BooleanProvider booleanProvider;

        [FormerlySerializedAs("controllerGameObject")] [FormerlySerializedAs("dynamicObstacle")] [SerializeField]
        protected GameObject controlledGameObject;

        protected abstract void PrepareObjectForTraining();

        protected virtual void CleanUpObjectForTraining()
        {
            //do nothing
        }

        public void TryPrepareObjectForTrainingIfActive()
        {
            var isActive = booleanProvider.ProvideBoolean();

            if (isActive)
            {
                PrepareObjectForTraining();
            }
            else
            {
                CleanUpObjectForTraining();
            }
        }
    }
}