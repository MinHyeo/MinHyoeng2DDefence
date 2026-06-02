using UnityEngine;

public class ClearPopupUI : UIBase
{
    [SerializeField] private UIButton _nextStageButton;
    [SerializeField] private UIButton _lobbyButton;

    private void OnEnable()
    {
        _nextStageButton.BindOnClickButtonEvent(StartNextStage);
        _lobbyButton.BindOnClickButtonEvent(ReturnLobby);
    }

    private void OnDisable()
    {
        _nextStageButton.UnBindOnClickButtonEvent(StartNextStage);
        _lobbyButton.UnBindOnClickButtonEvent(ReturnLobby);
    }

    private void StartNextStage()
    {
        // Stage 리셋 및 UI 끄기
        // closeall이 있으면 한줄로 끝남
        UIManager.Instance.CloseUI(UIRootType.MainUI, UIType.MainUI);
        UIManager.Instance.CloseUI(UIRootType.MainUI, UIType.HudUI);
        StageManager.Instance.ResetStage();

        // Stgae 다시 시작
        int nextStageIndex = StageManager.Instance.StageIndex + 1;
        StageManager.Instance.StartStage(nextStageIndex);

        // 4. ClearPopupUI 끄기
        UIManager.Instance.CloseUI(UIRootType.PopupUI, UIType.ClearPopupUI);
    }

    private void ReturnLobby()
    {
        // 1. Main UI/Hub UI 끄기
        UIManager.Instance.CloseUI(UIRootType.MainUI, UIType.MainUI);
        UIManager.Instance.CloseUI(UIRootType.MainUI, UIType.HudUI);

        // 2. 스테이지 초기화
        StageManager.Instance.ResetStage();

        // 3. 로비 UI 켜기
        UIManager.Instance.OpenUI(UIRootType.ContentUI, UIType.LobbyUI);

        // 4. ClearPopupUI 끄기
        UIManager.Instance.CloseUI(UIRootType.PopupUI, UIType.ClearPopupUI);
    }
}
