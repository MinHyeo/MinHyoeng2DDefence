using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBatchObject : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    [SerializeField] private string towerId = "tower_01";

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("배치 시도");
        if (TowerManager.Instance.CanPlaceTower(transform.position))
        {
            TowerManager.Instance.SpawnTower(towerId, transform.position);
            gameObject.SetActive(false);
        }
    }
}
