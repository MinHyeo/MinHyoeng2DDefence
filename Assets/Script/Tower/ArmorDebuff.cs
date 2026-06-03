using UnityEngine;

public class ArmorDebuff : DebuffBase
{
    private float _reduceAmount = 2f;
    private float _duration = 3f;

    public override void ApplyDebuff(Enemy enemy)
    {
        enemy.ApplyArmorReduce(_reduceAmount, _duration);
    }
}