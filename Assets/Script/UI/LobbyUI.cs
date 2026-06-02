using UnityEngine;

public class LobbyUI : UIBase
{
    [SerializeField] private UIButton _startButton;
    [SerializeField] private UIButton _cardListButton;

    private void OnEnable()
    {
        _startButton.BindOnClickButtonEvent(OpenStartSelectStage);
        _cardListButton.BindOnClickButtonEvent(OpenCardList);
    }

    private void OpenStartSelectStage()
    {
        // 1. Lobby UI 닫기
        UIManager.Instance.CloseUI(UIRootType.ContentUI, UIType.LobbyUI);

        // 2. StageSelectUI 열기
        UIManager.Instance.OpenUI(UIRootType.ContentUI, UIType.StageSelectUI);
    }

    private void OpenCardList()
    {
        UIManager.Instance.OpenUI(UIRootType.PopupUI, UIType.CardListPopup);
    }
}
