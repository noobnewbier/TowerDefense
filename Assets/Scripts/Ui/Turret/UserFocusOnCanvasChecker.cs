using EventManagement;
using Experimental;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui.Turret
{
    public class UserFocusOnCanvasChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private IEventAggregator _eventAggregator;
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _eventAggregator.Publish(new UserFocusOnCanvasChangedEvent(true));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _eventAggregator.Publish(new UserFocusOnCanvasChangedEvent(false));
        }

        private void OnEnable()
        {
            _eventAggregator = eventAggregatorProvider.ProvideEventAggregator();
        }
    }
}