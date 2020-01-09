using Common.Enum;
using Elements.Units.UnitCommon;

namespace Effects
{
    public class EffectHandler
    {
        private readonly Effect _effect;
        private readonly EffectSource _effectSource;
        private readonly IUnitDataRepository _unitDataRepository;
        private readonly IUnitDataService _unitDataService;

        private int _timer;

        public EffectHandler
        (
            Effect effect,
            IUnitDataRepository unitDataRepository,
            IUnitDataService unitDataService,
            EffectSource effectSource
        )
        {
            _effect = effect;
            _unitDataRepository = unitDataRepository;
            _unitDataService = unitDataService;
            _effectSource = effectSource;
        }

        public void InitEffect()
        {
            _effect.FirstEffectApply(_unitDataService, _unitDataRepository, _effectSource);
        }

        public void OnTick()
        {
            if (_timer > _effect.Duration)
            {
                OnEnd();
            }
            
            _timer++;
            _effect.TickEffect(_unitDataService, _unitDataRepository, _effectSource);
        }

        private void OnEnd()
        {
            _effect.EndEffect(_unitDataService, _unitDataRepository, _effectSource);
        }
    }
}