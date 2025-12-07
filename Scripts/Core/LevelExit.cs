using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// </summary>
public class LevelExit : MonoBehaviour
{
    [Header("Ustawienia Wyjścia")]
    [SerializeField] private int sceneToLoadIndex = 2;
    [SerializeField] private int requiredXP = 100;

    [Header("Dźwięk")]
    [SerializeField] private AudioClip levelEndSound;
    [SerializeField] private AudioClip lockedSound;

    [Header("Wizualizacja XP")]
    [SerializeField] private GameObject requiredXPPanel;
    [SerializeField] private Text requiredXPText;

    private bool isLoading = false;

    private void Start()
    {
        if (requiredXPPanel != null)
            requiredXPPanel.SetActive(false);
        
        if (requiredXPText != null)
            requiredXPText.text = $"COLLECT {requiredXP} XP!";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLoading && collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();

            if (playerStats != null && playerStats.currentXP >= requiredXP)
            {
                isLoading = true;

                if (SoundManager.instance != null)
                    SoundManager.instance.PlaySound(levelEndSound);

                PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                    playerMovement.enabled = false;

                if (requiredXPPanel != null)
                    requiredXPPanel.SetActive(false);

                SceneManager.LoadScene(sceneToLoadIndex);
            }
            else
            {
                if (SoundManager.instance != null && lockedSound != null)
                    SoundManager.instance.PlaySound(lockedSound);

                if (requiredXPPanel != null)
                    requiredXPPanel.SetActive(true);
                
                Debug.Log($"Potrzebujesz jeszcze {requiredXP - playerStats.currentXP} XP!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (requiredXPPanel != null)
                requiredXPPanel.SetActive(false);
        }
    }
}