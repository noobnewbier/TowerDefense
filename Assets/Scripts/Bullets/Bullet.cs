using System;
using System.Diagnostics;
using Common;
using Common.Events;
using EventManagement;
using UnityEngine;
using UnityUtils;

namespace Bullets
{
    public class Bullet : PooledMonoBehaviour
    {
        private float _cumulatedTraveledDistance;
        private IEventAggregator _eventAggregator;
        private LayerMask _layerMask;

        [SerializeField] private BulletData data;
        public int Damage => data.Damage;

        private void Awake()
        {
            #region set up layer so we do not hit friendly fire

            string layerToIgnore;

            switch (tag)
            {
                case ObjectTags.DamageAi:
                    layerToIgnore = LayerNames.PlayerDamageTaker;
                    break;
                case ObjectTags.DamagePlayer:
                    layerToIgnore = LayerNames.AiDamageTaker;
                    break;
                default:
                    throw new NotImplementedException("dude, wth am I supposed to do? What should I hit? I am confused as a bullet");
            }
            
            _layerMask = ~(1 << LayerMask.NameToLayer(layerToIgnore));

            #endregion
        }
        
        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _cumulatedTraveledDistance = 0f;
        }

        private void FixedUpdate()
        {
            // it is not the most precise calculation, but should do the trick - if there is a bug think about this shit.
            var traveledDistance = data.Speed * Time.fixedDeltaTime;
            _cumulatedTraveledDistance += traveledDistance;

            if (Physics.Raycast(transform.position, transform.forward, out var hit, traveledDistance, _layerMask))
            {
                OnCollide(hit);
            }

            if (_cumulatedTraveledDistance > data.Range)
            {
                AfterEffect(hit.point);
                SelfDestroy();
            }
            else
            {
                var selfTransform = transform;
                selfTransform.position += selfTransform.forward * traveledDistance;
            }
        }

        [Conditional(GameConfig.GameplayMode)]
        private void AfterEffect(Vector3 effectPos)
        {
            var afterEffect = Instantiate(data.AfterEffect);
            afterEffect.transform.position = effectPos;
        }
        
        private void OnCollide(RaycastHit hit)
        {
            var damageTaker = hit.transform.GetComponent<IDamageTaker>();

            if (damageTaker != null)
            {
                DoDamage(damageTaker);
            }

            AfterEffect(hit.point);
            SelfDestroy();
        }
        
        private void DoDamage(IDamageTaker damageTaker)
        {
            _eventAggregator.Publish(new DamageEvent(damageTaker, data.Damage));
        }
        
        private void SelfDestroy()
        {
            ReturnToPool();
        }
    }
}