using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class LevelIntroController : MonoBehaviour
{
    [SerializeField] private GameObject levelIntroPanel;
    [SerializeField] private float delayTime = 10f;       

    [Header("Skrypty Gracza do wyłączenia")]
    [SerializeField] private PlayerMovement playerMovement; 
    [SerializeField] private PlayerAttack PlayerAttack;
    [SerializeField] private PlayerInput playerInputComponent;


    void Start()
    {
        if (playerMovement == null || PlayerAttack == null || playerInputComponent == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (playerMovement == null)
                    playerMovement = player.GetComponent<PlayerMovement>();
                
                if (PlayerAttack == null)
                {
                    PlayerAttack = player.GetComponent<PlayerAttack>();
                }
                
                if (playerInputComponent == null)
                {
                    playerInputComponent = player.GetComponent<PlayerInput>();
                }
            }
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false; 
        }
        if (PlayerAttack != null)
        {
            PlayerAttack.enabled = false;
        }
        if (playerInputComponent != null) 
        {
            playerInputComponent.enabled = false; 
        }

        levelIntroPanel.SetActive(true);

        StartCoroutine(StartLevelAfterDelay());
    }

    private IEnumerator StartLevelAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        
        if (playerMovement != null)
        {
            playerMovement.enabled = true; 
        }
        if (PlayerAttack != null)
        {
            PlayerAttack.enabled = true;
        }
        if (playerInputComponent != null) 
        {
            playerInputComponent.enabled = true; 
        }

        levelIntroPanel.SetActive(false);
    }
}