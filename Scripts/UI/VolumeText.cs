using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class VolumeText : MonoBehaviour
{
    [Header("Ustawienia Tekstu")]
    [SerializeField] private string volumeType;
    [SerializeField] private string variableName;
    private Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    private void Start()
    {
        RefreshText();
    }

    public void RefreshText()
    {
        if (textComponent == null) return;
        float volume = PlayerPrefs.GetFloat(variableName, 1);
        textComponent.text = volumeType + " " + volume.ToString("F1");
    }
}
