

using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public Button sendBtn;
    public Text chatLog;
    public InputField input;
    ScrollRect scroll_rect = null;
    string chatters;

    private PlayerController playerController;
    private MenuPanel menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        scroll_rect = GameObject.FindObjectOfType<ScrollRect>();

        playerController = FindObjectOfType<PlayerController>();
        menuPanel = FindObjectOfType<MenuPanel>();
    }

    public void SendButtonOnClicked()
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        input.ActivateInputField();
        input.text = "";
    }

    void Update()
    {
        // InputField에 커서가 깜빡이는 동안에만 PlayerController 비활성화
        if (Input.GetKeyDown(KeyCode.Return) && !input.isFocused)
        {
            SendButtonOnClicked();
            DisablePlayerController();
        }
        else if (input.isFocused)
        {
            DisablePlayerController();
        }
        else
        {
            EnablePlayerController();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string locationInKorean = GetLocationInKorean(currentSceneName);
        string msg = string.Format("{0}님이 {1}에 들어오셨습니다.", newPlayer.NickName, locationInKorean);
        chatLog.text += "\n" + msg;
        scroll_rect.verticalNormalizedPosition = 0.0f;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string locationInKorean = GetLocationInKorean(currentSceneName);
        string msg = string.Format("{0}님이 {1}에서 나가셨습니다.", otherPlayer.NickName, locationInKorean);
        chatLog.text += "\n" + msg;
        scroll_rect.verticalNormalizedPosition = 0.0f;
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        // 25글자마다 줄바꿈 및 양쪽에 공백 추가
        string formattedMessage = InsertLineBreaks(msg, 25, " ", " ");
        chatLog.text += "\n" + formattedMessage;
        

        // Content의 크기 조절
        LayoutRebuilder.ForceRebuildLayoutImmediate(scroll_rect.content);

        scroll_rect.verticalNormalizedPosition = 0.0f;
    }

    // InsertLineBreaks 함수에서 양쪽 공백 추가
    string InsertLineBreaks(string input, int maxLength, string leftPadding, string rightPadding)
    {
        string result = "";

        for (int i = 0; i < input.Length; i += maxLength)
        {
            int length = Mathf.Min(maxLength, input.Length - i);
            result += leftPadding + input.Substring(i, length) + rightPadding;

            if (i + length < input.Length)
            {
                result += "\n";
            }
        }

        return result;
    }

    private void DisablePlayerController()
    {
        if (playerController != null && menuPanel != null)
        {
            playerController.enabled = false;
            menuPanel.enabled = false;
        }
    }

    private void EnablePlayerController()
    {
        if (playerController != null && menuPanel != null)
        {
            playerController.enabled = true; 
            menuPanel.enabled = true;
        }
    }

    private string GetLocationInKorean(string sceneName)
    {
        switch(sceneName)
        {
            case "Jeju":
                return "제주";
            case "Seoul":
                return "서울";
            default:
                return sceneName; 
        }
    }
}

