using UnityEngine;

public class DragonScript : EnemyBaseControler
{
    protected override void Awake()
    {
        base.Awake();

        HP = 100;
        Damage = 20;
        PriceForKill = 50;
    }

    protected override void Die()
    {
        base.Die();
    }
}
