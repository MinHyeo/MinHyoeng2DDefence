using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [Header("스테이지 타일맵 전부 여기 담기")]
    [SerializeField] private List<GameObject> _stageTilemapList;
    //[SerializeField] private GameObject _stage1TileMap;
    //[SerializeField] private GameObject _stage2TileMap;
    //[SerializeField] private GameObject _stage3TileMap;

    private LifeManager _lifeManager;
    private MeatManager _meatManager;
    private StageData _stageData;
    private Tilemap _currentTilemap;

    private int _stageIndex = 1;
    public int StageIndex => _stageIndex;
    private bool _isFaild = false;
    private int _cardDrawPrice = 50;
    

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        //StartCoroutine(CoLoadStageData());
        //LoadStageData();
    }

    #region Stage 관련 코드
    public void ClearStage()
    {
        if (_isFaild)
            return;

        Debug.Log("게임 클리어");
        UIManager.Instance.OpenUI(UIRootType.PopupUI, UIType.ClearPopupUI);
    }

    public void FailStage()
    {
        // wave 종료
        string waveIds = _stageData.WaveId;
        WaveManager.Instance.StopCoroutineSpawnWave(waveIds);

        // 실패 UI 띄위기
        UIManager.Instance.OpenUI(UIRootType.PopupUI, UIType.FailPopupUI);

        _stageData = null;
    }

    public void StartStage(int stageIndex)
    {
        _stageIndex = stageIndex;
        _isFaild = false;

        // Stage 관리 Manager들 생성
        _lifeManager = new LifeManager();
        _meatManager = new MeatManager();

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

    //private IEnumerator CoLoadStageData(string stageId)
    //{
    //    yield return null;
    //    //LoadStageData();
    //}

    private void LoadStageData(string stageId)
    {
        _stageData = GameDataManager.Instance.GetData<StageData>(stageId);

        if (_stageData == null)
            return;

        string wavdIds = _stageData.WaveId;
        WaveManager.Instance.LoadWaveData(wavdIds);

        _lifeManager.SetLifeCount(_stageData.MaxLife);
        //LifeManager.Instance.SetLifeCount(_stageData.MaxLife);

        //MeatManager.Instance.SetStartMeatCount(_stageData.StartMeatCount);
        IncreaseMeatCount(_stageData.StartMeatCount);
    }

    public Tilemap GetTilemap()
    {
        return _currentTilemap;
    }

    public void ResetStage()
    {
        // stage 관리 Manager들 제거
        _meatManager.UnBindOndMeatCountUpdate();
        _meatManager = null;
        _lifeManager.UnBindLifeIconUpdate();
        _lifeManager = null;

        // 타일맵 off
        _currentTilemap.gameObject.SetActive(false);
        // wayPoint 초기화
        WaypointManager.Instance.ResetWaypoint();
        // 설치된 타워 삭제
        TowerManager.Instance.DestroyAllTower();
        // 현재 존재하는 몬스터 제거
        GameObjectManager.Instance.RequestDestroyAllEnemyObject();
    }
    #endregion

    #region Meat 재화 관련 코드
    public void BindOndMeatCountUpdate(Action<int> callback)
    {
        _meatManager.BindOndMeatCountUpdate(callback);
    }

    public void IncreaseMeatCount(int increaseCount)
    {
        _meatManager.UpdateMeatCount(increaseCount);
    }

    public void DecreaseMeatCount(int decreaseCount)
    {
        int meatAmount = decreaseCount * -1;
        _meatManager.UpdateMeatCount(meatAmount);
    }

    public bool CanDrawCard()
    {
        var meatCount = _meatManager.GetMeatCount();
        return meatCount >= _cardDrawPrice;
    }
    #endregion

    #region Life 관련 코드
    public void BindOnLifeIconUpdate(Action<int> callback)
    {
        _lifeManager.BindLifeIconUpdate(callback);
    }

    public void DecreaseLifeCount()
    {
        if (_isFaild)
            return;

        _isFaild = _lifeManager.DecreaseLifeAndCheckDeath();

        if (_isFaild)
        {
            FailStage();
        }
    }
    #endregion
}
