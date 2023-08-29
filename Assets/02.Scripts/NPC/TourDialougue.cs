using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using PixelCrushers.DialogueSystem;

public class TourDialougue : MonoBehaviour
{
    private DatabaseReference databaseRef;
    DialogueSystemController dialogueSystemController;

    public void OnExecute() 
    {
        // GameObject가 활성화 되어있는지 확인
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }

        string localTag = DialogueLua.GetVariable("local").AsString;
        string conceptTag = DialogueLua.GetVariable("concept").AsString;
        string companionTag = DialogueLua.GetVariable("companion").AsString;
        string seasonTag = DialogueLua.GetVariable("season").AsString;

        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.Options.DatabaseUrl = new System.Uri("https://travir-1dadd-default-rtdb.firebaseio.com/");
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;  
        dialogueSystemController = FindObjectOfType<DialogueSystemController>();

        // 대화를 일시 중지
        PauseDialogue();

        // Coroutine으로 데이터 검색 시작
        StartCoroutine(SearchData(localTag, companionTag, seasonTag, conceptTag));
    }

    private void PauseDialogue()
    {
        if (dialogueSystemController != null)
        {
            dialogueSystemController.Pause();
        }
    }

    private void ResumeDialogue()
    {
        if (dialogueSystemController != null)
        {
            dialogueSystemController.Unpause();
        }
    }

    private IEnumerator SearchData(string local, string companion = null, string season = null, string concept = null)
    {
        string path = "tour/" + local;
        var task = databaseRef.Child(path).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.LogError("Failed to retrieve data: " + task.Exception);
            yield break;
        }

        DataSnapshot snapshot = task.Result;
        Dictionary<string, object> data = (Dictionary<string, object>)snapshot.Value;
        List<(int, string, string)> matchCountTitleAndURLs = new List<(int, string, string)>();

        foreach (KeyValuePair<string, object> entry in data)
        {
            string pushId = entry.Key;
            Dictionary<string, object> details = (Dictionary<string, object>)entry.Value;
            string title = details.ContainsKey("title") ? (string)details["title"] : null;
            string url = details.ContainsKey("url") ? (string)details["url"] : null;
            Dictionary<string, object> tag = details.ContainsKey("tag") ? (Dictionary<string, object>)details["tag"] : null;

            int matchCount = 0;
            if (tag != null)
            {
                if (companion == null || tag.ContainsKey("companion") && tag["companion"].ToString() == companion)
                    matchCount++;
                if (season == null || tag.ContainsKey("season") && tag["season"].ToString() == season)
                    matchCount++;
                if (concept == null || tag.ContainsKey("concept") && tag["concept"].ToString() == concept)
                    matchCount++;
            }

            matchCountTitleAndURLs.Add((matchCount, title, url));
        }

        matchCountTitleAndURLs.Sort((x, y) => y.Item1.CompareTo(x.Item1));
        for (int i = 0; i < 3 && i < matchCountTitleAndURLs.Count; i++)
        {
            Debug.Log("Title: " + matchCountTitleAndURLs[i].Item2);
            Debug.Log("URL: " + matchCountTitleAndURLs[i].Item3);
            DialogueLua.SetVariable("result"+i, matchCountTitleAndURLs[i].Item2);
            DialogueLua.SetVariable("link"+i, matchCountTitleAndURLs[i].Item3);
        }

        ResumeDialogue(); 
    }
}

//     private void SearchData(string local, string companion = null, string season = null, string concept = null)
//     {
//         // 데이터베이스 경로 설정
//         string path = "tour/" + local;

//         // 데이터 검색
//         databaseRef.Child(path).GetValueAsync().ContinueWith(task =>
//         {
//             if (task.IsFaulted)
//             {
//                 Debug.LogError("Failed to retrieve data: " + task.Exception);
//                 return;
//             }

//             DataSnapshot snapshot = task.Result;
//             Dictionary<string, object> data = (Dictionary<string, object>)snapshot.Value;

//             // 제목과 일치하는 필드 개수를 저장할 리스트 초기화
//             List<(int, string)> titleMatches = new List<(int, string)>();

//             // 데이터 순회
//             foreach (KeyValuePair<string, object> entry in data)
//             {
//                 string pushId = entry.Key;
//                 Dictionary<string, object> details = (Dictionary<string, object>)entry.Value;

