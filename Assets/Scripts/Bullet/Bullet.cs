using System;
using System.Diagnostics;
using Common.Class;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Effects;
using EventManagement;
using UnityEngine;
using UnityUtils;
using Debug = UnityEngine.Debug;

namespace Bullet
{
    public class Bullet : PooledMonoBehaviour
    {
        private EffectSource _bulletEffectSource;
        private float _cumulatedTraveledDistance;
        private IEventAggregator _eventAggregator;
        private LayerMask _layerMask;
        
        [SerializeField] private BulletData data;

        private void Awake()
        {
            #region set up layer so we do not friendly fire

            string layerToIgnore;

            switch (tag)
            {
                case ObjectTags.DamageAi:
                    layerToIgnore = LayerNames.PlayerDamageTaker;
                    _bulletEffectSource = EffectSource.Player;
                    break;
                case ObjectTags.DamagePlayer:
                    layerToIgnore = LayerNames.AiDamageTaker;
                    _bulletEffectSource = EffectSource.Ai;
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

            if (Physics.Raycast(transform.position, transform.forward, out var hit, traveledDistance, _layerMask, QueryTriggerInteraction.Ignore))
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
            var effectTaker = hit.transform.GetComponent<IEffectTaker>();

            if (effectTaker != null)
            {
                DoDamage(effectTaker);
            }
            AfterEffect(hit.point);
            SelfDestroy();
        }

        private void DoDamage(IEffectTaker effectTaker)
        {
            _eventAggregator.Publish(new ApplyEffectEvent(data.DamageEffect, effectTaker, _bulletEffectSource));
        }

        private void SelfDestroy()
        {
            ReturnToPool();
        }
    }
}