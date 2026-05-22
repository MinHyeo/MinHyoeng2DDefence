using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private int _instanceId;
    private EnemyData _enemyData;
    private int _waypointIndex = 0;
    private Vector3 _waypoint = Vector3.zero;
    private float _currentHp;

    public string _tempId = "enemy_01";

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _enemyData = GameDataManager.Instance.GetEnemyData(_tempId);
        _waypoint = WaypointManager.Instance.GetWaypoints()[++_waypointIndex];
        _currentHp = _enemyData.MaxHp;
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
        {
            if (WaypointManager.Instance.GetWaypoints().Count <= ++_waypointIndex)
            {
                GameObjectManager.Instance.RequestDestroyEnemyObject(_instanceId);
                WaveManager.Instance.DecreaseLife();
                return;
            }
            _waypoint = WaypointManager.Instance.GetWaypoints()[_waypointIndex];
        }    
    }

    public void OnDamaged(float damaged)
    {
        _currentHp -= damaged;
        if(_currentHp <= 0)
        {
            GameObjectManager.Instance.RequestDestroyEnemyObject(_instanceId);
        }

        _spriteRenderer.color = Color.red;

        Invoke("ChangeColor", 1f);
    }

    private void ChangeColor()
    {
        _spriteRenderer.color = Color.white;
    }
}