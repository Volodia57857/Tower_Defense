using UnityEngine;

public class DragonScript : EnemyBaseControler
{
    protected override void Awake()
    {
        base.Awake();

        HP = 250;
        Damage = 50;
        PriceForKill = 100;
    }

    protected override void Die()
    {
        base.Die();
    }
}
