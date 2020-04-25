using System;
using System.Diagnostics;
using System.Linq;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using EventManagement;
using EventManagement.Providers;
using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactable.PickUps
{
    [RequireComponent(typeof(Collider))]
    public class PickUp : MonoBehaviour, IInteractable
    {
        [SerializeField] private PickUpData data;
        [FormerlySerializedAs("scriptableEventAggregator")] [SerializeField] private EventAggregatorProvider eventAggregatorProvider;

        private void OnEnable()
        {
            eventAggregatorProvider.ProvideEventAggregator().Subscribe(this);

            if (!GetComponent<Collider>().isTrigger)
            {
                throw new InvalidOperationException($"{gameObject}'s 'collider should be a trigger collider");
            }
        }

        private void OnDisable()
        {
            eventAggregatorProvider.ProvideEventAggregator().Unsubscribe(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            var effectTaker = other.GetComponent<IEffectTaker>();

            if (effectTaker != null &&data.Rules.All(r => r.AdhereToRule(effectTaker)))
            {
                eventAggregatorProvider.ProvideEventAggregator().Publish(new ApplyEffectEvent(data.Effect, effectTaker, EffectSource.Environment));

                SelfDestroy();
            }
        }

        private void SelfDestroy()
        {
            VisualDestroyEffect();
            Destroy(gameObject);
        }

        [Conditional(GameConfig.GameplayMode)]
        private void VisualDestroyEffect()
        {
            //TODO: Add visual effect
        }
    }
}