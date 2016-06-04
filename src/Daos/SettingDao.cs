using System;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class SettingDao : AbstractDao<Setting>
    {
        public SettingDao(LiteDatabase _db) 
            : base(_db)
        {
        }

        public override void BuildIndexes()
        {
        }

        public override string CollectionName
        {
            get
            {
                return "Settings";
            }
        }
    }
}

