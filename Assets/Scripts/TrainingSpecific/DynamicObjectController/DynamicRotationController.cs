using UnityEngine;
using UnityUtils.FloatProvider;

namespace TrainingSpecific.DynamicObjectController
{
    public class DynamicRotationController : DynamicObjectController
    {
        [SerializeField] private FloatProvider eulerYProvider;

        protected override void PrepareObjectForTraining()
        {
            var transformRotation = controlledGameObject.transform.rotation;
            transformRotation.eulerAngles = new Vector3(
                transformRotation.eulerAngles.x,
                eulerYProvider.ProvideFloat(),
                transformRotation.eulerAngles.z
            );

            controlledGameObject.transform.rotation = transformRotation;
        }
    }
}