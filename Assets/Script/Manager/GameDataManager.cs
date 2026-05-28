using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    private Dictionary<string, object> _dataList = new Dictionary<string, object>();
    private Dictionary<string, EntityData> _entityDataList = new Dictionary<string, EntityData>();
    private Dictionary<string, TowerData> _towerDataList = new Dictionary<string, TowerData>();
    private Dictionary<string, EnemyData> _enemyDataList = new Dictionary<string, EnemyData>();
    private Dictionary<string, StageData> _stageDataList = new Dictionary<string, StageData>();
    private Dictionary<string, WaveData> _waveDataList = new Dictionary<string, WaveData>();

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
        LoadData<EntityData>("Entity");
        LoadData<TowerData>("Tower");
        LoadData<EnemyData>("Enemy");
        LoadData<StageData>("Stage");
        LoadData<WaveData>("Wave");
    }

    private Dictionary<string, T> LoadJsonData<T>(string jsonPath) where T : GameDataBase
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
                // 데이터 검증 및 null 제거를 안전하게 처리
                var validItems = wrapper.items
                    .Where(item => item != null && !string.IsNullOrEmpty(item.Id))
                    .ToList();

                // 데이터가 누락되었는지 확인 로그 출력
                if (validItems.Count != wrapper.items.Count)
                {
                    Debug.LogWarning($"[경고] {typeof(T).Name} 데이터 중 일부가 null이거나 Id가 비어있어 제외되었습니다. (원본: {wrapper.items.Count}개 -> 정제 후: {validItems.Count}개)");
                }

                Debug.Log($"{typeof(T).Name} 데이터를 {validItems.Count}개 로드했습니다.");

                // 안전해진 리스트로 Dictionary 생성
                return validItems.ToDictionary(item => item.Id);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[{typeof(T).Name} JSON 로드 오류] {ex.Message}");
        }

        return new Dictionary<string, T>();
    }

    private void LoadData<T>(string path) where T : GameDataBase
    {
        string jsonPath = $"C:/OZ_Project/MinHyeong2DProject/JsonConverter/JsonOutput/{path}.json";

        if(_dataList.ContainsKey(path) == false)
        {
            path = path + "Data";
            _dataList.Add(path, new Dictionary<string, T>());
        }

        _dataList[path] = LoadJsonData<T>(jsonPath);
    }

    public T GetData<T>(string id) where T : GameDataBase
    {
        string type = typeof(T).FullName;
        object dictObj = null;

        if (_dataList.TryGetValue(type, out dictObj))
        {
            var dict = dictObj as Dictionary<string, T>;
            return dict[id];
        }
        return null;
    }

    private List<string> GetAllId<T>() where T : GameDataBase
    {
        string type = typeof(T).FullName;
        object dictObj = null;
        if (_dataList.TryGetValue(type, out dictObj))
        {
            var dict = dictObj as Dictionary<string, T>;
            return dict.Keys.ToList();
        }
        return null;
    }

    public List<string> GetAllTowerIds()
    {
        return GetAllId<TowerData>();
    }

    public List<string> GetAllEnemyIds()
    {
        return GetAllId<EnemyData>();
    }

    public List<string> GetAllStageIds()
    {
        return GetAllId<StageData>();
    }
}
