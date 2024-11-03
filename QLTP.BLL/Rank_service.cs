using System;
using System.Collections.Generic;
using System.Linq;
using QLTP.DAL;

namespace QLTP.BLL
{
    public enum RankType
    {
        CP,  // Coupon
        BRZ, // Bronze
        SLV, // Silver
        GLD, // Gold
        PLT, // Platinum
        DIA  // Diamond
    }

    public class Rank_service
    {
        // Method to add a new rank to the database
        private static readonly Dictionary<RankType, string> RankNames = new Dictionary<RankType, string>
        {
            { RankType.CP, "Coupon" },
            { RankType.BRZ, "Đồng" },
            { RankType.SLV, "Bạc" },
            { RankType.GLD, "Vàng" },
            { RankType.PLT, "Bạch Kim" },
            { RankType.DIA, "Kim Cương" },
        };

        public int Rank_add(Rank rank)
        {
            if (rank == null) return -1; // Error: null rank object

            using (QLTP_Entities db = new QLTP_Entities())
            {
                if (db.Rank.Any(r => r.Rank_id == rank.Rank_id))
                    return -2; // Error: rank ID already exists

                db.Rank.Add(rank);
                db.SaveChanges();
                return 0; // Successful operation
            }
        }

        // Method to update an existing rank in the database
        public int Rank_update(Rank rank)
        {
            if (rank == null) return -1; // Error: null rank object

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var rankToUpdate = db.Rank.FirstOrDefault(r => r.Rank_id == rank.Rank_id);
                if (rankToUpdate == null)
                    return -2; // Error: rank not found

                rankToUpdate.Rank_name = rank.Rank_name;

                db.Entry(rankToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return 0; // Successful operation
            }
        }

        // Method to delete a rank by Rank_id
        public int Rank_delete(string rankId)
        {
            if (String.IsNullOrEmpty(rankId)) return -1; // Error: null or empty rank ID

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var rankToDelete = db.Rank.FirstOrDefault(r => r.Rank_id == rankId);
                if (rankToDelete == null)
                    return -2; // Error: rank not found

                db.Rank.Remove(rankToDelete);
                db.SaveChanges();
                return 0; // Successful operation
            }
        }

        // Method to get all ranks as a list
        public List<Rank> Rank_search_all()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Rank.ToList();
            }
        }

        // Method to get a single rank by Rank_id
        public Rank Rank_search_unit(string rankId)
        {
            if (String.IsNullOrEmpty(rankId)) return null;

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Rank.FirstOrDefault(r => r.Rank_id == rankId);
            }
        }

        // Method to get the name of a rank from its Rank_id
        public string GetRankName(string rankId)
        {
            if (Enum.TryParse(rankId, true, out RankType rank))
            {
                // Return the mapped name from the dictionary
                return RankNames.TryGetValue(rank, out var rankName) ? rankName : rank.ToString();
            }

            throw new ArgumentOutOfRangeException(nameof(rankId), "Invalid rank ID.");
        }


        // Method to get the Rank_id from its name
        public string GetRankId(string rankName)
        {
            if (Enum.TryParse(rankName, true, out RankType rank))
            {
                return rank.ToString();
            }
            throw new ArgumentException("Invalid rank name.", nameof(rankName));
        }

        // Method to get the next rank based on current Rank_id
        public RankType? GetNextRank(string currentRankId)
        {
            if (Enum.TryParse(currentRankId, true, out RankType currentRank))
            {
                if (currentRank < RankType.DIA)
                {
                    return currentRank + 1;
                }
                return null; // No higher rank available
            }
            throw new ArgumentOutOfRangeException(nameof(currentRankId), "Invalid rank ID.");
        }
    }
}
