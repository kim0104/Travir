using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddReviewScreen : MonoBehaviour
{
    public GameObject reviewlistScreen;
    public GameObject addreviewScreen;
    public Button addButton;
    public Button cancelButton;

    private void Start()
    {
        addButton.onClick.AddListener(OnAddButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    private void OnAddButtonClicked()
    {
        if (reviewlistScreen != null)
        {
 
            reviewlistScreen.SetActive(true);
            addreviewScreen.SetActive(false);
        }
    }

    private void OnCancelButtonClicked()
    {
        if (reviewlistScreen != null)
        {

            reviewlistScreen.SetActive(true);
            addreviewScreen.SetActive(false);
        }
    }
}
