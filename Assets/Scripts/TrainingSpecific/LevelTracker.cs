using System;
using Common.Class;
using EventManagement;
using TrainingSpecific.Events;

namespace TrainingSpecific
{
    public class LevelTracker : IHandle<CurrentTrainingLevelEvent>
    {
        private static readonly Lazy<LevelTracker> LazyInstance = new Lazy<LevelTracker>(() => new LevelTracker(EventAggregatorHolder.Instance));

        private LevelTracker(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }

        public static LevelTracker Instance => LazyInstance.Value;

        public int CurrentLevel { get; private set; }

        public void Handle(CurrentTrainingLevelEvent @event)
        {
            CurrentLevel = @event.Level;
        }
    }
}