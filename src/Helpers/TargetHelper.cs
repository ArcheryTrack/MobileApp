using System;
using System.Collections.Generic;
using ATMobile.Data;
using ATMobile.Objects;

namespace ATMobile.Helpers
{
    public static class TargetHelper
    {
        public static TargetFace FindTarget (Guid _targetId)
        {
            List<TargetFace> targets = TargetFaceData.GetData ();

            foreach (var item in targets) {
                if (item.Id.Equals (_targetId)) {
                    return item;
                }
            }

            return null;
        }
    }
}

