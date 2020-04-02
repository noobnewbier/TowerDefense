namespace AgentAi.Suicidal.HierarchyDeprecated.Event
{
    public struct FinishPathingEvent
    {
        public FinishPathingEvent(bool previousTargetReached)
        {
            PreviousTargetReached = previousTargetReached;
        }

        public bool PreviousTargetReached { get; }
    }
}