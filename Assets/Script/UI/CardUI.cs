using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text _towerNameText;
    [SerializeField] private Image _towerIconImage;

    private int _instanceId;
    private string _towerId;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject towerPrefab = Resources.Load<GameObject>(_towerId);
            GameObject towerObject = Instantiate(towerPrefab);
            towerObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            towerObject.transform.position = new Vector3(towerObject.transform.position.x, towerObject.transform.position.y, 0);
            towerObject.GetComponent<TowerBatchObject>().InitBatchObject(_towerId, HandlePlacementResult);

            eventData.pointerPress = towerObject;
            eventData.pointerDrag = towerObject;
            ExecuteEvents.Execute(towerObject, eventData, ExecuteEvents.pointerDownHandler);

            gameObject.SetActive(false);
        }
    }

    public void InitCardUI(int instanceId, string towerId)
    {
        _instanceId = instanceId;
        _towerId = towerId;

        SetCardUI();
    }

    private void SetCardUI()
    {
        EntityData towerData = GameDataManager.Instance.GetEntityData(_towerId);
        _towerNameText.text = towerData.Name;
        ResourceManager.Instance.LoadSprite(towerData.IconPath, (sprite) =>
        {
            if (sprite == null)
                return;
            _towerIconImage.sprite = sprite;
        });
    }

    private void HandlePlacementResult(bool isSurccess)
    {
        if (isSurccess)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
