using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoUI : UIBase
{
    [Header("UI Exit Button")]
    [SerializeField] private UIButton _exitButton;

    [Header("Tower Base Info")]
    [SerializeField] private Image _towerIcon;
    [SerializeField] private Text _towerName;
    [SerializeField] private Text _towerDescription;

    [Header("Tower Stat")]
    [SerializeField] private Text _towerAttackPower;
    [SerializeField] private Text _towerAttackRange;
    [SerializeField] private Text _towerAttackSpeed;

    private void OnEnable()
    {
        Debug.Log($"종료 등록 {_exitButton}");

        _exitButton.BindOnClickButtonEvent(ExitCardInfoUI);
    }

    private void OnDisable()
    {
        _exitButton.UnBindOnClickButtonEvent(ExitCardInfoUI);
    }

    private void ExitCardInfoUI()
    {
        Debug.Log("UI 종료");
        UIManager.Instance.CloseUI(UIRootType.PopupUI, UIType.CardInfoPopupUI);
    }

    public void InitCardInfoUI(string towerId)
    {
        var entityData = GameDataManager.Instance.GetData<EntityData>(towerId);
        SetTowerBaseInfo(entityData);

        var towerData = GameDataManager.Instance.GetData<TowerData>(towerId);
        SetTowerStat(towerData);
    }

    private void SetTowerBaseInfo(EntityData entityData)
    {
        string path = entityData.IconPath;
        ResourceManager.Instance.LoadSprite(path, (sprite) =>
        {
            _towerIcon.sprite = sprite;
        });

        _towerName.text = entityData.Name;
        _towerDescription.text = entityData.Description;
    }

    private void SetTowerStat(TowerData towerData)
    {
        _towerAttackPower.text = "공격력 : " + towerData.AttackDamage.ToString();
        _towerAttackRange.text = "공격범위 : " + towerData.AttackRange.ToString();
        _towerAttackSpeed.text = "공격속도 : " + towerData.AttackSpeed.ToString();
    }
}
