using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    private Dictionary<string, EntityData> _entityDataList = new Dictionary<string, EntityData>();
    private Dictionary<string, TowerData> _towerDataList = new Dictionary<string, TowerData>();
    private Dictionary<string, EnemyData> _enemyDataList = new Dictionary<string, EnemyData>();

    private void Awake()
    {
        Instance = this;
        LoadAllData();
    }

    [Serializable]
    private class SerializationWrapper<T>
    {
        public List<T> items; // JSON 파일의 루트 키 이름이 "items"여야 함
    }

    public void LoadAllData()
    {
        LoadEntityData("C:/OZ_Project/MinHyeong2DProject/JsonConverter/JsonOutput/Entity.json");
        LoadTowerData("C:/OZ_Project/MinHyeong2DProject/JsonConverter/JsonOutput/Tower.json");
        LoadEnemyData("C:/OZ_Project/MinHyeong2DProject/JsonConverter/JsonOutput/Enemy.json");
    }

    private Dictionary<string, T> LoadData<T>(string jsonPath) where T : GameDataBase
    {
        if (!File.Exists(jsonPath))
        {
            Debug.LogError($"[Error] 파일을 찾을 수 없습니다: {jsonPath}");
            return new Dictionary<string, T>();
        }
        try
        {
            string jsonString = File.ReadAllText(jsonPath);
            string wrappedJson = "{\"items\":" + jsonString + "}";
            SerializationWrapper<T> wrapper = JsonUtility.FromJson<SerializationWrapper<T>>(wrappedJson);

            if (wrapper != null && wrapper.items != null)
            {
                Debug.Log($"{typeof(T).Name} 데이터를 {wrapper.items.Count}개 로드했습니다.");
                return wrapper.items.ToDictionary(item => item.Id);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[{typeof(T).Name} JSON 로드 오류] {ex.Message}");
        }

        return new Dictionary<string, T>();
    }

    private void LoadEntityData(string jsonPath)
    {
        _entityDataList = LoadData<EntityData>(jsonPath);
    }

    private void LoadTowerData(string jsonPath)
    {
        _towerDataList = LoadData<TowerData>(jsonPath);
    }
    private void LoadEnemyData(string jsonPath)
    {
        _enemyDataList = LoadData<EnemyData>(jsonPath);
    }

    public EntityData GetEntityData(string id)
    {
        if (_entityDataList == null || string.IsNullOrEmpty(id))
            return null;

        return _entityDataList.TryGetValue(id, out var data) ? data : null;
    }

    public TowerData GetTowerData(string id)
    {
        if (_towerDataList == null || string.IsNullOrEmpty(id))
            return null;

        return _towerDataList.TryGetValue(id, out var data) ? data : null;
    }

    public EnemyData GetEnemyData(string id)
    {
        if (_enemyDataList == null || string.IsNullOrEmpty(id))
            return null;

        return _enemyDataList.TryGetValue(id, out var data) ? data : null;
    }

    public List<string> GetAllTowerIds()
    {
        if (_towerDataList == null)
            return null;

        return _towerDataList.Keys.ToList();
    }

    public List<string> GetAllEnemyIds()
    {
        if (_enemyDataList == null)
            return null;

        return _enemyDataList.Keys.ToList();
    }
}
