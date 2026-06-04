using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyCardAction : ICardAction
{
    // 클릭 했을 때
    public void OnClick(string towerId) 
    {
        UIManager.Instance.OpenCardInfoPopup(towerId);
    }

    // 드래그 시작/끝
    public void OnDrag(PointerEventData eventData, string towerId) { }
}