using System;
using System.Collections.Generic;
using System.Linq;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Common.Struct;
using Effects;
using Elements;
using Elements.Units.Enemies;
using TMPro;
using TrainingSpecific.Events;
using UnityEngine;

namespace TrainingSpecific
{
    /// <summary>
    /// Only used to aid training at the moment, damages everything within area, multiple enemies will split damage
    /// Don't plan to expand on it, dirty as fk, if the need to extend this arises rewrite the whole shit
    /// </summary>
    public class ConstantDamageArea : Element
    {
        [SerializeField] private Effect damageEffect;
        [SerializeField] private float radius;
        
        //can be considered as a turret, it just deal more constant damages.
        protected override InterestCategory Category => InterestCategory.TurretRange;
        private int _layerMaskToIgnore;
        private Bounds _areaBounds;
        public override Bounds Bounds => _areaBounds;
        private IEnumerable<Enemy> VisibleEnemies => _inRangeEnemies.Where(e => IsTargetVisible(e.transform));
        private IList<Enemy> _inRangeEnemies;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _areaBounds = new Bounds(transform.position, new Vector3(radius, radius));
            _inRangeEnemies = new List<Enemy>();
            _layerMaskToIgnore = ~((1 << LayerMask.NameToLayer(LayerNames.Turret)) |
                                   (1 << LayerMask.NameToLayer(LayerNames.PlayerDamageTaker)));
            
            EventAggregator.Publish(new ConstantDamageAreaSpawnedEvent(this));
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            EventAggregator.Publish(new ConstantDamageAreaDestroyedEvent(this));
        }

        private void FixedUpdate()
        {
            foreach (var visibleEnemy in VisibleEnemies)
            {
                EventAggregator.Publish(new ApplyEffectEvent(damageEffect, visibleEnemy, EffectSource.Environment));
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) _inRangeEnemies.Add(other.gameObject.GetComponent<Enemy>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) _inRangeEnemies.Remove(other.GetComponent<Enemy>());
        }

        private bool IsTargetVisible(Transform targetTransform)
        {
            var targetPosition = targetTransform.position;
            var selfPosition = transform.position;
            return Physics.Raycast(
                       selfPosition,
                       targetPosition - selfPosition,
                       out var hit,
                       Vector3.Distance(targetPosition, selfPosition),
                       _layerMaskToIgnore
                   ) && hit.collider.CompareTag(ObjectTags.Enemy);
        }
    }
}