using System.Collections.Generic;
using System.Linq;
using Common.Enum;
using Elements.Units.UnitCommon;

namespace Effects
{
    public class EffectHandler
    {
        public Effect Effect { get; }
        public bool IsDone { get; private set; }
        private readonly EffectSource _effectSource;
        private readonly IUnitDataRepository _unitDataRepository;
        private readonly IUnitDataModificationService _unitDataModificationService;

        private float _timer;
        private bool _isFirstApply = true;

        public EffectHandler
        (
            Effect effect,
            IUnitDataRepository unitDataRepository,
            IUnitDataModificationService unitDataModificationService,
            EffectSource effectSource
        )
        {
            Effect = effect;
            _unitDataRepository = unitDataRepository;
            _unitDataModificationService = unitDataModificationService;
            _effectSource = effectSource;
        }

        /// <summary>
        /// Try to initialize the effect if possible, returning if it can initialize the effect
        /// </summary>
        /// <param name="existingEffects">The effects that the unit already have at the moment</param>
        /// <returns>a boolean flag indicating whether or not the effect should be applied</returns>
        public bool TryInitEffect(IEnumerable<Effect> existingEffects)
        {
            return Effect.CanStack || !existingEffects.Contains(Effect);
        }

        public void OnTick(float deltaTime)
        {
            _timer += deltaTime;
            
            if (_isFirstApply)
            {
                _isFirstApply = false;
                Effect.FirstEffectApply(_unitDataModificationService, _unitDataRepository, _effectSource);
            }
            
            if (_timer > Effect.Duration)
            {
                OnEnd();
                return;
            }
            
            Effect.TickEffect(_unitDataModificationService, _unitDataRepository, _effectSource);
        }

        private void OnEnd()
        {
            IsDone = true;
            
            Effect.EndEffect(_unitDataModificationService, _unitDataRepository, _effectSource);
        }
    }
}