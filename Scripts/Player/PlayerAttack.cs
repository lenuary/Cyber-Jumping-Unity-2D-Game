using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private AudioClip fireballSound;
    private Animator anim;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (playerMovement.canAttack())
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySound(fireballSound);

        anim.SetTrigger("attack");
        playerMovement.ResetAttackCooldown();

    }

    public void ShootFireball()
    {
        int fireballIndex = FindFireball();
        if (fireballIndex != -1)
        {
            fireballs[fireballIndex].transform.position = firePoint.position;
            fireballs[fireballIndex].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            
            fireballs[fireballIndex].SetActive(true);
        }
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return -1;
    }
}