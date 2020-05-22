using System.Linq;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Effects;
using Elements.Units.UnitCommon;
using Rules;
using UnityEngine;

namespace Elements.Units.Enemies.Suicidal
{
    //enemy that touches you and explode, deals damage and destruct itself
    public class SuicidalEnemy : Enemy
    {
        private IUnitDataModificationService _unitDataModificationService;

        private IUnitDataRepository _unitDataRepository;
        [SerializeField] private Effect damageEffect;
        [SerializeField] private UnitProvider provider;
        [SerializeField] private Rule[] rules;
        [SerializeField] private Effect selfEffectWhenCollide;

        public override float YEuler
        {
            get => RealRotation.eulerAngles.y;
            set => RealRotation = Quaternion.Euler(RealRotation.eulerAngles.x, value, RealRotation.eulerAngles.z);
        }

        protected override IUnitDataRepository UnitDataRepository => _unitDataRepository;
        protected override IUnitDataModificationService UnitDataModificationService => _unitDataModificationService;

        //todo: hack 1
        public Quaternion RealRotation { get; set; }

        protected override void Awake()
        {
            base.Awake();

            _unitDataRepository = provider.ProvideUnitDataRepository();
            _unitDataModificationService = provider.ProvideUnitDataService();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            RealRotation = transform.rotation;
        }

        protected override void DeathVisualEffect()
        {
            // do nothing for now
        }

        private void OnCollisionEnter(Collision other)
        {
            var effectTaker = other.gameObject.GetComponent<IEffectTaker>();
            if (effectTaker != null && rules.All(r => r.AdhereToRule(effectTaker)))
            {
                EventAggregator.Publish(new ApplyEffectEvent(damageEffect, effectTaker, EffectSource.Ai));
                ApplyEffect(selfEffectWhenCollide, EffectSource.SelfDestruction);
            }
        }
    }
}