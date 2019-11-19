using System;
using EventManagement;

namespace Common.Class
{
    //Introduced only to not touch the code in sub module - what a genius
    public static class EventAggregatorHolder
    {
        private static readonly Lazy<EventAggregator> LazyInstance = new Lazy<EventAggregator>(() => new EventAggregator());

        public static EventAggregator Instance => LazyInstance.Value;
    }
}