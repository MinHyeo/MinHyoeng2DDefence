using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _targetTransform;
    private float _speed;
    private float _damage;

    private void FixedUpdate()
    {
        OnMove();
    }

    public void SetTargetTransform(Transform target, float speed, float damage)
    {
        _targetTransform = target;
        _speed = speed;
        _damage = damage;
    }

    private void OnMove()
    {
        // Destory 된다고 바로 코드가 실행이 멈추지 않고 사라지기 전까지 일부분은 실행이 됨
        if (_targetTransform == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 direction = (_targetTransform.position - transform.position).normalized;

        transform.Translate(direction * _speed * Time.fixedDeltaTime, Space.World);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("적 접촉");
            Enemy enemyComponent = null;
            if(collision.TryGetComponent<Enemy>(out enemyComponent))
            {
                Debug.Log("적에게 데미지 부여");
                enemyComponent.OnDamaged(_damage);

                DebuffBase[] debuffs = GetComponents<DebuffBase>();
                foreach(var debuff in debuffs)
                {
                    Debug.Log("디버프 부여");
                    debuff.ApplyDebuff(enemyComponent);
                }

                Destroy(this.gameObject);
            }
        }
    }
}