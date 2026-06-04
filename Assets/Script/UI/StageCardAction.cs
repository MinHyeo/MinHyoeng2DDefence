using UnityEngine;
using UnityEngine.EventSystems;

public class StageCardAction : MonoBehaviour, ICardAction
{
    // 클릭 했을 때
    public void OnClick(string towerId) { }

    // 드래그 시작/끝
    public void OnDrag(PointerEventData eventData, string towerId)
    {
        GameObject towerPrefab = Resources.Load<GameObject>(towerId);
        GameObject towerObject = Instantiate(towerPrefab);
        towerObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        towerObject.transform.position = new Vector3(towerObject.transform.position.x, towerObject.transform.position.y, 0);
        towerObject.GetComponent<TowerBatchObject>().InitBatchObject(towerId, HandlePlacementResult);

        eventData.pointerPress = towerObject;
        eventData.pointerDrag = towerObject;
        ExecuteEvents.Execute(towerObject, eventData, ExecuteEvents.pointerDownHandler);

        gameObject.SetActive(false);
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