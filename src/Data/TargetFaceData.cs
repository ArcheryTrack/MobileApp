using System;
using System.Collections.Generic;
using ATMobile.Objects;

namespace ATMobile.Data
{
    /// <summary>
    /// Temporary - load from server later.
    /// </summary>
    public static class TargetFaceData
    {
        public static List<TargetFace> GetData ()
        {
            List<TargetFace> faces = new List<TargetFace> ();

            faces.Add (new TargetFace { Id = new Guid ("fedcd971-6a9a-40c2-84a2-6b3b6863d0fe"), Name = "FITA 3 Spot", MinimumPoints = 4, MaximumPoints = 5, Size = new Distance { Measurement = 40, Units = Enums.DistanceUnits.Centimeters } });
            faces.Add (new TargetFace { Id = new Guid ("d1e1ee63-bf12-4b8e-8e7f-5038b5611837"), Name = "FITA 10 Ring Single Spot", MinimumPoints = 1, MaximumPoints = 10, Size = new Distance { Measurement = 40, Units = Enums.DistanceUnits.Centimeters } });
            faces.Add (new TargetFace { Id = new Guid ("f87e4430-f260-4687-979d-87bd3e633040"), Name = "NFAA Single Spot", MinimumPoints = 1, MaximumPoints = 5, Size = new Distance { Measurement = 40, Units = Enums.DistanceUnits.Centimeters } });
            faces.Add (new TargetFace { Id = new Guid ("97e94f83-2208-4873-996b-55c29add340c"), Name = "NFAA 5 Spot", MinimumPoints = 4, MaximumPoints = 5, Size = new Distance { Measurement = 40, Units = Enums.DistanceUnits.Centimeters } });
            faces.Add (new TargetFace { Id = new Guid ("556db410-ce22-4b1b-b0ed-cbb02230c643"), Name = "10 Ring -  60 CM", MinimumPoints = 1, MaximumPoints = 10, Size = new Distance { Measurement = 60, Units = Enums.DistanceUnits.Centimeters } });
            faces.Add (new TargetFace { Id = new Guid ("ed31b693-015e-49dc-965a-93d093ff5bad"), Name = "10 Ring -  80 CM", MinimumPoints = 1, MaximumPoints = 10, Size = new Distance { Measurement = 80, Units = Enums.DistanceUnits.Centimeters } });
            faces.Add (new TargetFace { Id = new Guid ("ed43f96e-0690-4409-888f-420a73c9e594"), Name = "10 Ring - 122 CM", MinimumPoints = 1, MaximumPoints = 10, Size = new Distance { Measurement = 122, Units = Enums.DistanceUnits.Centimeters } });

            return faces;
        }
    }
}

