using UnityEngine;

public class GolemScript : EnemyBaseControler
{
    protected override void Awake()
    {
        base.Awake();
        HP = 25;
        Damage = 8;
        PriceForKill = 10;
    }

    protected override void Die()
    {
        base.Die();
    }
    protected override void Start()
    {
        base.Start();

    }
}
