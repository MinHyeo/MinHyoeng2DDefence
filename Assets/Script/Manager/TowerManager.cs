using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Tilemap _tilemap;

    public bool CanPlaceTower(Vector3 cellPos)
    {
        Vector3Int vector3Int= _tilemap.WorldToCell(cellPos);
        if (!_tilemap.HasTile(vector3Int))
            return false;

        TileBase clickedTile = _tilemap.GetTile(vector3Int);
        if (clickedTile.name == "backyard_4")
            return false;

        return true;
    }

    public void SpawnTower(Vector3 cellPos)
    {
        Vector3Int vector3Int = _tilemap.WorldToCell(cellPos);
        Vector3 spawnPos = _tilemap.GetCellCenterWorld(vector3Int);
        GameObjectManager.Instance.CreateTowerOjbect("tower_01", spawnPos);
    }
}
