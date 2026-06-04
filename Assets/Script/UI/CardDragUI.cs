using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragUI : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
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
