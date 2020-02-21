using UnityEngine;

namespace Elements.Turret.Rotation
{
    /*
     * Literally named : Rotating Turret Structure, god damn it
     * https://en.wikipedia.org/wiki/Gun_turret#/media/File:Iowa_16_inch_Gun-EN.svg
     */
    public class RotatingStructureRotator : Rotator
    {
        [SerializeField] private Rotator verticalDialRotator;
        [SerializeField] private Rotator horizontalDialRotator;
        [SerializeField] private Transform bulletSpawnpoint;
        
        public override void LookAt(Vector3 target, float speed)
        {
            var verticalRotatorPosition = verticalDialRotator.transform.position;
            var verticalPositionOffset = verticalRotatorPosition - bulletSpawnpoint.position + target;
            //todo: figure out the actual math, for now it works fine I guess
            // verticalDialRotator.LookAt(new Vector3(verticalRotatorPosition.x, verticalPositionOffset.y, verticalRotatorPosition.z), speed);
            horizontalDialRotator.LookAt(new Vector3(target.x, horizontalDialRotator.transform.position.y,  target.z), speed);
        }
    }
}