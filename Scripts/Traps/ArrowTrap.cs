using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Attack()
    {
        cooldownTimer = 0;

        int arrowIndex = FindArrow();

        if (arrowIndex != -1)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound(arrowSound, transform.position);


            arrows[arrowIndex].transform.position = firePoint.position;
            arrows[arrowIndex].GetComponent<EnemyProjectile>().ActivateProjectile();
        }
    }

    private int FindArrow()
    {
        if (arrows == null || arrows.Length == 0)
        {
            return -1;
        }

        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        
        return -1; 
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}