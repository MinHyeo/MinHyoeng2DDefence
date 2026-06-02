using System.Collections.Generic;
using UnityEngine;

public class CardListPopup : UIBase
{
    [SerializeField] private UIButton _exitButton;
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
        UIManager.Instance.CloseUI(UIRootType.PopupUI, UIType.CardListPopup);
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

                cardComponent.InitCardUI(_cardInstanceId++, towerId);
                _createdCardUIList.Add(towerId, cardComponent);
            }
        }
    }
}
