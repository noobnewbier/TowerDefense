using Experimental;
using UnityEngine;

namespace Ui
{
    public class MapElementYPositionIntializer : MonoBehaviour
    {
        [SerializeField] private FloatVariable yPosition;

        private void Update()
        {
            var selfTransform = transform;
            var position = selfTransform.position;
            position = new Vector3(position.x, yPosition, position.z);
            
            selfTransform.position = position;
            
            Destroy(this);
        }
    }
}