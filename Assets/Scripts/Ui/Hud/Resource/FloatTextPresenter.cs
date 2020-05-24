using UnityUtils.ScriptableReference;

namespace Ui.Hud.Resource
{
    public interface IFloatTextPresenter
    {
        void OnUpdate();
    }

    public class FloatTextPresenter : IFloatTextPresenter
    {
        private readonly RuntimeFloat _runtimeFloat;
        private readonly IFloatTextView _view;
        private readonly string _prefix;

        public FloatTextPresenter(IFloatTextView view, RuntimeFloat runtimeFloat, string prefix)
        {
            _view = view;
            _runtimeFloat = runtimeFloat;
            _prefix = prefix;
        }

        public void OnUpdate()
        {
            _view.OnUpdate($"{_prefix}{(int) _runtimeFloat.CurrentValue}");
        }
    }
}