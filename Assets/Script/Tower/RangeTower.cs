using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] private GameObject _projectilePrefab;

    protected override void OnAttack(Transform target)
    {
        base.OnAttack(target);

        var projectileObject = Instantiate(_projectilePrefab);
        projectileObject.transform.position = transform.position;
        var projectileComponent = projectileObject.GetComponent<Projectile>();

        float projectileSpeed = _towerData.ProjectileSpeed;
        float projectileDamage = _towerData.AttackDamage;
        projectileComponent.SetTargetTransform(target, projectileSpeed, projectileDamage);
    }
}
