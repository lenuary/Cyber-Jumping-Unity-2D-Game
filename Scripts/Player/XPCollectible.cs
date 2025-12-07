using UnityEngine;

public class XPCollectible : MonoBehaviour
{
    [SerializeField] private int xpValue = 10;
    [SerializeField] private AudioClip pickupSound;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.AddXP(xpValue);
                
                if (SoundManager.instance != null && pickupSound != null)
                    SoundManager.instance.PlaySound(pickupSound);

                Destroy(gameObject); 
            }
        }
    }
}