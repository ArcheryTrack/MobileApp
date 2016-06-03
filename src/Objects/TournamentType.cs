﻿using System;

namespace ATMobile.Objects
{
    public class TournamentType
    {
        public TournamentType()
        {
        }

        public string Name { get; set; }

        public int Rounds { get; set; }

        public int EndsPerRound { get; set; }

        public int ArrowsPerEnd { get; set; }
    }
}