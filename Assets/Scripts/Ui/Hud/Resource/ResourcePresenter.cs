using UnityUtils.ScriptableReference;

namespace Ui.Hud.Resource
{
    public interface IResourcePresenter
    {
        void OnUpdate();
    }

    public class ResourcePresenter : IResourcePresenter
    {
        private readonly RuntimeFloat _resourceFloat;
        private readonly IResourceView _view;

        public ResourcePresenter(IResourceView view, RuntimeFloat resourceFloat)
        {
            _view = view;
            _resourceFloat = resourceFloat;
        }

        public void OnUpdate()
        {
            _view.OnResourceUpdate($"Resource: {(int) _resourceFloat.CurrentValue}");
        }
    }
}