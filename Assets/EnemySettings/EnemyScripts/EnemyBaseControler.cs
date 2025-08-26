using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseControler : MonoBehaviour
{
    [Header("Enemy Settings")]
    public NavMeshAgent agent;
    public int HP = 50;
    public int Damage = 10;
    public int PriceForKill = 20;
    public Animator animator;

    [Header("Audio Clips")]
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    protected AudioSource audioSource;

    [Header("Target")]
    public Transform targetObject;
    protected Vector3 targetPosition;

    protected float stopDistance = 1f;

    protected virtual void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // Регистрируем у GlobalMusicManager для SFX
        GlobalMusicManager.instance?.RegisterSFXSource(audioSource);
    }

    protected virtual void Start()
    {
        PlaySound(spawnSound);

        if (targetObject != null)
            targetPosition = targetObject.position;

        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(targetPosition);
            agent.avoidancePriority = Random.Range(1, 100);
        }
    }

    protected virtual void Update()
    {
        if (agent == null) return;

        Vector3 dir = agent.velocity.normalized;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, agent.angularSpeed * Time.deltaTime);
        }

        if (agent.isOnNavMesh && !agent.pathPending && agent.hasPath && agent.remainingDistance <= stopDistance)
            EnemySurvived();
    }

    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        PlaySound(hurtSound);

        if (HP <= 0)
            Die();
    }

    public virtual void Attack(GameObject target)
    {
        if (target != null)
            PlaySound(attackSound);
    }

    protected virtual void Die()
    {
        if (agent != null) agent.isStopped = true;
        if (animator != null) animator.SetTrigger("Die");
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

    protected virtual void OnDestroy()
    {
        // Убираем из списка SFX при уничтожении
        GlobalMusicManager.instance?.UnregisterSFXSource(audioSource);
    }
}