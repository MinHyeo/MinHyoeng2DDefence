using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] private GameObject _stage1TileMap;
    [SerializeField] private GameObject _stage2TileMap;
    [SerializeField] private GameObject _stage3TileMap;

    private StageData _stageData;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        //StartCoroutine(CoLoadStageData());
        //LoadStageData();
    }

    public void StartStage(int stageIndex)
    {
        // 메인 UI 열기
        UIManager.Instance.OpenUI(UIRootType.MainUI, UIType.MainUI);

        // TileMap 설정
        _stage1TileMap.SetActive(true);

        // 스테이지 데이터 불러오기
        string stageId = "stage_" + stageIndex;
        LoadStageData(stageId);
    }
    
    // 데이터 세팅
    // MainUI 호출
    // 각 UI에 초기값 셋팅 + 이벤트 구독

    private IEnumerator CoLoadStageData()
    {
        yield return null;
        //LoadStageData();
    }

    private void LoadStageData(string stageId)
    {
        _stageData = GameDataManager.Instance.GetStageData(stageId);

        // MAIN UI 호출

        if (_stageData == null)
            return;

        string[] waveIdList = _stageData.WaveId.Split(',');
        WaveManager.Instance.LoadWaveData(waveIdList);

        LifeManager.Instance.SetLifeCount(_stageData.MaxLife);

        MeatManager.Instance.SetStartMeatCount(_stageData.StartMeatCount);
    }
}
