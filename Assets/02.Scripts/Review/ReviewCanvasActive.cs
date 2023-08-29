
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

    private void Start()
    {
        reviewCanvas.SetActive(false);
        closeButton.onClick.AddListener(CloseCanvas);

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

            PlayerController playerController = FindObjectOfType<PlayerController>();
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

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = true; 
        }

    }
}
