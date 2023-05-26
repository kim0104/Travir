using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewListScreen : MonoBehaviour
{

    public GameObject reviewlistScreen;
    public GameObject addreviewScreen;
    public Button addButton;


    private void Start()
    {
        addButton.onClick.AddListener(OnAddButtonClicked);
    }

    private void OnAddButtonClicked()
    {
        if (addreviewScreen != null)
        {

            reviewlistScreen.SetActive(false);
            addreviewScreen.SetActive(true);

        }
    }

   
}
