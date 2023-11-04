
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using Photon.Pun;

public class ReviewCanvasActive : MonoBehaviourPunCallbacks
{
    public GameObject reviewCanvas;
    public GameObject reviewlistScreen;
    public GameObject addreviewScreen;
    public GameObject chatCanvas;
    public Button closeButton;

    private bool canvasShown = false;
    private bool canInteract = true;

    private PlayerController playerController;
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        reviewCanvas.SetActive(false);
        closeButton.onClick.AddListener(CloseCanvas);
    }

    private void OnMouseUp()
    {
       // playerController.enabled = false;
        if (DialogueManager.isConversationActive) // 대화가 진행 중인지 확인
        {
            return; // 대화 중이면 여기서 리턴
        }
        if (!canvasShown && canInteract)
        {
            reviewCanvas.SetActive(true);
            reviewlistScreen.SetActive(true);
            addreviewScreen.SetActive(false);

            canvasShown = true;
            canInteract = false;
            chatCanvas.SetActive(false);

           
            if (playerController != null)
            {
                playerController.enabled = false;
                Debug.Log("PlayerController is disabled.");

            }
        }
    }

    private void CloseCanvas()
    {
        reviewCanvas.SetActive(false);
        canvasShown = false;
        canInteract = true;
        chatCanvas.SetActive(true);

        if (playerController != null)
        {
            playerController.enabled = true;
            
        }

    }
}
