using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSolution.Server
{
    public class API
    {
        private readonly List<SeizureDbObject> databaseSeizures = new List<SeizureDbObject>();

        public List<SeizureDbObject> Create(List<MobileApp.SeizureDbObject> seizures)
        {
            var newSeizures = new List<SeizureDbObject>();
            foreach (var seizure in seizures)
            {
                var newSeizure = new SeizureDbObject()
                {
                    Id = Guid.NewGuid(),
                    Value = seizure.Value,
                    ModificationTimesamp = seizure.ModificationTimesamp
                };
                databaseSeizures.Add(newSeizure);
                newSeizures.Add(newSeizure);
            }
            return newSeizures;
        }

        public void Update(List<MobileApp.SeizureDbObject> seizures, DateTime modificationTimestamp)
        {
            foreach (var seizure in seizures)
            {
                var dbSeizure = databaseSeizures.Find(_ => _.Id == seizure.Id);
                if (dbSeizure.ModificationTimesamp < seizure.ModificationTimesamp)
                {
                    dbSeizure.Value = seizure.Value;
                    dbSeizure.ModificationTimesamp = modificationTimestamp;
                }
            }
        }

        public void Delete(List<Guid> seizureIds)
        {
            foreach (var seizureId in seizureIds)
            {
                var seizure = databaseSeizures.Find(_ => _.Id == seizureId);
                if (seizure != null)
                    databaseSeizures.Remove(seizure);
            }
        }

        public List<SeizureDbObject> Pull(DateTime cumulativeDateAck)
        {
            var objectFreeze = new ObjectFreeze();
            return
                databaseSeizures
                .Where(_ => _.ModificationTimesamp < cumulativeDateAck)
                .ToList();
        }
    }
}
