using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad
{
    private static BinaryFormatter binaryFormatter;
    private static GameData gameData;
    private static string filePath;
    static SaveAndLoad()
    {
        var directory = Application.persistentDataPath + "/saves";
        filePath = directory + "/GameSave.save";

        Directory.CreateDirectory(directory);

        binaryFormatter = new BinaryFormatter();
        gameData = InitData();
        Debug.Log("Static Constructure");
    }
    private static GameData InitData()
    {
        GameData saveData = new GameData();
        if (File.Exists(filePath))
        {
            var file = File.Open(filePath, FileMode.Open);
            saveData = (GameData)binaryFormatter.Deserialize(file);
            Debug.Log(saveData.statesOfBackground);
            Debug.Log(saveData.bestScore);
        }
        else
        {
            Debug.Log("NotInit");
        }
        return saveData;
    }
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
    public void SaveMoneyBinary(int _money)
    {
        gameData.money = _money;
        var file = File.Create(filePath);
        binaryFormatter.Serialize(file, gameData);
        file.Close();
    }
    public int LoadMoneyBinary(int defaultValue)
    {
        if (File.Exists(filePath))
        {
            return gameData.money;
        }
        return defaultValue;
    }
    public void SaveBestScoreBinary(int _bestScore)
    {
        gameData.bestScore = _bestScore;
        var file = File.Create(filePath);
        binaryFormatter.Serialize(file, gameData);
        file.Close();
    }
    public int LoadBestScoreBinary(int defaultValue)
    {
        if (File.Exists(filePath))
        {
            return gameData.bestScore;
        }
        return defaultValue;
    }
    public void SaveBackgroundsBinary(bool[] backGroundsState)
    {
        gameData.statesOfBackground = backGroundsState;
        var file = File.Create(filePath);
        binaryFormatter.Serialize(file, gameData);
        file.Close();
    }
    public bool[] LoadBackgroundsBinary(bool[] defaultValue)
    {
        if (File.Exists(filePath))
        {
            Debug.Log("Exist");
            return gameData.statesOfBackground;
        }
        Debug.Log("NotExist");
        return defaultValue;
    }
    public void SaveCurrentBackgroundsBinary(int currentBackGround)
    {
        gameData.indexCurrentBackGround = currentBackGround;
        var file = File.Create(filePath);
        binaryFormatter.Serialize(file, gameData);
        file.Close();
    }
    public int LoadCurrentBackgroundsBinary(int defaultValue)
    {
        if (File.Exists(filePath))
        {
            Debug.Log("Exist");
            return gameData.indexCurrentBackGround;
        }
        Debug.Log("NotExist");
        return defaultValue;
    }
}
[Serializable]
public class GameData
{
    public int money;
    public int bestScore;
    public bool[] statesOfBackground = new bool[0];
    public int indexCurrentBackGround;
}
