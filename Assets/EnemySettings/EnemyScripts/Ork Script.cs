using UnityEngine;
public class OrkScript : EnemyBaseControler
{
    protected override void Awake()
    {
        base.Awake()
        HP = 120;
        Damage = 30;
        PriceForKill = 50;
    }

    protected override void Die()
    {
        base.Die();
    }
}