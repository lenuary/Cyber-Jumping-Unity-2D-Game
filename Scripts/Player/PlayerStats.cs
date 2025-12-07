using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int currentXP { get; private set; }
    [SerializeField] public int xpToNextLevel = 100;
 
    [Header("Efekt Tekstowy XP")]
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private float textLifetime = 2f;
    private GameObject canvas;

    [Header("Stałe UI")]
    [SerializeField] private Text xpDisplayText;

    private void Start()
    {
        currentXP = 0;
        canvas = GameObject.FindGameObjectWithTag("UICanvas");
        if (canvas == null)
        {
        }

        UpdateXPText();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log("Zebrano XP! Aktualne XP: " + currentXP);

        UpdateXPText();

        StartCoroutine(ShowAndDestroyFloatingText(amount));
    }

    private void UpdateXPText()
    {
        if (xpDisplayText != null)
        {
            xpDisplayText.text = currentXP.ToString(); 
            
        }
        else
        {
        }
    }

    private IEnumerator ShowAndDestroyFloatingText(int xpValue)
    {
        if (floatingTextPrefab == null || canvas == null) 
        {
            yield break;
        }

        GameObject textInstance = Instantiate(floatingTextPrefab, canvas.transform);
        FloatingText floatingText = textInstance.GetComponent<FloatingText>();
        if (floatingText != null)
        {
            floatingText.SetText("+" + xpValue + " XP");
        }
        else
        {
        }

        yield return new WaitForSecondsRealtime(textLifetime);

        if (textInstance != null)
            Destroy(textInstance);
    }
}