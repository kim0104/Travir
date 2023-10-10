using UnityEngine;
using UnityEngine.UI;
using Vuplex.WebView;
using PixelCrushers.DialogueSystem;

public class CanvasWebviewLoader : MonoBehaviour {

    [SerializeField]
    private CanvasWebViewPrefab webViewPrefab;

    public void OnExecute() {
        // 대화 데이터베이스에서 저장된 링크를 가져옵니다.
        webViewPrefab.InitialUrl = "";
        string link = DialogueLua.GetVariable("result").AsString;
    
        // WebViewPrefab을 추가합니다.
        if (webViewPrefab == null) {
            webViewPrefab = gameObject.AddComponent<CanvasWebViewPrefab>();
        }
        Debug.Log(link);
        webViewPrefab.InitialUrl = link;
        webViewPrefab.WebView.LoadUrl(link);
    }

}

