using TMPro;
using UnityEngine;

namespace Ui.Hud.Resource
{
    public interface IResourceView
    {
        void OnResourceUpdate(string resourceString);
    }

    public class ResourceView : MonoBehaviour, IResourceView
    {
        [SerializeField] private ResourcePresenterFactory presenterFactory;
        [SerializeField] private TextMeshProUGUI textComponent;
        
        private IResourcePresenter _presenter;

        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this);
        }

        private void Update()
        {
            _presenter.OnUpdate();
        }
        
        public void OnResourceUpdate(string resourceString)
        {
            textComponent.text = resourceString;
        }
    }
}