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

using System.Collections;
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
}