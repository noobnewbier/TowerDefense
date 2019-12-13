using MLAgents;

namespace TrainingSpecific
{
    public struct ResetAcademyEvent
    {
        public ResetAcademyEvent(Academy academy)
        {
            Academy = academy;
        }

        public Academy Academy { get; }
    }
}