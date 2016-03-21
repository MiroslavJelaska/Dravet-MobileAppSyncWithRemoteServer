using System;
using System.Collections.Generic;
using System.Linq;
using ProblemSolution.Server;

namespace ProblemSolution.MobileApp
{
    public class DbContext
    {
        public DbContext(API webApi)
        {
            _webApi = webApi;
        }

        private readonly API _webApi;
        private readonly List<SeizureDbObject> _seizures = new List<SeizureDbObject>();

        public SeizureDbObject CreateSeizure(string value, DateTime modificationTimestamp)
        {
            var newSeizure = new SeizureDbObject()
            {
                Id = Guid.NewGuid(),
                Value = value,
                ModificationTimesamp = modificationTimestamp, //new DateTime(2016, 1, 10, 12,0,0),
                State = ObjectState.Created
            };
            _seizures.Add(newSeizure);

            return newSeizure;
        }

        public SeizureDbObject UpdateSeizure(Guid id, string value)
        {
            var seizureToUpdate = _seizures.First(_ => _.Id == id);
            if (seizureToUpdate == null || seizureToUpdate.State == ObjectState.Deleted) return null;

            if (seizureToUpdate.State == ObjectState.Created)
            {
                seizureToUpdate.Value = value;
            }
            else if(seizureToUpdate.State == ObjectState.Modified)
            {
                seizureToUpdate.Value = value;
            }
            else if(seizureToUpdate.State == ObjectState.Unmodified)
            {
                seizureToUpdate.Value = value;
                seizureToUpdate.State = ObjectState.Modified;
            }
            
            return seizureToUpdate;
        }

        public void DeleteSeizure(Guid id)
        {
            var seizureToDelete = _seizures.First(_ => _.Id == id);
            if(seizureToDelete == null) return;
            
            seizureToDelete.State = ObjectState.Deleted;
        }

        private void Pull()
        {
            var objectFreeze = new ObjectFreeze();
            var latestSyncedDate =
                _seizures
               .Where(_ => _.State != ObjectState.Unmodified)
               .Where(_ => objectFreeze.IsFrozen(_))
               .Max(_ => _.ModificationTimesamp)
               .Subtract(TimeSpan.FromDays(objectFreeze.NumberOfDaysObjectCanBeModified));

            var unsyncedRecordsFromServer = _webApi.Pull(latestSyncedDate);

            // Update

            // 
        }
    }
}
