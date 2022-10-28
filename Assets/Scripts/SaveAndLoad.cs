using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad
{
    public void SaveBestScore(int bestScore)
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
    }
    public int LoadBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            return PlayerPrefs.GetInt("BestScore");
        }
        return 0;
    }
    public void SaveMoney(int countMoney)
    {
        PlayerPrefs.SetInt("CountMoney", countMoney);
    }
    public int LoadMoney()
    {
        if (PlayerPrefs.HasKey("CountMoney"))
        {
            return PlayerPrefs.GetInt("CountMoney");
        }
        return 0;
    }
}
