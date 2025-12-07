using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [Header("SFX")]
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.AddHealth(1);

                if (SoundManager.instance != null && pickupSound != null)
                    SoundManager.instance.PlaySound(pickupSound);

                gameObject.SetActive(false);
            }
        }
    }
}