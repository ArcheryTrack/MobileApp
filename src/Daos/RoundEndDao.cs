using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class RoundEndDao : AbstractDao<RoundEnd>
    {
        public RoundEndDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "RoundEnds";
            }
        }
    }
}

