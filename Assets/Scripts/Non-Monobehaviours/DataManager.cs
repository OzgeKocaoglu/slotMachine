using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IDataManager
{
    List<ProbabilityTableVariable> Init();
    void SaveData(int[] _spins, int spinCount);
    public void GetData(out int spinCount, out int[] _spins);
}

public class DataManager : IDataManager
{
    PlayerData data;
    string json, path;

    public DataManager()
    {
        data = new PlayerData();
        path = "/PlayerData.json";
    }

    public List<ProbabilityTableVariable> Init()
    {
        return Utils.GetAllInstances<ProbabilityTableVariable>();
    }

    public void SaveData(int[] _spins, int spinCount)
    {
        data.spins = _spins;
        data.totalSpinCount = spinCount;
        json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + path, json);
    }

    public void GetData(out int spinCount, out int[] _spins)
    {
        json = File.ReadAllText(Application.persistentDataPath + path);
        Debug.Log(json);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        spinCount = data.totalSpinCount;
        _spins = data.spins;
    }
}
