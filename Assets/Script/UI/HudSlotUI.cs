using UnityEngine;
using UnityEngine.UI;

public class HudSlotUI : MonoBehaviour
{
    private int _instanceId;
    private Transform _targetTransform;

    [SerializeField] private Slider _hpSilder;

    public void InitSlot(int instanceId, Transform targetTransform)
    {
        _instanceId = instanceId;
        _targetTransform = targetTransform;

        TryBindStatChangedEvent(targetTransform.gameObject);
    }

    private void TryBindStatChangedEvent(GameObject gObj)
    {
        var enemy = gObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.BindOnStatChangedEvnet(OnTargetEntityHpChanged);

            return;
        }
    }

    private void OnTargetEntityHpChanged(float hp)
    {
        _hpSilder.value = hp;
    }

    private void Update()
    {
        if (_targetTransform == null)
            return;

        var screenPos = Camera.main.WorldToScreenPoint(_targetTransform.position);

        var rectTransform = this.GetComponent<RectTransform>();
        if (rectTransform == null)
            return;

        Vector2 finalScreenPos = new Vector2(screenPos.x, screenPos.y - 50);
        rectTransform.anchoredPosition = finalScreenPos;
    }
}
