using System;
using EventManagement;

namespace Common
{
    //Introduced only to not touch the code in sub module - what a genius
    public class EventAggregatorHolder
    {
        private static readonly Lazy<EventAggregator> LazyInstance = new Lazy<EventAggregator>(() => new EventAggregator());

        public static EventAggregator Instance => LazyInstance.Value;
    }
}