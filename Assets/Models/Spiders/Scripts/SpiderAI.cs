using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Transform target;
    public float sightRange = 15f;
    public float attackRange = 2.2f;
    public float preferredMin = 1.8f;
    public float preferredMax = 3.5f;
    public float attackCooldown = 1.25f;
    [Range(0, 1)] public float attack2Chance = 0.35f;

    float lastAttackTime;

    void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();
        if (!target)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) target = p.transform;
        }
    }

    void Update()
    {
        if (!target) return;

        float d = Vector3.Distance(transform.position, target.position);
        agent.stoppingDistance = Mathf.Clamp(preferredMin * 0.8f, 0.1f, attackRange - 0.1f);
        agent.SetDestination(target.position);

        // Backpedal band
        bool back = d < preferredMin;
        animator.SetBool("Backpedal", back);

        // Attack window
        if (d <= attackRange && Time.time >= lastAttackTime + attackCooldown && !back)
        {
            animator.SetInteger("AttackIndex", Random.value < attack2Chance ? 1 : 0);
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }

    // Hook these to animation events (optional):
    public void AE_LockMovement() { agent.isStopped = true; }
    public void AE_UnlockMovement() { agent.isStopped = false; }

    // Instead of damage, apply status to the player right at the “impact” frames:
    public void AE_ApplyStatus()
    {
        if (!target) return;
        var affectable = target.GetComponentInParent<IStatusAffectable>();
        if (affectable != null)
            affectable.ApplyStatus(new SlowStatus(0.5f, 1.5f)); // 50% slow for 1.5s (example)
    }
}
