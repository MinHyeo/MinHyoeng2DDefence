using NUnit.Framework.Constraints;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private int _instanceId;
    private EnemyData _enemyData;
    private int _waveGroup;
    private int _waypointIndex = 0;
    private Vector3 _waypoint = Vector3.zero;

    private float _currentHp;
    private float _currentDenfece;
    private float _currentSpeed;
    private string _moveType;
    public string MoveType => _moveType;
    private Coroutine _armorReduceCoroutine = null;
    private Coroutine _slowCoroutine = null;

    private event Action<float> _onHpChanged;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    private void OnDisable()
    {
        UIManager.Instance.RemoveHudSlot(_instanceId);
        WaveManager.Instance.DecreaseEnemyCount();

        _onHpChanged = null;
    }

    // 적 생성 시 초기화
    public void InitEnemyInfoOnCreated(int instanceId, string enemyDataId, int waveGroup)
    {
        _instanceId = instanceId;

        _enemyData = GameDataManager.Instance.GetData<EnemyData>(enemyDataId);
        _waveGroup = waveGroup;
        _waypoint = WaypointManager.Instance.GetWaypoints(_waveGroup)[_waypointIndex];
        _currentHp = _enemyData.MaxHp;
        _currentDenfece = _enemyData.Defence;
        _currentSpeed = _enemyData.MoveSpeed;
        _moveType = _enemyData.MoveType;

        UIManager.Instance.AddHudSlot(_instanceId, this.transform);
    }

    // 적 이동
    private void OnMove()
    {
        Vector3 direction = (_waypoint - transform.position).normalized;

        transform.Translate(direction * _currentSpeed * Time.fixedDeltaTime);
        _spriteRenderer.flipX = direction.x < 0;

        float distance = Vector3.Distance(_waypoint, transform.position);

        if (distance < 0.05f)
        {
            if (WaypointManager.Instance.GetWaypoints(_waveGroup).Count <= ++_waypointIndex)
            {
                GameObjectManager.Instance.RequestDestroyEnemyObject(_instanceId);
                //LifeManager.Instance.DecreaseLifeCount();
                StageManager.Instance.DecreaseLifeCount();
                return;
            }
            _waypoint = WaypointManager.Instance.GetWaypoints(_waveGroup)[_waypointIndex];
        }    
    }

    // 적 방어력 감소 디버프 적용
    public void ApplyArmorReduce(float reduceAmount, float duration)
    {
        if (_armorReduceCoroutine != null)
            return;

        _armorReduceCoroutine = StartCoroutine(CoArmorReduce(reduceAmount, duration));
    }

    private IEnumerator CoArmorReduce(float reduceAmount, float duration)
    {
        _currentDenfece -= reduceAmount;
        Debug.Log($"방어력 감소 : {_enemyData.Defence} -> {_currentDenfece}");

        yield return new WaitForSeconds(_currentDenfece);

        Debug.Log("방어력 복구");
        _currentDenfece += reduceAmount;
        _armorReduceCoroutine = null;
    }

    // 적 슬로우 디버프 적용
    public void ApplySlow(float slowPercent, float duration)
    {
        if (_slowCoroutine != null)
            return;

        _slowCoroutine = StartCoroutine(CoSlow(slowPercent, duration));
    }

    private IEnumerator CoSlow(float slowPercent, float duration)
    {
        _currentSpeed -= (_currentSpeed * slowPercent);
        Debug.Log($"이동 속도 감소 : {_enemyData.MoveSpeed} -> {_currentSpeed}");

        yield return new WaitForSeconds(duration);

        _currentSpeed = _enemyData.MoveSpeed;
        _slowCoroutine = null;
    }

    // 데미지 부여
    public void OnDamaged(float damaged)
    {
        _currentHp -= (damaged - _currentDenfece);
        InvokeStatChangedEvnet();
        if(_currentHp <= 0)
        {
            //MeatManager.Instance.IncreaseMeatCount(_enemyData.RewardGold);
            StageManager.Instance.IncreaseMeatCount(_enemyData.RewardGold);
            GameObjectManager.Instance.RequestDestroyEnemyObject(_instanceId);
        }

        _spriteRenderer.color = Color.red;

        Invoke("ChangeColor", 1f);
    }

    private void ChangeColor()
    {
        _spriteRenderer.color = Color.white;
    }

    public void BindOnStatChangedEvnet(Action<float> hpChangeCallback)
    {
        _onHpChanged += hpChangeCallback;
    }

    private void InvokeStatChangedEvnet()
    {
        float hp = _currentHp / _enemyData.MaxHp;
        _onHpChanged?.Invoke(hp);
    }
}