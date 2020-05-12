using System.Collections.Generic;
using System.Linq;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Effects;
using Effects.Modifiers;
using Elements;
using Elements.Units.Enemies;
using EventManagement;
using TrainingSpecific.Events;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;

namespace TrainingSpecific
{
    /// <summary>
    ///     Only used to aid training at the moment, damages everything within area, multiple enemies will split damage
    ///     Don't plan to expand on it, dirty as fk, if the need to extend this arises rewrite the whole shit
    /// </summary>
    public class ConstantDamageArea : Element, IHandle<EnemyDeadEvent>
    {
        private Bounds _areaBounds;
        private IList<Enemy> _inRangeEnemies;
        private int _layerMaskToIgnore;

        [SerializeField] private float damage;
        [SerializeField] private Collider occupiedSpaceCollider;

        [FormerlySerializedAs("sphereCollider")] [SerializeField]
        private SphereCollider rangeCollider;

        //can be considered as a turret, it just deal more constant damages.
        protected override InterestCategory Category => InterestCategory.TurretRange;

        public override Bounds Bounds => rangeCollider.bounds;

        /// <summary>
        ///     Different from bounds, indicates how much space it is needed to be spawned
        /// </summary>
        public Bounds OccupiedBounds => occupiedSpaceCollider.bounds;

        private IEnumerable<Enemy> VisibleEnemies => _inRangeEnemies.Where(e => IsTargetVisible(e.transform));

        public float Radius
        {
            set => rangeCollider.radius = value;
        }

        public void Handle(EnemyDeadEvent @event)
        {
            _inRangeEnemies.Remove(@event.Enemy);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
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
            var effectTakers = VisibleEnemies as Enemy[] ?? VisibleEnemies.ToArray();
            if (!effectTakers.Any()) return;

            var damageEffect = HealthInstantEffect.CreateInstantHealthEffect(
                ConstantModifier.CreateInstantHealthEffect(-damage / effectTakers.Length)
            );

            foreach (var effectTaker in effectTakers)
                EventAggregator.Publish(new ApplyEffectEvent(damageEffect, effectTaker, EffectSource.Environment));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) _inRangeEnemies.Add(other.gameObject.GetComponent<Enemy>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy)) _inRangeEnemies.Remove(other.GetComponent<Enemy>());
        }

        /// <summary>
        ///     Being cheeky, this simulates how real turret works
        /// </summary>
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

        private void OnDrawGizmosSelected()
        {
            GizmosHelpers.DrawWireDisc(rangeCollider.radius, transform.position, Color.green);
        }
    }
}