using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    private string _stageId = "stage_01";
    private StageData _stageData;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(CoLoadStageData());
        //LoadStageData();
    }
    
    // 데이터 세팅
    // MainUI 호출
    // 각 UI에 초기값 셋팅 + 이벤트 구독

    private IEnumerator CoLoadStageData()
    {
        yield return null;
        LoadStageData();
    }

    private void LoadStageData()
    {
        _stageData = GameDataManager.Instance.GetStageData(_stageId);

        // MAIN UI 호출

        if (_stageData == null)
            return;

        string[] waveIdList = _stageData.WaveId.Split(',');
        WaveManager.Instance.LoadWaveData(waveIdList);

        LifeManager.Instance.SetLifeCount(_stageData.MaxLife);

        MeatManager.Instance.SetStartMeatCount(_stageData.StartMeatCount);
    }
}
