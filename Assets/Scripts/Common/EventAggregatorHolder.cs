using System;
using EventManagement;

namespace Common
{
    public class EventAggregatorHolder
    {
        private static readonly Lazy<EventAggregator> LazyInstance = new Lazy<EventAggregator>(() => new EventAggregator());

        public static EventAggregator Instance => LazyInstance.Value;
    }
}