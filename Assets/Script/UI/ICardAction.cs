

using UnityEngine.EventSystems;

public interface ICardAction
{
    // 클릭 했을 때
    public void OnClick(string towerId);

    // 드래그 시작/끝
    public void OnDrag(PointerEventData eventData, string towerId);
}