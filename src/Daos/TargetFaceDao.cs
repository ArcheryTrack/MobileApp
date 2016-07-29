using System;
using System.Collections.Generic;
using ATMobile.Data;
using ATMobile.Objects;
using LiteDB;

namespace ATMobile.Daos
{
    public class TargetFaceDao : AbstractDao<TargetFace>
    {
        public TargetFaceDao (LiteDatabase _db)
            : base (_db)
        {
        }

        public override void BuildIndexes ()
        {
        }

        public override string CollectionName {
            get {
                return "TargetFaces";
            }
        }

        public List<TargetFace> GetTargetFaces ()
        {
            List<TargetFace> faces = GetAll ();

            if (faces.Count == 0) {
                //Default the data

                faces = TargetFaceData.GetData ();

                foreach (var item in faces) {
                    Persist (item);
                }
            }

            return faces;
        }
    }
}

