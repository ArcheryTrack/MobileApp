using System;
using System.Collections.Generic;
using System.Linq;
using ATMobile.Enums;
using ATMobile.Objects;

namespace ATMobile.Managers
{
    public class ChartingManager
    {
        private ATManager m_Manager;

        public ChartingManager (ATManager manager)
        {
            m_Manager = manager;
        }

        public void ProcessEnd (PracticeEnd _practiceEnd)
        {
            int count = _practiceEnd.Results.Count;
            double possible = _practiceEnd.Results.Sum ((ShotArrow arg) => arg.PossiblePoints);
            double sum = _practiceEnd.Results.Sum ((ShotArrow arg) => arg.ScoreValue);
            double min = _practiceEnd.Results.Min ((ShotArrow arg) => arg.ScoreValue);
            double max = _practiceEnd.Results.Max ((ShotArrow arg) => arg.ScoreValue);

            ChartEntry chartEntry = new ChartEntry {
                Id = _practiceEnd.Id,
                ParentId = _practiceEnd.ParentId,
                Sequence = _practiceEnd.EndNumber,
                Date = null,
                ChartEntryType = ChartEntryTypes.End,
                Count = count,
                Score = sum,
                Possible = possible,
                Maximum = max,
                Minimum = min,
            };

            m_Manager.Persist (chartEntry);

            ProcessPractice (_practiceEnd.ParentId, _practiceEnd.ArcherId);
        }

        public void ProcessPractice (Guid _practiceId, Guid _archerId)
        {
            Practice practice = m_Manager.GetPractice (_practiceId);

            //Load all chart entries
            List<ChartEntry> entries = m_Manager.GetChartEntriesForPractice (_practiceId, _archerId);

            int count = entries.Sum ((ChartEntry arg) => arg.Count);
            double possible = entries.Sum ((ChartEntry arg) => arg.Possible);
            double sum = entries.Sum ((ChartEntry arg) => arg.Score);
            double min = entries.Min ((ChartEntry arg) => arg.Minimum);
            double max = entries.Max ((ChartEntry arg) => arg.Maximum);

            ChartEntry chartEntry = new ChartEntry {
                Id = _practiceId,
                ParentId = null,
                Sequence = null,
                Date = practice.DateTime,
                ChartEntryType = ChartEntryTypes.End,
                Count = count,
                Score = sum,
                Possible = possible,
                Maximum = max,
                Minimum = min,
            };

            m_Manager.Persist (chartEntry);
        }

        public void ProcessEnd (TournamentEnd _tournamentEnd)
        {
            int count = _tournamentEnd.Results.Count;
            double possible = _tournamentEnd.Results.Sum ((ShotArrow arg) => arg.PossiblePoints);
            double sum = _tournamentEnd.Results.Sum ((ShotArrow arg) => arg.ScoreValue);
            double min = _tournamentEnd.Results.Min ((ShotArrow arg) => arg.ScoreValue);
            double max = _tournamentEnd.Results.Max ((ShotArrow arg) => arg.ScoreValue);

            ChartEntry chartEntry = new ChartEntry {
                Id = _tournamentEnd.Id,
                ParentId = _tournamentEnd.ParentId,
                Sequence = _tournamentEnd.EndNumber,
                Date = null,
                ChartEntryType = ChartEntryTypes.End,
                Count = count,
                Score = sum,
                Possible = possible,
                Maximum = max,
                Minimum = min,
            };

            m_Manager.Persist (chartEntry);

            ProcessRound (_tournamentEnd.ParentId, _tournamentEnd.ArcherId);
        }

        public void ProcessRound (Guid _roundId, Guid _archerId)
        {
            Round round = m_Manager.GetRound (_roundId);

            //Load all chart entries
            List<ChartEntry> entries = m_Manager.GetChartEntriesForPractice (_roundId, _archerId);

            int count = entries.Sum ((ChartEntry arg) => arg.Count);
            double possible = entries.Sum ((ChartEntry arg) => arg.Possible);
            double sum = entries.Sum ((ChartEntry arg) => arg.Score);
            double min = entries.Min ((ChartEntry arg) => arg.Minimum);
            double max = entries.Max ((ChartEntry arg) => arg.Maximum);

            ChartEntry chartEntry = new ChartEntry {
                Id = _roundId,
                ParentId = null,
                Sequence = null,
                Date = round.DateTime,
                ChartEntryType = ChartEntryTypes.End,
                Count = count,
                Score = sum,
                Possible = possible,
                Maximum = max,
                Minimum = min,
            };

            m_Manager.Persist (chartEntry);
        }

        public void ProcessTournament (Guid _id)
        {
        }
    }
}

