using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseControler : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected NavMeshAgent NavMeshAgent;
    [SerializeField] protected int HP;
    [SerializeField] protected int Damage;
    [SerializeField] protected int PriceForKill;
    [SerializeField] protected Animator animator;

    [Header("Audio Clips")]
    [SerializeField] protected AudioClip spawnSound;
    [SerializeField] protected AudioClip attackSound;
    [SerializeField] protected AudioClip hurtSound;
    [SerializeField] protected AudioClip deathSound;

    protected AudioSource audioSource;

    [Header("Target")]
    [SerializeField] protected Transform targetObject;
    protected Vector3 targetPosition;
    protected float stopDistance = 1f;

    //[Header("HealthBar")]
    //[SerializeField] private HealthBar healthBarPrefab; // Префаб плашки
    //private HealthBar healthBarInstance;

    

    protected virtual void Awake()
    {
        if (NavMeshAgent == null)
            NavMeshAgent = GetComponent<NavMeshAgent>();

        if (animator == null)
            animator = GetComponent<Animator>();


        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    protected virtual void Start()
    {
        PlaySound(spawnSound);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }

        if (targetObject != null)
            targetPosition = targetObject.position;

        if (NavMeshAgent != null && NavMeshAgent.isOnNavMesh)
        {
            NavMeshAgent.SetDestination(targetPosition);
            NavMeshAgent.avoidancePriority = Random.Range(1, 100);
        }

    }

    protected virtual void Update()
    {
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
            EnemySurvived();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        HP -= damage;


        PlaySound(hurtSound);

        if (HP <= 0)
        {
            Die();
        }
    }

    public virtual void Attack(GameObject target)
    {
        if (target != null)
        {

            PlaySound(attackSound);
        }
    }

    protected virtual void Die()
    {
        if (NavMeshAgent != null) NavMeshAgent.isStopped = true;

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        PlaySound(deathSound);


        Destroy(gameObject, 4f);
    }

    protected virtual void EnemySurvived()
    {
        Destroy(gameObject);
    }

    protected void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {

            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}