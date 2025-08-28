using UnityEngine;

public class GolemScript : EnemyBaseControler
{
    protected override void Awake()
    {
        base.Awake();
        HP = 50;
        Damage = 13;
        PriceForKill = 20;
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
