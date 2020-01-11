using System;
using System.Diagnostics;
using System.Linq;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Experimental;
using UnityEngine;

namespace Interactable.PickUps
{
    [RequireComponent(typeof(Collider))]
    public class PickUp : MonoBehaviour, IInteractable
    {
        [SerializeField] private PickUpData data;
        [SerializeField] private ScriptableEventAggregator scriptableEventAggregator;

        private void OnEnable()
        {
            scriptableEventAggregator.Instance.Subscribe(this);

            if (!GetComponent<Collider>().isTrigger)
            {
                throw new InvalidOperationException($"{gameObject}'s 'collider should be a trigger collider");
            }
        }

        private void OnDisable()
        {
            scriptableEventAggregator.Instance.Unsubscribe(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            var effectTaker = other.GetComponent<IEffectTaker>();

            if (effectTaker != null &&data.Rules.All(r => r.AdhereToRule(effectTaker)))
            {
                scriptableEventAggregator.Instance.Publish(new ApplyEffectEvent(data.Effect, effectTaker, EffectSource.Environment));

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