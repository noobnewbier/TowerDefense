using UnityEngine;
using UnityUtils.Timers;

namespace Elements.Turret.Animation
{
    public class EngineSmokeController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particleSystems;
        [SerializeField] private ThresholdTimer startSmokeTimer;
        [SerializeField] private ThresholdTimer stopSmokeTimer;
        private bool _isCurrentlyPlaying = false;


        private void Update()
        {
            if (stopSmokeTimer.PassedThreshold && _isCurrentlyPlaying)
                SetParticlesActivity(false);
            else if (startSmokeTimer.PassedThreshold && !_isCurrentlyPlaying) SetParticlesActivity(true);
        }

        private void SetParticlesActivity(bool shouldEmitParticles)
        {
            _isCurrentlyPlaying = shouldEmitParticles;
            foreach (var p in particleSystems)
                if (shouldEmitParticles)
                    p.Play();
                else
                    p.Stop();
        }
    }
}