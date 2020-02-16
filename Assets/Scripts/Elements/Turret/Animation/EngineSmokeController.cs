using UnityEngine;
using UnityUtils.Timers;

namespace Elements.Turret.Animation
{
    public class EngineSmokeController : MonoBehaviour
    {
        [SerializeField] private GameObject[] particleSystemsGameObjects;
        [SerializeField] private ThresholdTimer startSmokeTimer;
        [SerializeField] private ThresholdTimer stopSmokeTimer;


        private void Update()
        {
            if (stopSmokeTimer.PassedThreshold)
                SetParticlesActivity(false);
            else if (startSmokeTimer.PassedThreshold) SetParticlesActivity(true);
        }

        private void SetParticlesActivity(bool shouldEmitParticles)
        {
            foreach (var particleSystemsGameObject in particleSystemsGameObjects)
                particleSystemsGameObject.SetActive(shouldEmitParticles);
        }
    }
}