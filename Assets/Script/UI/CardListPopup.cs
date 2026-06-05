using System.Collections.Generic;
using UnityEngine;

public class CardListPopup : UIBase
{
    [Header("끄기 버튼")]
    [SerializeField] private UIButton _exitButton;
    [Header("카드 관련 변수")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardRootTransform;

    private Dictionary<string, CardUI> _createdCardUIList = new Dictionary<string, CardUI>();
    private int _cardInstanceId = 0;

    private void OnEnable()
    {
        _exitButton.BindOnClickButtonEvent(ExitPopup);

        AddTowerCard();
    }

    private void ExitPopup()
    {
        UIManager.Instance.CloseUI(UIRootType.PopupUI, UIType.CardListPopupUI);
    }

    private void AddTowerCard()
    {
        var allTowerIds = GameDataManager.Instance.GetAllTowerIds();
        foreach(var towerId in allTowerIds)
        {
            if(_createdCardUIList.ContainsKey(towerId) == false)
            {
                var cardObject = Instantiate(_cardPrefab, _cardRootTransform);
                var cardComponent = cardObject.GetComponent<CardUI>();

                ICardAction iCardAction = new LobbyCardAction();
                cardComponent.InitCardUI(_cardInstanceId++, towerId, iCardAction);
                _createdCardUIList.Add(towerId, cardComponent);
            }
        }
    }
}
