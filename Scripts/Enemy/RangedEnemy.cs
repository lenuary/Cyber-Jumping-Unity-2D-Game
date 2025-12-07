using System.Runtime.Serialization;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Fireball Sound")]

    [SerializeField] private AudioClip FireballSound;

    private Animator anim;
    [SerializeField] private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
{
    cooldownTimer += Time.deltaTime;

    if (PlayerInSight())
    {
        if (cooldownTimer >= attackCooldown)
        {
            anim.SetTrigger("rangedAttack");
        }
    }
    if (enemyPatrol != null)
        enemyPatrol.enabled = !PlayerInSight();
}
        private void RangedAttack()
    {
        int fireballIndex = FindFireball();

        if (fireballIndex != -1)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound(FireballSound);

            cooldownTimer = 0;
            fireballs[fireballIndex].transform.position = firepoint.position;
            fireballs[fireballIndex].GetComponent<EnemyProjectile>().ActivateProjectile();
        }
    }

    private int FindFireball()
    {
        if (fireballs == null || fireballs.Length == 0)
        {
            Debug.LogError("Tablica 'fireballs' w RangedEnemy jest pusta!");
            return -1;
        }
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (fireballs[i] != null && !fireballs[i].activeInHierarchy)
                return i;
        }
        return -1;
    }

    private bool PlayerInSight()
{
    Vector3 origin = boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
    Vector3 size = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

    Collider2D hit = Physics2D.OverlapBox(origin, size, 0, playerLayer);

    return hit != null;
}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}