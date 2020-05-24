using TMPro;
using UnityEngine;

namespace Ui.Hud.Resource
{
    public interface IFloatTextView
    {
        void OnUpdate(string floatAsString);
    }

    public class FloatTextView : MonoBehaviour, IFloatTextView
    {
        private IFloatTextPresenter _presenter;
        [SerializeField] private FloatTextPresenterFactory presenterFactory;
        [SerializeField] private TextMeshProUGUI textComponent;
        [SerializeField] private string titlePrefix;

        public void OnUpdate(string floatAsString)
        {
            textComponent.text = floatAsString;
        }

        private void OnEnable()
        {
            _presenter = presenterFactory.CreatePresenter(this, titlePrefix);
        }

        private void Update()
        {
            _presenter.OnUpdate();
        }
    }
}