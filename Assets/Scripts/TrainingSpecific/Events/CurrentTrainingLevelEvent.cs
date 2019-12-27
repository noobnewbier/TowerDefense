namespace TrainingSpecific.Events
{
    public struct CurrentTrainingLevelEvent
    {
        public CurrentTrainingLevelEvent(int level)
        {
            Level = level;
        }

        public int Level { get; }
    }
}