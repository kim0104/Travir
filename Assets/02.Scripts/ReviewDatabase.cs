/*using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseDataViewer : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Text itemTemplate;

    private DatabaseReference databaseReference;

    private void Start()
    {
        // Firebase 초기화
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://travir-1dadd-default-rtdb.firebaseio.com/"
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // 데이터 가져오기
        databaseReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // 가져온 데이터를 처리하고 캔버스에 출력하는 함수 호출
                ProcessData(snapshot);
            }
        });
    }

    private void ProcessData(DataSnapshot snapshot)
    {
        // Scroll View의 Content 오브젝트 참조
        Transform contentTransform = scrollRect.content.transform;

        // 각 데이터 항목을 순회하면서 UI에 출력
        foreach (DataSnapshot childSnapshot in snapshot.Children)
        {
            // 데이터 값 가져오기
            string dataValue = childSnapshot.Value.ToString();

            // 새로운 UI 텍스트 오브젝트 생성
            Text newItem = Instantiate(itemTemplate, contentTransform);
            newItem.text = dataValue;
        }
    }
}
*/