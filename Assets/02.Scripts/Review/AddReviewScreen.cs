using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;


public class AddReviewScreen : MonoBehaviour
{
    public GameObject reviewlistScreen;
    public GameObject addreviewScreen;
    public Button addButton;
    public Button cancelButton;

    public Dropdown localDropdown;
    public Dropdown courseDropdown;
    public Dropdown starDropdown;
    public InputField reviewDetailInput;

    private DatabaseReference databaseRef;

    private HeaderDropdown headerDropdown;



    private void Start()
    {
        // HeaderDropdown 인스턴스를 할당
        headerDropdown = FindObjectOfType<HeaderDropdown>();

        addButton.onClick.AddListener(OnAddButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);

        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.Options.DatabaseUrl = new System.Uri("https://travir-1dadd-default-rtdb.firebaseio.com/");
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private async void OnAddButtonClicked()
    {
        string selectedLocal = localDropdown.options[localDropdown.value].text;
        string selectedCourse = courseDropdown.options[courseDropdown.value].text;
        string selectedStar = starDropdown.options[starDropdown.value].text;
        string reviewDetail = reviewDetailInput.text;

        string courseKey = await FindCourseKeyByTitle(selectedLocal, selectedCourse);
        if (courseKey != null)
        {
            // 리뷰를 추가할 경로 설정
            string reviewPath = $"tour/{selectedLocal}/{courseKey}/reviews";

            // 저장할 데이터를 Dictionary로 구성
            Dictionary<string, object> reviewData = new Dictionary<string, object>();
            string reviewId = databaseRef.Child(reviewPath).Push().Key; // 리뷰의 고유 ID 생성

            reviewData[reviewId] = new Dictionary<string, object>
{
             { "star", selectedStar },
             { "review", reviewDetail }
};

            // 데이터베이스에 데이터 저장
            await databaseRef.Child(reviewPath).UpdateChildrenAsync(reviewData);
            Debug.Log("Review saved successfully!");


            if (reviewlistScreen != null)
            {
                reviewlistScreen.SetActive(true);
                addreviewScreen.SetActive(false);

                courseDropdown.ClearOptions();
                courseDropdown.AddOptions(new List<string> { "태그를 선택하세요" });
                courseDropdown.value = 0;

                starDropdown.value = 0;
                reviewDetailInput.text = string.Empty;

                headerDropdown.ResetDropdowns();
                RefreshList();

            }
        }
        else
        {
            Debug.LogError("Failed to find the course key for the selected course!");
        }




        async Task<string> FindCourseKeyByTitle(string localPath, string courseTitle)
        {
            DataSnapshot snapshot = await databaseRef.Child($"tour/{localPath}").GetValueAsync();
            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                Dictionary<string, object> data = (Dictionary<string, object>)childSnapshot.Value;
                foreach (var entry in data)
                {

                    if (entry.Value.ToString() == courseTitle)
                    {
                        return childSnapshot.Key;
                    }
                }
            }
            return null;
        }
    }

    private void OnCancelButtonClicked()
    {
        if (reviewlistScreen != null)
        {
            reviewlistScreen.SetActive(true);
            addreviewScreen.SetActive(false);
            RefreshList();
        }
    }

    private void RefreshList()
    {
        ReviewListScreen reviewListScreenComponent = reviewlistScreen.GetComponent<ReviewListScreen>();
        if (reviewListScreenComponent != null)
        {
            reviewListScreenComponent.Refresh();
        }
        else
        {
            Debug.LogError("ReviewListScreen component not found on reviewlistScreen!");
        }
    }
}