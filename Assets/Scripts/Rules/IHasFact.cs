using System.Collections.Generic;

namespace Rules
{
    public interface IHasFact
    {
        IEnumerable<Fact> Facts { get; }
    }
}