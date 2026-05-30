using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private string[] GetWaveIds(string waveId)
    {
        return waveId.Split(',');
    }

    public void LoadWaveData(string waveIds)
    {
        var waveIdList = GetWaveIds(waveIds);
        foreach (string waveId in waveIdList)
        {
            WaveData waveData = GameDataManager.Instance.GetData<WaveData>(waveId.Trim());
            StartCoroutine(CoSpawnWave(waveData));
        }
    }

    private IEnumerator CoSpawnWave(WaveData waveData)
    {
        yield return new WaitForSeconds(waveData.PreDelay);

        var waitTime = new WaitForSeconds(waveData.Interval);
        for (int i = 0; i < waveData.Count; i++)
        {
            SpawnEnemy(waveData.EnemyId, waveData);
            yield return waitTime;
        }
    }

    private void SpawnEnemy(string enemyId, WaveData waveData) 
    {
        int waveGroup = waveData.WaveGroup;
        Vector3 spawnTransform = WaypointManager.Instance.GetWaypoints(waveGroup)[0];
        Debug.Log($"{waveGroup} : {enemyId} 몬스터 생성");
        GameObjectManager.Instance.CreateEnemyOjbect(enemyId, spawnTransform, waveGroup);
    }

    public void StopCoroutineSpawnWave(string waveIds)
    {
        var waveIdList = GetWaveIds(waveIds);
        foreach (string waveId in waveIdList)
        {
            WaveData waveData = GameDataManager.Instance.GetData<WaveData>(waveId.Trim());
            StopCoroutine(CoSpawnWave(waveData));
        }
    }
}
