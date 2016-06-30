using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class StateDao : AbstractDao<State>
    {
        public StateDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override string CollectionName {
            get {
                return "States";
            }
        }

        public override void BuildIndexes ()
        {
            //TODO - Build name index
        }

        public List<State> GetStates (Guid _countryId)
        {
            var states = GetChildren (_countryId);

            return states.OrderBy ((State arg) => arg.Name).ToList ();
        }
    }
}

