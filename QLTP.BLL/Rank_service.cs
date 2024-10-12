using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLTP.BLL
{
    public enum RankType // Renamed from Rank to RankType
    {
        CP,  // Coupon
        BRZ, // Đồng (Bronze)
        SLV, // Bạc (Silver)
        GLD, // Vàng (Gold)
        PLT, // Bạch kim (Platinum)
        DIA  // Kim cương (Diamond)
    }

    internal class RankService
    {
        private readonly string _connectionString;

        public RankService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Existing methods

        public string GetRankName(string rankId)
        {
            if (!Enum.IsDefined(typeof(RankType), rankId))
            {
                throw new ArgumentOutOfRangeException(nameof(rankId), "Invalid rank ID.");
            }
            return rankId.ToString();
        }

        public string GetRankId(string rankName)
        {
            if (Enum.TryParse(rankName, true, out RankType rank))
            {
                return rank.ToString();
            }
            else
            {
                throw new ArgumentException("Invalid rank name.", nameof(rankName));
            }
        }

        public RankType? GetNextRank(string currentRankId)
        {
            if (!Enum.IsDefined(typeof(RankType), currentRankId))
            {
                throw new ArgumentOutOfRangeException(nameof(currentRankId), "Invalid rank ID.");
            }

            RankType currentRank = (RankType)Enum.Parse(typeof(RankType), currentRankId);

            if (currentRank < RankType.DIA)
            {
                return currentRank + 1;
            }
            return null; // No higher rank available
        }

        // New method to update rank
        /*public void UpdateRank(string currentRankId, string newRankId, string newRankName)
        {
            string query = "UPDATE RankTable SET Rank_id = @newRankId, Rank_name = @newRankName WHERE Rank_id = @currentRankId;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Define parameters
                    command.Parameters.Add(new SqlParameter("@currentRankId", currentRankId));
                    command.Parameters.Add(new SqlParameter("@newRankId", newRankId));
                    command.Parameters.Add(new SqlParameter("@newRankName", newRankName));

                    // Open the connection
                    connection.Open();

                    // Execute the update command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Optionally, check how many rows were affected
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Rank updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No rank found with the specified ID.");
                    }
                }
            }
        }*/
    }
}
