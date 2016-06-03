using System;
using ATMobile.Objects;

namespace ATMobile.Daos
{
	public class PracticeDao : AbstractDao<Practice>
	{
		public PracticeDao(LiteDB _db): base(_db)
		{
		}

		public override void BuildIndexes()
		{
			throw new NotImplementedException();
		}

		public override string CollectionName
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}

