using System;
using Common.Class;
using Common.Constant;
using Elements.Units.Players;
using EventManagement;
using UnityEngine;

namespace Interactable.PickUps
{
    [RequireComponent(typeof(Collider))]
    public class PickUp : MonoBehaviour, IInteractable
    {
        private IEventAggregator _eventAggregator;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);

            if (!GetComponent<Collider>().isTrigger)
            {
                throw new InvalidOperationException($"{gameObject}'s 'collider should be a trigger collider");
            }
        }


        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            var canInteract = other.GetComponent<ICanInteract>();
            if (canInteract != null)
            {
                
            }
        }
    }
}