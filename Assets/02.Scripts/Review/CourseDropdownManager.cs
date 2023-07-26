using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System;

public class CourseDropdownManager : MonoBehaviour
{
    public Dropdown localDropdown;
    public Dropdown conceptDropdown;
    public Dropdown seasonDropdown;
    public Dropdown companionDropdown;
    public Dropdown courseDropdown;
    public Button searchButton;

    private DatabaseReference databaseRef;

    private void Start()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.Options.DatabaseUrl = new System.Uri("https://travir-1dadd-default-rtdb.firebaseio.com/");
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        searchButton.onClick.AddListener(async () => await SearchButtonClicked());

        InitializeDropdown(courseDropdown);
        OnDropdownValueChanged(courseDropdown);
    }

    private void InitializeDropdown(Dropdown dropdown)
    {
        dropdown.ClearOptions();
        dropdown.options.Add(new Dropdown.OptionData("태그를 선택하세요"));
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    private void RemoveDuplicateOptions(Dropdown dropdown)
    {
        HashSet<string> uniqueOptions = new HashSet<string>(dropdown.options.Select(option => option.text).Where(text => !string.IsNullOrEmpty(text)));

        dropdown.ClearOptions();
        dropdown.AddOptions(uniqueOptions.ToList());

        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    public async void OnDropdownValueChanged(Dropdown dropdown)
    {
        // 검색 버튼을 클릭한 경우에만 동작
        if (searchButtonClicked)
        {
            string local = localDropdown.options[localDropdown.value].text;
            string companion = companionDropdown.options[companionDropdown.value].text;
            string season = seasonDropdown.options[seasonDropdown.value].text;
            string concept = conceptDropdown.options[conceptDropdown.value].text;

            SearchData(local, companion, season, concept);

            RemoveDuplicateOptions(dropdown);
        }
    }



    private bool searchButtonClicked = false;
    private async Task SearchButtonClicked()
    {
        searchButtonClicked = true;

        try
        {
            // 기존 코드
            string local = localDropdown.options[localDropdown.value].text;
            string concept = conceptDropdown.options[conceptDropdown.value].text;
            string companion = companionDropdown.options[companionDropdown.value].text;
            string season = seasonDropdown.options[seasonDropdown.value].text;

            await SearchData(local, companion, season, concept);

            // Initialize dropdown options
           // InitializeDropdown(courseDropdown);

            // UI 업데이트
            StartCoroutine(UpdateDropdownCaptionDelayed());
            ReviewListScreen reviewListScreen = FindObjectOfType<ReviewListScreen>();
            if (reviewListScreen != null && reviewListScreen.isActiveAndEnabled)
            {
                reviewListScreen.FilterTilesByCourse(courseDropdown.options[courseDropdown.value].text);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Runtime Error: " + e.Message);
        }
    }



    private IEnumerator UpdateDropdownCaptionDelayed()
    {
        // 1 프레임 대기
        yield return null;

        // 첫 번째 옵션의 텍스트를 label에 표시
        if (courseDropdown.options.Count > 0)
        {
            if (courseDropdown.captionText != null)
            {
                courseDropdown.captionText.text = courseDropdown.options[0].text;
                LayoutRebuilder.ForceRebuildLayoutImmediate(courseDropdown.GetComponent<RectTransform>());
            }
        }
        else
        {
            if (courseDropdown.captionText != null)
            {
                courseDropdown.captionText.text = "";
                LayoutRebuilder.ForceRebuildLayoutImmediate(courseDropdown.GetComponent<RectTransform>());
            }
        }
    }




    private async Task SearchData(string local, string companion = null, string season = null, string concept = null)
    {
        string path = "tour/" + local;

        DataSnapshot snapshot = await databaseRef.Child(path).GetValueAsync();
        Dictionary<string, object> data = snapshot.Value as Dictionary<string, object>;

        if (data != null)
        {
            List<(int, string)> titleMatches = new List<(int, string)>();

            courseDropdown.ClearOptions();

            foreach (KeyValuePair<string, object> entry in data)
            {
                string pushId = entry.Key;
                Dictionary<string, object> details = entry.Value as Dictionary<string, object>;

                string title = details.ContainsKey("title") ? details["title"].ToString() : null;
                Dictionary<string, object> tag = details.ContainsKey("tag") ? details["tag"] as Dictionary<string, object> : null;

                int matchCount = 0;
                if (tag != null)
                {
                    if (string.IsNullOrEmpty(companion) || (tag.ContainsKey("companion") && tag["companion"].ToString() == companion))
                        matchCount++;
                    if (string.IsNullOrEmpty(season) || (tag.ContainsKey("season") && tag["season"].ToString() == season))
                        matchCount++;
                    if (string.IsNullOrEmpty(concept) || (tag.ContainsKey("concept") && tag["concept"].ToString() == concept))
                        matchCount++;
                }

                titleMatches.Add((matchCount, title));
            }

            titleMatches.Sort((x, y) => y.Item1.CompareTo(x.Item1));

            foreach (var titleMatch in titleMatches.Take(3))
            {
                courseDropdown.options.Add(new Dropdown.OptionData(titleMatch.Item2));
            }

            courseDropdown.value = 0;
            courseDropdown.RefreshShownValue();

            // 중복 옵션 제거 및 업데이트
            RemoveDuplicateOptions(courseDropdown);
        }
      
    }

}