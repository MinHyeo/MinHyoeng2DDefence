using System.Collections.Generic;
[System.Serializable]
public class GameDataBase
{
    public string Id;
}

[System.Serializable]
public class EntityData : GameDataBase
{
    public string Name;
    public string Description;
    public string EntityType;
    public string IconPath;
}

[System.Serializable]
public class TowerData : GameDataBase
{
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float ProjectileSpeed;
    public string MoveType;
    public int BuildPrice;
    public int SellPrice;
    public string UpgradeId;
    public int UpgradePrice;
    public string PrefabPath;
}

[System.Serializable]
public class EnemyData : GameDataBase
{
    public float MaxHp;
    public float MoveSpeed;
    public float Defence;
    public int ReawrdGold;
    public int PenaltyLife;
    public string MoveType;
    public string PrefabPath;
}

[System.Serializable]
public class StageData : GameDataBase
{
    public int MaxLife;
    public string WaveId;
}

[System.Serializable]
public class WaveData : GameDataBase
{
    public int WaveGroup;
    public string EnemyId;
    public int Count;
    public float Interval;
    public float PreDelay;
}

