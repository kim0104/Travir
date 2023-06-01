using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using System.Collections.Generic;
using System.Linq;

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

        searchButton.onClick.AddListener(SearchButtonClicked);

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

    public void OnDropdownValueChanged(Dropdown dropdown)
    {
        string local = localDropdown.options[localDropdown.value].text;
        string companion = companionDropdown.options[companionDropdown.value].text;
        string season = seasonDropdown.options[seasonDropdown.value].text;
        string concept = conceptDropdown.options[conceptDropdown.value].text;

        SearchData(local, companion, season, concept);

        RemoveDuplicateOptions(dropdown);
    }

    private void SearchButtonClicked()
    {
        if (courseDropdown != null)
        {
            courseDropdown.ClearOptions();
            courseDropdown.value = 0;
            courseDropdown.RefreshShownValue();
        }

        string local = localDropdown.options[localDropdown.value].text;
        string concept = conceptDropdown.options[conceptDropdown.value].text;
        string companion = companionDropdown.options[companionDropdown.value].text;
        string season = seasonDropdown.options[seasonDropdown.value].text;

        SearchData(local, companion, season, concept);
    }

    private void SearchData(string local, string companion = null, string season = null, string concept = null)
    {
        string path = "tour/" + local;

        databaseRef.Child(path).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to retrieve data: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Dictionary<string, object> data = snapshot.Value as Dictionary<string, object>;

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
            }
        });
    }
}