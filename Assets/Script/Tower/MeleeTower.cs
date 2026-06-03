using UnityEngine;

public class MeleeTower : Tower
{
    protected override void OnAttack(Transform target)
    {
        base.OnAttack(target);

        target.gameObject.GetComponent<Enemy>().OnDamaged(_towerData.AttackDamage);
    }
}
