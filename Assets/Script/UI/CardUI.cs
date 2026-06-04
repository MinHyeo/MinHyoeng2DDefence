using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Text _towerNameText;
    [SerializeField] private Image _towerIconImage;

    private int _instanceId;
    private string _towerId;

    public void InitCardUI(int instanceId, string towerId)
    {
        _instanceId = instanceId;
        _towerId = towerId;

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
}
