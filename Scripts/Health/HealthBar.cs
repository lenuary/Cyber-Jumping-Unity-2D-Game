using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    [Header("XP")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Image currentXpBar;

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.startingHealth / 10; 
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;

        if (playerStats != null && currentXpBar != null)
        {
            currentXpBar.fillAmount = (float)playerStats.currentXP / (float)playerStats.xpToNextLevel;
        }
    }
}