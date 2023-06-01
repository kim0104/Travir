using Firebase;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;


public class ReviewListScreen : MonoBehaviour
{
    public GameObject reviewlistScreen;
    public GameObject addreviewScreen;
    public GameObject tiledetailScreen;
    public Button writeButton;
    public Button refreshButton;

    public ScrollRect scrollView;

    //드롭다운
    public Dropdown courseDropdown;
    public Transform tileContainer;

    //스크롤뷰의 버튼타일
    public Sprite buttonImage;

    private DatabaseReference databaseRef;

    private void Start()
    {
        writeButton.onClick.AddListener(OnWriteButtonClicked);
        refreshButton.onClick.AddListener(Refresh);
        courseDropdown.onValueChanged.AddListener(delegate { FilterTilesByCourse(); });


        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.Options.DatabaseUrl = new System.Uri("https://your-firebase-database-url"); 
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        LoadTileValues();
    }

    private void OnWriteButtonClicked()
    {
        if (addreviewScreen != null)
        {
            reviewlistScreen.SetActive(false);
            addreviewScreen.SetActive(true);
        }
    }

    public void Refresh()
    {
        ClearTiles();
        LoadTileValues();
    }

    private async void LoadTileValues()
    {
        DataSnapshot snapshot = await databaseRef.Child("tour").GetValueAsync();
        foreach (DataSnapshot childSnapshot in snapshot.Children)
        {
            foreach (DataSnapshot grandchildSnapshot in childSnapshot.Children)
            {
                if (grandchildSnapshot.HasChild("reviews"))
                {
                    DataSnapshot reviewsSnapshot = grandchildSnapshot.Child("reviews");
                    foreach (DataSnapshot reviewSnapshot in reviewsSnapshot.Children)
                    {
                        string titleValue = grandchildSnapshot.Child("title").Value?.ToString();
                        string starValue = reviewSnapshot.Child("star").Value?.ToString();
                        string reviewValue = reviewSnapshot.Child("review").Value?.ToString();

                        CreateTile(titleValue, starValue, reviewValue, buttonImage);
                    }
                }
            }
        }
    }

    private void ClearTiles()
    {
        foreach (Transform child in tileContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateTile(string title, string starValue, string reviewValue, Sprite buttonImage)
    {
        GameObject buttonObject = new GameObject("TileButton");
        Button tileButton = buttonObject.AddComponent<Button>();

        tileButton.targetGraphic = buttonObject.AddComponent<Image>();
        tileButton.image.sprite = buttonImage;

        RectTransform buttonTransform = buttonObject.GetComponent<RectTransform>();
        buttonTransform.SetParent(tileContainer, false);

        buttonTransform.sizeDelta = new Vector2(650f, 70f);

        GameObject textObject = new GameObject("TileText");
        Text tileText = textObject.AddComponent<Text>();

        tileText.text = "[" + title + "]\n" +
                         "별점: " + starValue + "\n" +
                         "내용: " + reviewValue;

        RectTransform textTransform = textObject.GetComponent<RectTransform>();
        textTransform.SetParent(buttonTransform, false);

        textTransform.sizeDelta = new Vector2(580f, 70f);

        tileText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        tileText.fontSize = 14;
        tileText.fontStyle = FontStyle.Bold;
        tileText.alignment = TextAnchor.MiddleLeft;
        tileText.color = Color.black;

        textTransform.anchoredPosition = new Vector2(5f, 0f);

        tileButton.onClick.AddListener(() =>
        {
            tiledetailScreen.SetActive(true);
            TileDetailScreen detailScreen = FindObjectOfType<TileDetailScreen>();
            if (detailScreen != null)
            {
                detailScreen.ShowDetail(title, starValue, reviewValue);
            }
        });
    }

    public void FilterTilesByCourse()
    {
        string selectedCourse = courseDropdown.options[courseDropdown.value].text;

        for (int i = 0; i < tileContainer.childCount; i++)
        {
            Button tileButton = tileContainer.GetChild(i).GetComponent<Button>();
            if (tileButton != null)
            {
                Text buttonText = tileButton.GetComponentInChildren<Text>();
                if (buttonText != null)
                {
                    string buttonTitle = GetTitleFromButtonText(buttonText.text);
                    if (buttonTitle == selectedCourse)
                    {
                        tileButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        tileButton.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private string GetTitleFromButtonText(string buttonText)
    {
        int startIndex = buttonText.IndexOf('[');
        int endIndex = buttonText.IndexOf(']');
        if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
        {
            return buttonText.Substring(startIndex + 1, endIndex - startIndex - 1);
        }
        return string.Empty;
    }



}
