using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavMeshAgent;
    [SerializeField] private int HP;
    [SerializeField] private int Damage;
    [SerializeField] private int PriceForKill;
    [SerializeField] private Animation anim;

    private Vector3 targetPosition = new Vector3(20, 0, 11);
    private float stopDistance = 0.5f;

    void Start()
    {
        // Притискаємо ворога до NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        anim.Play("Run");

        if (NavMeshAgent != null && NavMeshAgent.isOnNavMesh)
        {
            NavMeshAgent.SetDestination(targetPosition);
            NavMeshAgent.avoidancePriority = Random.Range(1, 100);
        }
    }

    void Update()
    {
        // Поворот ворога у напрямку руху
        Vector3 direction = NavMeshAgent.velocity.normalized;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, NavMeshAgent.angularSpeed * Time.deltaTime);
        }

        // ✅ Перевірка без помилок
        if (NavMeshAgent.isOnNavMesh &&
            !NavMeshAgent.pathPending &&
            NavMeshAgent.hasPath &&
            NavMeshAgent.remainingDistance <= stopDistance)
        {
            EnemySurvived();
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (NavMeshAgent != null) NavMeshAgent.isStopped = true;
        anim.Play("Death");
        Destroy(gameObject, 2f);
    }

    public void EnemySurvived()
    {
        Destroy(gameObject);
    }
}
