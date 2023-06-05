using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReviewCanvasActive : MonoBehaviour, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canvasShown && canInteract)
        {
            reviewCanvas.SetActive(true);
            reviewlistScreen.SetActive(true);
            addreviewScreen.SetActive(false);
            canvasShown = true;
            canInteract = false;
        }
    }

    private void CloseCanvas()
    {
        reviewCanvas.SetActive(false);
        canvasShown = false;
        canInteract = true;
    }
}
