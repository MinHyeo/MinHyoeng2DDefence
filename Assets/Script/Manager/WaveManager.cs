using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    private string _tempStageId = "stage_01";
    private List<WaveData> _waveData = new List<WaveData>();

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
        var stageData = GameDataManager.Instance.GetStageData(_tempStageId);
        string[] waveIdList = stageData.WaveId.Split(',');


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
        GameObjectManager.Instance.CreateEnemyOjbect(enemyId, transform);
    }
}
