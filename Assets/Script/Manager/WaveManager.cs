using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    private StageData _stageData;
    private string _tempStageId = "stage_01";
    private List<WaveData> _waveData = new List<WaveData>();
    private int _currentLifeCount;

    private event Action<int> _onDecreaseLife;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadWaveData();
    }

    private void LoadWaveData()
    {
        _stageData = GameDataManager.Instance.GetStageData(_tempStageId);
        if (_stageData == null)
            return;
        string[] waveIdList = _stageData.WaveId.Split(',');
        _currentLifeCount = _stageData.MaxLife;

        foreach (string waveId in waveIdList)
        {
            WaveData waveData = GameDataManager.Instance.GetWaveData(waveId.Trim());
            StartCoroutine(CoSpawnWave(waveData));
        }
    }

    private IEnumerator CoSpawnWave(WaveData waveData)
    {
        yield return new WaitForSeconds(waveData.PreDelay);

        var waitTime = new WaitForSeconds(waveData.Interval);
        for (int i = 0; i < waveData.Count; i++)
        {
            SpawnEnemy(waveData.EnemyId);
            yield return waitTime;
        }
    }

    private void SpawnEnemy(string enemyId) 
    {
        Vector3 spawnTransform = WaypointManager.Instance.GetWaypoints()[0];
        GameObjectManager.Instance.CreateEnemyOjbect(enemyId, spawnTransform);
    }

    public void DecreaseLife()
    {
        _currentLifeCount -= 1;
        if(_currentLifeCount < 0)
        {
            Debug.Log("게임 클리어 실패");
            return;
        }
        _onDecreaseLife?.Invoke(_currentLifeCount);
    }

    public void SubscribeLifeDecrease(Action<int> callback)
    {
        _onDecreaseLife += callback;
    }

    public int GetStageMaxLifeCount()
    {
        return _stageData.MaxLife;
    }
}
