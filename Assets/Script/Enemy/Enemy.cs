using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private int _instanceId;
    private EnemyData _enemyData;
    private int _waypointIndex = 0;
    private Vector3 _waypoint = Vector3.zero;

    public string _tempId = "enemy_01";

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _enemyData = GameDataManager.Instance.GetEnemyData(_tempId);
        _waypoint = WaypointManager.Instance.GetWaypoints()[_waypointIndex++];
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    public void InitEnemyInfoOnCreated(int instanceId)
    {
        _instanceId = instanceId;
    }

    private void OnMove()
    {
        Vector3 direction = (_waypoint - transform.position).normalized;

        transform.Translate(direction * _enemyData.MoveSpeed * Time.fixedDeltaTime);
        _spriteRenderer.flipX = direction.x < 0;

        float distance = Vector3.Distance(_waypoint, transform.position);

        if (distance < 0.05f)
            _waypoint = WaypointManager.Instance.GetWaypoints()[_waypointIndex++];
    }

    public void OnDamaged()
    {
        _spriteRenderer.color = Color.red;

        Invoke("ChangeColor", 1f);
    }

    private void ChangeColor()
    {
        _spriteRenderer.color = Color.white;
    }
}