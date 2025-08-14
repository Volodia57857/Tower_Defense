using UnityEngine;

public class OrkScript : EnemyBaseControler
{
    protected override void Awake()
    {
        base.Awake();

        HP = 35;
        Damage = 12;
        PriceForKill = 30;
    }

    protected override void Die()
    {
        base.Die();
    }
}
