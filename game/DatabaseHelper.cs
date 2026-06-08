using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
class DatabaseHelper
{
   
    private readonly string _connString = "Server=localhost;Port=3306;Database=CarGameDB;Uid=root;Pwd=Minerooh.1;";
    public void EnsureTableExists()
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();

                string sql = @"
                    CREATE TABLE IF NOT EXISTS HighScores (
                        Id           INT AUTO_INCREMENT PRIMARY KEY,
                        PlayerName   VARCHAR(50)  NOT NULL,
                        Score        INT          NOT NULL,
                        TimeSurvived VARCHAR(10)  NOT NULL,
                        PlayedAt     DATETIME     DEFAULT CURRENT_TIMESTAMP
                    )";
                new MySqlCommand(sql, conn).ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n [DB] Setup error: {ex.Message}");
            Console.ResetColor();
        }
    }
    public bool SaveScore(ScoreRecord record)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();    

               
                string query = "INSERT INTO HighScores (PlayerName, Score, TimeSurvived) VALUES (@name, @s, @t)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@name", record.PlayerName);
                cmd.Parameters.AddWithValue("@s", record.Score);
                cmd.Parameters.AddWithValue("@t", record.TimeSurvived);

                cmd.ExecuteNonQuery();  
                return true;          
            }
        }
        catch (Exception ex)
        {
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n [DB] Save error: {ex.Message}");
            Console.ResetColor();
            return false;
        }
    }

    public List<ScoreRecord> GetTopScores(int count = 5)
    {
        List<ScoreRecord> results = new List<ScoreRecord>();
        try
        {
            using (MySqlConnection conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string query = "SELECT PlayerName, Score, TimeSurvived FROM HighScores ORDER BY Score DESC LIMIT @n";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@n", count);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new ScoreRecord(
                            reader.GetString("PlayerName"),
                            reader.GetInt32("Score"),
                            reader.GetString("TimeSurvived")
                        ));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n [DB] Read error: {ex.Message}");
            Console.ResetColor();
        }
        return results;
    }
}