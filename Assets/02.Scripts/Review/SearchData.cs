/*using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SearchData : MonoBehaviour
{
    public Dropdown locationDropdown;
    public Dropdown conceptDropdown;
    public Dropdown seasonDropdown;
    public Dropdown companionDropdown;
    public Button searchButton;
    public Transform scrollViewContent;
    public GameObject listItemPrefab;

    private DatabaseReference databaseReference;

    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("YOUR_FIREBASE_DATABASE_URL");
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        searchButton.onClick.AddListener(SearchDataFromFirebase);
    }

    private void SearchDataFromFirebase()
    {
        string selectedLocation = locationDropdown.options[locationDropdown.value].text;
        string selectedConcept = conceptDropdown.options[conceptDropdown.value].text;
        string selectedSeason = seasonDropdown.options[seasonDropdown.value].text;
        string selectedCompanion = companionDropdown.options[companionDropdown.value].text;

        // Firebase 경로 설정
        DatabaseReference tourReference = databaseReference.Child("tour");

        // Query 생성
        Query query = tourReference.OrderByChild("location").EqualTo(selectedLocation);

        query.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                // 검색 결과를 저장할 리스트
                List<string> searchResults = new List<string>();

                foreach (var childSnapshot in snapshot.Children)
                {
                    DataSnapshot tourDataSnapshot = childSnapshot;

                    string concept = tourDataSnapshot.Child("tag/concept").Value.ToString();
                    string season = tourDataSnapshot.Child("tag/season").Value.ToString();
                    string companion = tourDataSnapshot.Child("tag/companion").Value.ToString();

                    // 조건에 맞는 데이터인지 확인
                    if (concept == selectedConcept && season == selectedSeason && companion == selectedCompanion)
                    {
                        string title = tourDataSnapshot.Child("title").Value.ToString();
                        searchResults.Add(title);
                    }
                }

                // 스크롤 뷰에 검색 결과 표시
                DisplaySearchResults(searchResults);
            }
        });
    }

    private void DisplaySearchResults(List<string> searchResults)
    {
        // 기존에 표시되어 있던 항목들 삭제
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        // 검색 결과를 스크롤 뷰에 추가
        foreach (string result in searchResults)
        {
            GameObject listItem = Instantiate(listItemPrefab, scrollViewContent);
            listItem.GetComponentInChildren<Text>().text = result;
        }
    }
}
*/