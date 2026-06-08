using System;
class ScoreRecord
{
    public string PlayerName {
        get; private set; 
    }   
    public int Score 
    {
        get; private set; 
    }  
    public string TimeSurvived
    {
        get; private set; 
    }   
    public DateTime PlayedAt 
    {
        get; private set; 
    }
    public ScoreRecord(string playerName, int score, string timeSurvived)
    {
        PlayerName = playerName;
        Score = score;
        TimeSurvived = timeSurvived;
        PlayedAt = DateTime.Now;
    }
    public override string ToString()
    {
        return $"{PlayerName} | Score: {Score} | Time: {TimeSurvived} | Played: {PlayedAt:HH:mm:ss}";
    }
}