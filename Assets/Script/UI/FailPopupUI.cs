using UnityEngine;

public class FailPopupUI : UIBase
{
    [SerializeField] private UIButton _retryButton;
    [SerializeField] private UIButton _lobbyButton;

    private void OnEnable()
    {
        _lobbyButton.BindOnClickButtonEvent(ReturnLobby);
    }

    private void ReturnLobby()
    {
        // 1. Main UI/Hub UI 끄기
        UIManager.Instance.CloseUI(UIRootType.MainUI, UIType.MainUI);
        UIManager.Instance.CloseUI(UIRootType.MainUI, UIType.HudUI);

        // 2. 타일맵 끄기
        StageManager.Instance.ResetStage();
        StageManager.Instance.GetTilemap().gameObject.SetActive(false);

        // 3. 로비 UI 켜기
        UIManager.Instance.OpenUI(UIRootType.ContentUI, UIType.LobbyUI);

        // 4. failPopupUI 끄기
        UIManager.Instance.CloseUI(UIRootType.PopupUI, UIType.FailPopupUI);
    }
}
