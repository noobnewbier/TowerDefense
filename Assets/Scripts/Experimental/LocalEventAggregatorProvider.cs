using System;
using EventManagement;
using UnityEngine;

namespace Experimental
{
    public class LocalEventAggregatorProvider : MonoBehaviour
    {
        private Lazy<EventAggregator> LazyInstance = new Lazy<EventAggregator>(() => new EventAggregator());
        public IEventAggregator ProvideEventAggregator() => LazyInstance.Value;
    }
}