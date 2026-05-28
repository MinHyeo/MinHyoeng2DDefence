using System;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectUI : UIBase
{
    [SerializeField] private List<UIButton> _stageButtonList;
    [SerializeField] private UIButton _stageButton;

    [SerializeField] private GameObject _stageButtonPrefab;

    private void OnEnable()
    {
        //CreateStageButton();
        Debug.Log("start 버튼 등록");
        //_stageButton.BindOnClickButtonEvent(OnSelectedStage);

        // TODO : 최적화 작업 할때 해야 함 (UIButton.cs에서 내용 확인)
        // TODO :따로 클래스를 만들어서 각자 인덱스를 지역변수로 가지고 있고 그걸 가지고 와서 button을 초기화하는 방식으로 구현해야할 듯
        // 람다식을 사용하는 과정에서 외부변수를 사용하면 박싱/언박싱이 발생해서 자원이 소모됨
        // 캡쳐 / 클로저 문제 발생

        for (int i = 0; i < _stageButtonList.Count; i++)
        {
            int temp = i;
            _stageButtonList[i].BindOnClickButtonEvent(
                () =>
                {
                    OnSelectedStage(temp);
                }
            );
        }
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

    private void OnSelectedStage(int index)
    {
        Debug.Log("게임 시작");
        // 1. 현재 UI 종료
        UIManager.Instance.CloseUI(UIRootType.ContentUI, UIType.StageSelectUI);

        // 2. StageManager에게 stage 시작하라고 명령
        StageManager.Instance.StartStage(index);
    }

    private void OnDisable()
    {
        //_stageButton.UnBindOnClickButtonEvent(OnSelectedStage);
    }
}
