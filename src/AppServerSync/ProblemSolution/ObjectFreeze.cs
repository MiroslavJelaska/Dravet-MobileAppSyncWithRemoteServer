using System;

namespace ProblemSolution
{
    public class ObjectFreeze
    {
        public ObjectFreeze()
        {
            CurrentDateTime = DateTime.Now;
        }
        public DateTime CurrentDateTime { get; set; }
        public int NumberOfDaysObjectCanBeModified = 2;

        public bool IsFrozen(MobileApp.SeizureDbObject seizure)
        {
            return seizure.ModificationTimesamp < CurrentDateTime.Subtract(TimeSpan.FromDays(NumberOfDaysObjectCanBeModified));
        }
        public bool IsFrozen(Server.SeizureDbObject seizure)
        {
            return seizure.ModificationTimesamp < CurrentDateTime.Subtract(TimeSpan.FromDays(NumberOfDaysObjectCanBeModified));
        }
    }
}
