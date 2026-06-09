using System;
using UnityEngine;

public class PlayerScore
{
    private static PlayerScore instance;
    private static int _currentScore;

    public static event Action<int> onScoreChange;

    public static PlayerScore GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerScore();
        }
        return instance;
    }
    public void ResetScore()
    {
        _currentScore = 0;
        onScoreChange?.Invoke(_currentScore);
    }
    public int GetCurrentScore()
    {
        return _currentScore;
    }
    public void AddScore(int amount)
    {
        int absScore = Mathf.Abs(amount);
        _currentScore += absScore;
        onScoreChange?.Invoke(_currentScore);
    }

}
