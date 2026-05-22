using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBatchObject : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    private string _towerId;
    private Action<bool> _onPlacementResult;

    public void InitBatchObject(string towerId, Action<bool> callback)
    {
        _towerId = towerId;
        _onPlacementResult = callback;
    }

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
            TowerManager.Instance.SpawnTower(_towerId, transform.position);
            gameObject.SetActive(false);

            _onPlacementResult?.Invoke(true);
        }
        else
        {
            gameObject.SetActive(false);
            _onPlacementResult?.Invoke(false);
        }
    }
}
