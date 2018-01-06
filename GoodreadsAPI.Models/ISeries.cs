using System;
using System.Collections.Generic;
using System.Text;

namespace GoodreadsAPI.Models
{
    public interface ISeries
    {
        int Id { get; }
        bool IsOrdered { get; }

        string Title { get; }
        string Description { get; }
        string Note { get; }
        int WorksCount { get; }
        int PrimaryWorksCount { get; }
        IReadOnlyCollection<IWork> Works { get; }
    }
}
