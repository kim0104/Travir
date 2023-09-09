/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public GameObject m_Content;
    public InputField m_inputField;

    PhotonView photonview;

    GameObject m_ContentText;

    string m_strUserName;

    //플레이어의 닉네임을 전달받음
    public void SetPlayerName(string playerName)
    {
        m_strUserName = playerName;
    }

    void Start()
    {
        m_ContentText = m_Content.transform.GetChild(0).gameObject;
        photonview = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && m_inputField.isFocused == false)
        {
            m_inputField.ActivateInputField();
        }
    }
    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
   

        int nRandomKey = Random.Range(0, 100);

        m_strUserName = "user" + nRandomKey;

        PhotonNetwork.LocalPlayer.NickName = m_strUserName;
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null);
    }

    public override void OnJoinedRoom()
    {
        AddChatMessage(PhotonNetwork.LocalPlayer.NickName+"님이 입장하였습니다.");
    }

    public void OnEndEditEvent()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (string.IsNullOrEmpty(m_inputField.text))
                return;

            string strMessage = m_strUserName + " : " + m_inputField.text;
            photonview.RPC("RPC_Chat", RpcTarget.All, strMessage);
            m_inputField.text = "";
        }
    }

    // 사용자가 나갈 때 호출되는 메서드
    public void OnLeaveButtonClicked()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    // 플레이어가 방을 나갔을 때 호출되는 콜백
    public override void OnLeftRoom()
    {
        AddChatMessage(m_strUserName + "님이 나갔습니다.");
    }

    void AddChatMessage(string message)
    {
        GameObject goText = Instantiate(m_ContentText, m_Content.transform);

        goText.GetComponent<TextMeshProUGUI>().text = message;
        m_Content.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

    }

    [PunRPC]
    void RPC_Chat(string message)
    {
        AddChatMessage(message);
    }

}

*/

/*using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public Text chatText;
    public InputField chatInput;
    private PhotonView chatPhotonView;

    public GameObject itemsGameObject; // 아이템들을 담고 있는 게임 오브젝트


    private void Awake()
    {
        chatPhotonView = GetComponent<PhotonView>(); 
    }

    private void Start()
    {
        chatInput.onEndEdit.AddListener(SendChatMessageOnEnter);

        // itemsGameObject 초기화
        itemsGameObject = GameObject.Find("Items (Drag Items Here)"); // 예시
        if (itemsGameObject == null)
        {
            Debug.LogError("Items GameObject not found.");
        }

        // ChatManager 게임 오브젝트 활성화
        if (itemsGameObject != null)
        {
            itemsGameObject.SetActive(true);
        }
    }


    private void SendChatMessageOnEnter(string message)
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            SendChatMessage();
        }
    }

    public void SendChatMessage()
    {
        string message = chatInput.text;
        if (!string.IsNullOrEmpty(message))
        {
            chatPhotonView.RPC("SendChatMessageRPC", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, message);
            chatInput.text = string.Empty;
        }
    }

    [PunRPC]
    private void SendChatMessageRPC(string sender, string message)
    {
        string formattedMessage = $"{sender}: {message}\n";
        chatText.text += formattedMessage;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + "님이 입장하셨습니다.");
        chatText.text += $"{newPlayer.NickName}님이 입장했습니다.\n";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "님이 퇴장하셨습니다.");
        chatText.text += $"{otherPlayer.NickName}님이 퇴장했습니다.\n";
    }

}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public GameObject Content;
    public InputField InputField;

    PhotonView photonview;

    GameObject ChatText;

    string PlayerNickName;


    void Start()
    {
 
        ChatText = Content.transform.GetChild(0).gameObject;
        photonview = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && InputField.isFocused == false)
        {
            InputField.ActivateInputField();
        }
    }

    public override void OnJoinedRoom()
    {
        AddChatMessage("connect user : " + PhotonNetwork.LocalPlayer.NickName);
    }

    public void OnEndEditEvent()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string strMessage = PlayerNickName + " : " + InputField.text;

            photonview.RPC("RPC_Chat", RpcTarget.All, strMessage);
            InputField.text = "";
        }
    }

    void AddChatMessage(string message)
    {
        GameObject goText = Instantiate(ChatText, Content.transform);
        TextMeshProUGUI textMeshProUGUI = goText.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = message;

        Content.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        // 스크롤을 맨 아래로 이동
        Canvas.ForceUpdateCanvases(); // 캔버스 업데이트 강제 실행
        Content.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f; // 스크롤 위치 조정
    }


    [PunRPC]
    void RPC_Chat(string message)
    {
        AddChatMessage(message);
    }

    private IEnumerator EnableChatManagerCoroutine()
    {
        yield return new WaitForSeconds(1f); // 예시로 1초 대기

        if (Content != null)
        {
            Content.SetActive(true); // Content 게임 오브젝트 활성화
        }
        else
        {
            Debug.LogWarning("Content is null. Make sure to assign the GameObject reference.");
        }
    }
}*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public GameObject Content;
    public InputField InputField;

    PhotonView photonview;

    GameObject ChatText;

    string PlayerNickName;

    private bool enableContent = false; // Content 활성화 여부를 나타내는 변수


    void Start()
    {
        ChatText = Content.transform.GetChild(0).gameObject;
        photonview = GetComponent<PhotonView>();

        // Content를 활성화합니다.
        if (Content != null)
        {
            Content.SetActive(true);
            enableContent = true; // Content가 활성화되었음을 표시
        }
        else
        {
            Debug.LogWarning("Content is null. Make sure to assign the GameObject reference.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && InputField.isFocused == false)
        {
            InputField.ActivateInputField();
        }
    }

    public override void OnJoinedRoom()
    {
        AddChatMessage("connect user : " + PhotonNetwork.LocalPlayer.NickName);
    }

    public void OnEndEditEvent()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string strMessage = PlayerNickName + " : " + InputField.text;

            photonview.RPC("RPC_Chat", RpcTarget.All, strMessage);
            InputField.text = "";
        }
    }

    void AddChatMessage(string message)
    {
        if (!enableContent) return; // Content가 활성화되지 않았다면 더 이상 진행하지 않습니다.

        GameObject goText = Instantiate(ChatText, Content.transform);
        TextMeshProUGUI textMeshProUGUI = goText.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = message;

        Content.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        // 스크롤을 맨 아래로 이동
        Canvas.ForceUpdateCanvases(); // 캔버스 업데이트 강제 실행
        Content.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f; // 스크롤 위치 조정
    }


    [PunRPC]
    void RPC_Chat(string message)
    {
        AddChatMessage(message);
    }
}*/

using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public Button sendBtn;
    public Text chatLog;
    public InputField input;
    ScrollRect scroll_rect = null;
    string chatters;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        scroll_rect = GameObject.FindObjectOfType<ScrollRect>();

        playerController = FindObjectOfType<PlayerController>();
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
        string msg = string.Format("{0}님이 들어오셨습니다.", newPlayer.NickName);
        chatLog.text += "\n" + msg;
        scroll_rect.verticalNormalizedPosition = 0.0f;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        string msg = string.Format("{0}님이 나가셨습니다.", otherPlayer.NickName);
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
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }

    private void EnablePlayerController()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}

