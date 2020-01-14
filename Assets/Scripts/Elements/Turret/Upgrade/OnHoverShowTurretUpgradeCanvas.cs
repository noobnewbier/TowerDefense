using Elements.Turret.Upgrade.UI;
using EventManagement;
using Experimental;
using UnityEngine;

namespace Elements.Turret.Upgrade
{
    public class OnHoverShowTurretUpgradeCanvas : MonoBehaviour, IHandle<UserFocusOnCanvasChangedEvent>
    {
        private IEventAggregator _eventAggregator;
        private bool _shouldCountDownToSwitchOff;
        private float _timerToSwitchOff;
        private float _timerToSwitchOn;
        [SerializeField] private GameObject canvasRoot;
        [SerializeField] private EventAggregatorProvider eventAggregatorProviderProvider;
        [Range(0, 10)] [SerializeField] private float timeThresholdToSwitchOff;
        [Range(0, 10)] [SerializeField] private float timeThresholdToSwitchOn;

        public void Handle(UserFocusOnCanvasChangedEvent @event)
        {
            if (@event.IsFocusingOnCanvas)
                NotCountForSwitchOff();
            else
                CountForSwitchOff();
        }

        private void OnEnable()
        {
            _eventAggregator = eventAggregatorProviderProvider.ProvideEventAggregator();

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        private void OnMouseEnter()
        {
            NotCountForSwitchOff();
        }

        private void OnMouseOver()
        {
            if (canvasRoot.activeSelf) return;

            _timerToSwitchOn += Time.deltaTime;
            if (_timerToSwitchOn > timeThresholdToSwitchOn) ShowUpgradeOptions();
        }

        private void ShowUpgradeOptions()
        {
            canvasRoot.SetActive(true);
        }

        private void HideUpgradeOptions()
        {
            canvasRoot.SetActive(false);
        }

        private void OnMouseExit()
        {
            CountForSwitchOff();
        }

        private void Update()
        {
            if (_shouldCountDownToSwitchOff)
            {
                _timerToSwitchOff += Time.deltaTime;

                if (_timerToSwitchOff > timeThresholdToSwitchOff && canvasRoot.activeSelf)
                {
                    HideUpgradeOptions();
                    NotCountForSwitchOff();
                }
            }
        }

        private void CountForSwitchOff()
        {
            _shouldCountDownToSwitchOff = true;
            _timerToSwitchOn = 0f;
        }

        private void NotCountForSwitchOff()
        {
            _shouldCountDownToSwitchOff = false;
            _timerToSwitchOff = 0f;
        }
    }
}