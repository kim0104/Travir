using UnityEngine;
using UnityEngine.UI;

public class TextLengthLogger : MonoBehaviour
{
    void Start()
    {
        foreach (Transform buttonTransform in transform)
        {
            Text textComponent = buttonTransform.GetComponentInChildren<Text>();

            if (textComponent)
            {
                Debug.Log($"Text in '{buttonTransform.name}' has {textComponent.text.Length} characters.");
            }
            else
            {
                Debug.LogWarning($"No Text component found for button '{buttonTransform.name}'!");
            }
        }
    }
}
