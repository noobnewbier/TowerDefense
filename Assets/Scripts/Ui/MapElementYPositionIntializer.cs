using System.Diagnostics;
using Common.Constant;
using Experimental;
using UnityEngine;

namespace Ui
{
    public class MapElementYPositionIntializer : MonoBehaviour
    {
        [SerializeField] private FloatVariable yPosition;

        private void Start()
        {
            RemoveForTraining();
            SetupForMinimap();
        }

        [Conditional(GameConfig.TrainingMode)]
        private void RemoveForTraining()
        {
            Destroy(gameObject);
        }

        [Conditional(GameConfig.GameplayMode)]
        private void SetupForMinimap()
        {
            var selfTransform = transform;
            var position = selfTransform.position;
            position = new Vector3(position.x, yPosition, position.z);
            
            selfTransform.position = position;
            
            Destroy(this);    
        }
    }
}