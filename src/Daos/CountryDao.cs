using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Daos;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class CountryDao : AbstractDao<Country>
    {
        public CountryDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override string CollectionName {
            get {
                return "Countries";
            }
        }

        public override void BuildIndexes ()
        {
            //TODO - Build name index
        }

        public List<Country> GetCountries ()
        {
            var countries = GetAll ();

            return countries.OrderBy ((Country arg) => arg.Name).ToList ();
        }
    }
}

