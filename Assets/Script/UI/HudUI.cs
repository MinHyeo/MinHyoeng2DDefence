using System.Collections.Generic;
using UnityEngine;

public class HudUI : UIBase
{
    [SerializeField] private GameObject _hudSlotPrefab;
    [SerializeField] private Transform _slotRootTransform;

    private Dictionary<int, HudSlotUI> _hudSlotList = new Dictionary<int, HudSlotUI>();

    public void AddHudSlot(int instanceId, Transform targetTransform)
    {
        CreateHudSlot(instanceId, targetTransform);
    }

    private void CreateHudSlot(int instanceId, Transform targetTransform)
    {
        var gObj = Instantiate(_hudSlotPrefab, _slotRootTransform);
        if (gObj == null)
            return;

        var slotComponent = gObj.GetComponent<HudSlotUI>();
        if (slotComponent == null)
            return;

        slotComponent.InitSlot(instanceId, targetTransform);
        _hudSlotList.Add(instanceId, slotComponent);
    }

    public void RemoveHudSlot(int instanceId)
    {
        if (_hudSlotList.ContainsKey(instanceId) == true)
        {
            var slot = _hudSlotList[instanceId];
            Destroy(slot.gameObject);

            _hudSlotList.Remove(instanceId);
        }
    }
}
