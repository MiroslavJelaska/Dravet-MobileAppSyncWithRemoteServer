using System;

namespace ProblemSolution.Server
{
    public class SeizureDbObject
    {
        public Guid             Id      { get; set; }
        public string           Value   { get; set; } // Real set ot props simplification
        public DateTime         ModificationTimesamp { get; set; }
    }
}
