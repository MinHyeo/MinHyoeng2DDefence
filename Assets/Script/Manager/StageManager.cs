using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] private List<GameObject> _stageTilemapList;
    [SerializeField] private GameObject _stage1TileMap;
    [SerializeField] private GameObject _stage2TileMap;
    [SerializeField] private GameObject _stage3TileMap;

    private StageData _stageData;
    private Tilemap _currentTilemap;

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
        _currentTilemap = _stageTilemapList[stageIndex].GetComponent<Tilemap>();
        _currentTilemap.gameObject.SetActive(true);
        WaypointManager.Instance.SetWayPoint();

        // 스테이지 데이터 불러오기
        string stageId = "stage_0" + (stageIndex + 1);
        LoadStageData(stageId);
    }
    
    // 데이터 세팅
    // MainUI 호출
    // 각 UI에 초기값 셋팅 + 이벤트 구독

    private IEnumerator CoLoadStageData(string stageId)
    {
        yield return null;
        //LoadStageData();
    }

    private void LoadStageData(string stageId)
    {
        _stageData = GameDataManager.Instance.GetData<StageData>(stageId);

        if (_stageData == null)
            return;

        string[] waveIdList = _stageData.WaveId.Split(',');
        WaveManager.Instance.LoadWaveData(waveIdList);

        LifeManager.Instance.SetLifeCount(_stageData.MaxLife);

        MeatManager.Instance.SetStartMeatCount(_stageData.StartMeatCount);
    }

    public Tilemap GetTilemap()
    {
        return _currentTilemap;
    }
}
