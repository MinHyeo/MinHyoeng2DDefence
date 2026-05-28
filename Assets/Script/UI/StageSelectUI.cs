using System.Collections.Generic;
using UnityEngine;

public class StageSelectUI : UIBase
{
    [SerializeField] private UIButton _stageButton;

    [SerializeField] private GameObject _stageButtonPrefab;

    private void OnEnable()
    {
        //CreateStageButton();
        Debug.Log("start 버튼 등록");
        _stageButton.BindOnClickButtonEvent(OnSelectedStage);
    }

    private void CreateStageButton()
    {
        List<string> stageIdList = GameDataManager.Instance.GetAllStageIds();

        int listCount = stageIdList.Count;
        for (int i = 1; i <= listCount; i++)
        {
            Instantiate(_stageButtonPrefab, this.transform);
        }
    }

    private void OnSelectedStage()
    {
        Debug.Log("게임 시작");
        // 1. 현재 UI 종료
        UIManager.Instance.CloseUI(UIRootType.ContentUI, UIType.StageSelectUI);

        // 2. StageManager에게 stage 시작하라고 명령
        StageManager.Instance.StartStage(1);
    }
}
