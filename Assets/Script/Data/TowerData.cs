using UnityEngine;

[System.Serializable]
public class TowerData : GameDataBase
{
    public int AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float ProjectileSpeed;
    public string MoveType;
    public int BuildPrice;
    public int SellPrice;
    public string UpgradeId;
    public int UpgradePrice;
}