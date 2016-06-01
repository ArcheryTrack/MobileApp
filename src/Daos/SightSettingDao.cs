using System;
using LiteDB;
using ATMobile.Objects;

namespace ATMobile.Daos
{
	public class SightSettingDao : AbstractDao<SightSetting>
	{
		public SightSettingDao (LiteDatabase _database): base(_database)
		{
			
		}
			
		public override void BuildIndexes ()
		{
		}

		public override string CollectionName
		{
			get
			{
				return "SightSettings";
			}
		}
	}
}

