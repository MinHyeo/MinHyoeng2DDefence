using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardUI : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [Header("카드 정보 관련 변수")]
    [SerializeField] private TextMeshProUGUI _towerNameText;
    [SerializeField] private Image _towerIconImage;

    private int _instanceId;
    private string _towerId;

    // 카드가 있는 상황에 따라 다른 행동 실행
    private ICardAction _cardAction;

    public void InitCardUI(int instanceId, string towerId, ICardAction iCardAction)
    {
        _instanceId = instanceId;
        _towerId = towerId;

        _cardAction = iCardAction;

        SetCardUI();
    }

    private void SetCardUI()
    {
        var towerData = GameDataManager.Instance.GetData<EntityData>(_towerId);
        _towerNameText.text = towerData.Name;
        ResourceManager.Instance.LoadSprite(towerData.IconPath, (sprite) =>
        {
            if (sprite == null)
                return;
            _towerIconImage.sprite = sprite;
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _cardAction.OnClick(_towerId);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _cardAction.OnDrag(eventData, _towerId);
        }
    }
}