//                 // 필드 값 가져오기
//                 string title = details.ContainsKey("title") ? (string)details["title"] : null;
//                 Dictionary<string, object> tag = details.ContainsKey("tag") ? (Dictionary<string, object>)details["tag"] : null;

//                 // 매칭되는 필드 개수 세기
//                 int matchCount = 0;
//                 if (tag != null)
//                 {
//                     if (companion == null || tag.ContainsKey("companion") && tag["companion"].ToString() == companion)
//                         matchCount++;
//                     if (season == null || tag.ContainsKey("season") && tag["season"].ToString() == season)
//                         matchCount++;
//                     if (concept == null || tag.ContainsKey("concept") && tag["concept"].ToString() == concept)
//                         matchCount++;
//                 }

//                 // (일치 개수, 제목) 튜플을 리스트에 추가
//                 titleMatches.Add((matchCount, title));
//             }

//             // 가장 많이 일치하는 제목을 기준으로 정렬
//             titleMatches.Sort((x, y) => y.Item1.CompareTo(x.Item1));

//             for (int i = 0; i < 3 && i < titleMatches.Count; i++)
//             {
//                 Debug.Log(titleMatches[i].Item2);
//                 DialogueLua.SetVariable("result"+i, titleMatches[i].Item2);
//             }
//         });
//     }
// }




// using System.Collections.Generic;
// using System;
// using UnityEngine;
// using Firebase;
// using Firebase.Database;
// using Firebase.Unity;

// public class TourDialougue : MonoBehaviour
// {
//     private DatabaseReference databaseRef;

//     private void Start()
//     {
//         // Firebase 초기화
//         FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
//         {
//             if (task.IsCompleted)
//             {
//                 FirebaseApp app = FirebaseApp.DefaultInstance;
//                 app.Options.DatabaseUrl = new System.Uri("https://travir-1dadd-default-rtdb.firebaseio.com/");
//                 databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

//                 // 예시 호출
//                 SearchData("서울", "가족", "봄", "문화");
//             }
//             else
//             {
//                 Debug.LogError("Failed to initialize Firebase: " + task.Exception);
//             }
//         });
//     }

//     private void SearchData(string local, string companion = null, string season = null, string concept = null)
//     {
//         // 데이터베이스 경로 설정
//         string path = "tour/" + local;

//         // 데이터 검색
//         databaseRef.Child(path).GetValueAsync().ContinueWith(task =>
//         {
//             if (task.IsFaulted)
//             {
//                 Debug.LogError("Failed to retrieve data: " + task.Exception);
//                 return;
//             }

//             DataSnapshot snapshot = task.Result;
//             Dictionary<string, object> data = (Dictionary<string, object>)snapshot.Value;

//             // 제목과 일치하는 필드 개수를 저장할 리스트 초기화
//             List<(int, string)> titleMatches = new List<(int, string)>();

//             // 데이터 순회
//             foreach (KeyValuePair<string, object> entry in data)
//             {
//                 string pushId = entry.Key;
//                 Dictionary<string, object> details = (Dictionary<string, object>)entry.Value;

//                 // 필드 값 가져오기
//                 string title = details.ContainsKey("title") ? (string)details["title"] : null;
//                 Dictionary<string, object> tag = details.ContainsKey("tag") ? (Dictionary<string, object>)details["tag"] : null;

//                 // 매칭되는 필드 개수 세기
//                 int matchCount = 0;
//                 if (tag != null)
//                 {
//                     if (companion == null || tag.ContainsKey("companion") && tag["companion"].ToString() == companion)
//                         matchCount++;
//                     if (season == null || tag.ContainsKey("season") && tag["season"].ToString() == season)
//                         matchCount++;
//                     if (concept == null || tag.ContainsKey("concept") && tag["concept"].ToString() == concept)
//                         matchCount++;
//                 }

//                 // (일치 개수, 제목) 튜플을 리스트에 추가
//                 titleMatches.Add((matchCount, title));
//             }

//             // 가장 많이 일치하는 제목을 기준으로 정렬
//             titleMatches.Sort((x, y) => y.Item1.CompareTo(x.Item1));

//             // 결과 출력
//             Debug.Log("Matching Titles:");
//             for (int i = 0; i < 3 && i < titleMatches.Count; i++)
//             {
//                 Debug.Log(titleMatches[i].Item2);
//             }
//         });
//     }
// }
