using UnityEngine;

public class GhoulScript : EnemyBaseControler
{
    protected override void Awake()
    {
        base.Awake();

        HP = 10;
        Damage = 5;
        PriceForKill = 5;
    }

    protected override void Die()
    {
        base.Die();
    }
}