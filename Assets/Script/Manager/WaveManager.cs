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

    public void LoadWaveData(string[] waveIdList)
    {
        foreach(string waveId in waveIdList)
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
}
