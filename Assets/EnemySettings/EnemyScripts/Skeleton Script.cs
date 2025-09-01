using UnityEngine;

public class SkeletonScript : EnemyBaseControler
{
    protected override void Awake()
    {
        base.Awake();

        HP = 5;
        Damage = 5;
        PriceForKill = 5;
    }

    protected override void Die()
    {
        base.Die();
    }
}
