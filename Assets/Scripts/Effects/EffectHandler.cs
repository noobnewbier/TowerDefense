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
        private readonly IUnitDataService _unitDataService;

        private float _timer;

        public EffectHandler
        (
            Effect effect,
            IUnitDataRepository unitDataRepository,
            IUnitDataService unitDataService,
            EffectSource effectSource
        )
        {
            Effect = effect;
            _unitDataRepository = unitDataRepository;
            _unitDataService = unitDataService;
            _effectSource = effectSource;
        }

        /// <summary>
        /// Try to initialize the effect if possible, returning if it can initialize the effect
        /// </summary>
        /// <param name="existingEffects">The effects that the unit already have at the moment</param>
        /// <returns>a boolean flag indicating whether or not the effect should be applied</returns>
        public bool TryInitEffect(IEnumerable<Effect> existingEffects)
        {
            if (!Effect.CanStack && existingEffects.Contains(Effect))
            {
                return false;
            }
            
            Effect.FirstEffectApply(_unitDataService, _unitDataRepository, _effectSource);

            return true;
        }

        public void OnTick(float deltaTime)
        {
            if (_timer > Effect.Duration)
            {
                OnEnd();
                return;
            }
            
            _timer += deltaTime;
            Effect.TickEffect(_unitDataService, _unitDataRepository, _effectSource);
        }

        private void OnEnd()
        {
            IsDone = true;
            
            Effect.EndEffect(_unitDataService, _unitDataRepository, _effectSource);
        }
    }
}