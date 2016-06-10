using System;
using LiteDB;
using ATMobile.Objects;
using System.Collections.Generic;

namespace ATMobile.Daos
{
    public class SightSettingDao : AbstractDao<SightSetting>
    {
        public SightSettingDao(LiteDatabase _database) 
            : base(_database)
        {
        }

        public override void BuildIndexes()
        {
        }

        public override string CollectionName
        {
            get
            {
                return "SightSettings";
            }
        }

        public List<SightSetting> GetSightSettings(Guid _archerGuid)
        {
            return GetChildren (_archerGuid);
        }
    }
}

