using UnityEngine;

public class NecromancerScript : EnemyBaseControler
{
    [Header("Necromancer Settings")]
    [SerializeField] private float summonCooldown = 10f; 
    [SerializeField] private GameObject minionPrefab;    
    [SerializeField] private Transform summonPoint;
    [SerializeField] private ParticleSystem castEffect;

    private float summonTimer;
    private bool isCasting = false; 

    protected override void Awake()
    {
        if (castEffect != null)
            castEffect.Stop();
        base.Awake();

        HP = 35;
        Damage = 12;
        PriceForKill = 30;
        summonTimer = summonCooldown;
    }

    protected override void Update()
    {
        
        if (isCasting) return;

        base.Update();

       
        summonTimer -= Time.deltaTime;
        if (summonTimer <= 0f)
        {
            CastSpell();
            summonTimer = summonCooldown;
        }
    }

    private void CastSpell()
    {
        if (animator != null)
        {
            isCasting = true;

            if (NavMeshAgent != null)
                NavMeshAgent.isStopped = true;

            animator.Play("spellcast1 0", 0, 0f);

            
            if (castEffect != null)
            {
                castEffect.transform.position = transform.position; // на всякий випадок підтягуємо позицію
                castEffect.Play();
            }

            Invoke(nameof(SummonMinions), 1.5f);
            Invoke(nameof(EndCasting), 2f);
        }
    }

    private void EndCasting()
    {
        isCasting = false;

        if (NavMeshAgent != null && NavMeshAgent.enabled)
            NavMeshAgent.isStopped = false;

        if (castEffect != null)
            castEffect.Stop();
    }

    public void SummonMinions()
    {
        int minionCount = 3;
        float radius = 3.5f;

        for (int i = 0; i < minionCount; i++)
        {
            float angle = i * Mathf.PI * 2f / minionCount;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            Vector3 spawnPos = (summonPoint != null ? summonPoint.position : transform.position) + offset;

            Instantiate(minionPrefab, spawnPos, Quaternion.identity);
        }
    }

    protected override void Die()
    {
        base.Die();
    }
}