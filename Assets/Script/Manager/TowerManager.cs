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

    public void CreateTowerTest()
    {
        GameObject towerPrefab = Resources.Load<GameObject>("tower_01");
        GameObject towerObject = Instantiate(towerPrefab);
        towerObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        towerObject.transform.position = new Vector3(towerObject.transform.position.x, towerObject.transform.position.y, 0);
    }

    public bool CanPlaceTower(Vector3 cellPos)
    {
        Vector3Int vector3Int = _tilemap.WorldToCell(cellPos);
        if (!_tilemap.HasTile(vector3Int))
        {
            Debug.LogWarning("타일이 없음");
            return false;
        }

        TileBase clickedTile = _tilemap.GetTile(vector3Int);
        if (clickedTile.name == "backyard_4")
        {
            Debug.LogWarning("배치 불가능한 타일임");
            return false;
        }

        // TODO : 이거 나중에 바꿔야함(너무 성능 이슈가 있을거 같음)
        RaycastHit2D[] hits = Physics2D.RaycastAll(cellPos, Vector2.zero);
        foreach(var hit in hits)
        {
            var component = hit.collider.GetComponent<Tower>();
            if (component != null)
                return false;
        }

        return true;
    }

    public void SpawnTower(string towerId, Vector3 cellPos)
    {
        Vector3Int vector3Int = _tilemap.WorldToCell(cellPos);
        Vector3 spawnPos = _tilemap.GetCellCenterWorld(vector3Int);
        GameObjectManager.Instance.CreateTowerOjbect(towerId, spawnPos);
    }
}
