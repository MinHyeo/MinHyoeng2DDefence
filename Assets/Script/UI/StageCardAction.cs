using UnityEngine;
using UnityEngine.EventSystems;

public class StageCardAction : MonoBehaviour, ICardAction
{
    // 클릭 했을 때
    public void OnClick(string towerId) { }

    // 드래그 시작/끝
    public void OnDrag(PointerEventData eventData, string towerId)
    {
        // 1. 오브젝트 생성
        GameObject towerPrefab = Resources.Load<GameObject>(towerId);
        GameObject towerObject = Instantiate(towerPrefab);
        // 2. 오브젝트 위치를 마우스 위치 이동
        towerObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        towerObject.transform.position = new Vector3(towerObject.transform.position.x, towerObject.transform.position.y, 0);
        towerObject.GetComponent<TowerBatchObject>().InitBatchObject(towerId, HandlePlacementResult);

        // 3. 이 오브젝트가 클릭된 상태다. eventData에게 전달
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