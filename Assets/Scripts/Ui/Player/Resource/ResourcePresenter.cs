using Experimental;

namespace Ui.Player.Resource
{
    public interface IResourcePresenter
    {
        void OnUpdate();
    }

    public class ResourcePresenter : IResourcePresenter
    {
        private readonly IResourceView _view;
        private readonly RuntimeFloat _resourceFloat;

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