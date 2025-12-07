using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public void SetText(string text)
    {
        Text textMesh = GetComponent<Text>();
        if (textMesh != null)
        {
            textMesh.text = text;
        }
    }
}