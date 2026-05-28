using UnityEngine;

public class LobbyUI : UIBase
{
    [SerializeField] private UIButton _startButton;

    private void OnEnable()
    {
        _startButton.BindOnClickButtonEvent(OnStartSelectStage);
    }

    private void OnStartSelectStage()
    {
        // 1. Lobby UI 닫기
        UIManager.Instance.CloseUI(UIRootType.ContentUI, UIType.LobbyUI);

        // 2. StageSelectUI 열기
        UIManager.Instance.OpenUI(UIRootType.ContentUI, UIType.StageSelectUI);
    }
}
