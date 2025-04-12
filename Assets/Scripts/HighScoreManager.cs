using UnityEngine;
using System.Collections.Generic;

public static class HighscoreManager
{
    private const int maxScores = 5;
    private const string key = "Highscore_";

    public static void AddScore(float timeInSeconds)
    {
        List<float> scores = GetScores();
        scores.Add(timeInSeconds);
        scores.Sort(); // menor tempo primeiro
        if (scores.Count > maxScores)
            scores.RemoveAt(scores.Count - 1);

        for (int i = 0; i < scores.Count; i++)
            PlayerPrefs.SetFloat(key + i, scores[i]);

        PlayerPrefs.Save();
    }

    public static List<float> GetScores()
    {
        List<float> scores = new List<float>();
        for (int i = 0; i < maxScores; i++)
        {
            if (PlayerPrefs.HasKey(key + i))
                scores.Add(PlayerPrefs.GetFloat(key + i));
        }
        return scores;
    }
}
