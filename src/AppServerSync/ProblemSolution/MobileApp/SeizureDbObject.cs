using System;

namespace ProblemSolution.MobileApp
{
    public class SeizureDbObject
    {
        public Guid             Id      { get; set; }
        public string           Value   { get; set; } // Real set ot props simplification
        public ObjectState      State   { get; set; }
        public DateTime         ModificationTimesamp { get; set; }
    }
}
