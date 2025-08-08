using UnityEngine;
using UnityEngine.AI;

public class GoelmScript : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavMeshAgent;
    [SerializeField] private int HP;
    [SerializeField] private int Damage;
    [SerializeField] private int PriceForKill;
    [SerializeField] private Animator animator;

    private Vector3 targetPosition = new Vector3(20, 0, 11);
    private float stopDistance = 1f;

    void Start()
    {
        // Притискаємо ворога до NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        

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

        
        if (NavMeshAgent.isOnNavMesh &&
            !NavMeshAgent.pathPending &&
            NavMeshAgent.hasPath &&
            NavMeshAgent.remainingDistance <= stopDistance)
        {
            //EnemySurvived();
            Die();
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
    /*public void Attack(GameObject target)
    {
        if (target != null)
        {
            // Тут можна додати логіку атаки
            // Наприклад, виклик методу на цілі, щоб вона отримала пошкодження
            target.GetComponent<Health>().TakeDamage(Damage);
        }
    }*/
    private void Die()
    {
        if (NavMeshAgent != null) NavMeshAgent.isStopped = true;


        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        Destroy(gameObject, 4f);
    }

    public void EnemySurvived()
    {
        Destroy(gameObject);
    }
}
