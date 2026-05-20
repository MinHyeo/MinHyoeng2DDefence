using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum AttackType
{
    None,
    AttackUp = 1,
    Attack = 0,
    AttackDown = -1,
}

public class Tower : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public string _tempId;
    private TowerData _towerData;
    private bool _isAttacking = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _towerData = GameDataManager.Instance.GetTowerData(_tempId);
    }

    private void Update()
    {
        if (_isAttacking)
            return;

        CheckEnemyInAttackRanage();
    }

    private void CheckEnemyInAttackRanage()
    {
        RaycastHit2D rayHit = Physics2D.CircleCast(transform.position, _towerData.AttackRange, Vector2.zero, 1, LayerMask.GetMask("Enemy"));
        if (rayHit)
        {
            Debug.Log("공격 대상 찾음");
            OnAttack(rayHit.transform);
        }
    }

    private void OnAttack(Transform target)
    {
        _isAttacking = true;

        _spriteRenderer.flipX = IsEnemyOnLeft(target);

        _animator.SetTrigger("IsAttack");

        float angle = GetAngle(target);

        if(angle > 30)
        {
            _animator.SetInteger("AttackType", 1);
        }
        else if(angle > -30)
        {
            _animator.SetInteger("AttackType", 0);
        }
        else
        {
            _animator.SetInteger("AttackType", -1);
        }

        target.gameObject.GetComponent<Enemy>().OnDamaged();
    }

    private bool IsEnemyOnLeft(Transform target)
    {
        return (target.position.x - transform.position.x < 0);
    }

    private float GetAngle(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        direction.x = Mathf.Abs(direction.x);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return angle;
    }

    public void EndAttack()
    {
        _isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        // 게임이 실행 중이 아닐 때 _towerData가 없어서 에러 나는 것을 방지
        if (_towerData == null) return;

        // 기즈모 색상을 반투명한 빨간색으로 설정
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);

        // CircleCast의 시작 지점과 반지름(_towerData.AttackRange)으로 원을 그림
        Gizmos.DrawWireSphere(transform.position, _towerData.AttackRange);
    }
}
