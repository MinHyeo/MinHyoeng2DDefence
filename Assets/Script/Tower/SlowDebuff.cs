using UnityEngine;

public class SlowDebuff : DebuffBase
{
    private float _slowPercent = 0.3f;
    private float _duration = 2f;

    public override void ApplyDebuff(Enemy enemy)
    {
        enemy.ApplySlow(_slowPercent, _duration);
    }
}