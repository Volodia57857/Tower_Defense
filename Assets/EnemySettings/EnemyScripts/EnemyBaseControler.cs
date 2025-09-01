using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    [Header("Attack Settings")]
    [SerializeField] protected float attackCooldown = 1.5f; 
    protected float lastAttackTime;

    [Header("HealthBar")]
    [SerializeField] private UnityEngine.UI.Slider HP_slider;
    [SerializeField] private Canvas HP_Canvas;

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
        HP_Canvas = transform.Find("Canvas").GetComponent<Canvas>();
        HP_slider = HP_Canvas.transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>();
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
        HP_slider.maxValue = HP;
        HP_slider.value = HP;
        HP_Canvas.transform.LookAt(Camera.main.transform);
    }
    protected virtual void Update()
    {
        Vector3 direction = NavMeshAgent.velocity.normalized;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, NavMeshAgent.angularSpeed * Time.deltaTime);
        }
        HP_Canvas.transform.LookAt(Camera.main.transform);
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
        HP_slider.value = HP;

        PlaySound(hurtSound);

        if (HP <= 0)
        {
            Die();
        }
    }
    protected virtual void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack(targetObject.gameObject);
        }
    }
    public virtual void Attack(GameObject target)
    {
        if (target != null)
        {
            PlaySound(attackSound);
            if (animator != null)
                animator.SetTrigger("Attack");
            //var health = target.GetComponent<PlayerHealth>();
            //if (health != null)
            //{
                //health.TakeDamage(Damage);
            //}
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
        WaveManager.aliveEnemies--;
        Destroy(gameObject, 4f);
    }
    protected virtual void EnemySurvived()
    {
        WaveManager.aliveEnemies--;
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