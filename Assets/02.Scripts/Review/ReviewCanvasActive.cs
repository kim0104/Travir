
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class ReviewCanvasActive : MonoBehaviour
{
    public GameObject reviewCanvas;
    public GameObject reviewlistScreen;
    public GameObject addreviewScreen;
    public Button closeButton;

    private bool canvasShown = false;
    private bool canInteract = true;

    private PlayerController playerController;

    private void Start()
    {
        reviewCanvas.SetActive(false);
        closeButton.onClick.AddListener(CloseCanvas);
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnMouseUp()
    {
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

            if (playerController != null)
            {
                playerController.enabled = false; 
            }
        }
    }

    private void CloseCanvas()
    {
        reviewCanvas.SetActive(false);
        canvasShown = false;
        canInteract = true;

        
        if (playerController != null)
        {
            playerController.enabled = true; 
        }

    }
}
