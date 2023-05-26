using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReviewCanvasActive : MonoBehaviour, IPointerClickHandler
{
    public GameObject reviewCanvas; 
    public GameObject reviewlistScreen;
    public GameObject addreviewScreen;
    public GameObject closeButton;

    private bool canvasShown = false; // 팝업창이 표시되었는지 여부를 추적하는 변수
    //private bool canInteract = true;

    private void OnMouseUp()
    {

        if (!canvasShown)
        {
            if (reviewCanvas != null && reviewCanvas.activeSelf == false)
            {
                reviewCanvas.SetActive(true);
                reviewlistScreen.SetActive(true);
                addreviewScreen.SetActive(false);
                canvasShown = true;
                //canInteract = false;
            }
        }
        else
        {
            reviewCanvas.SetActive(false);
            reviewlistScreen.SetActive(false);
            addreviewScreen.SetActive(false);
            canvasShown = false; // 팝업창이 이미 표시된 경우 다시 클릭할 수 있도록 상태를 재설정
            //canInteract = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭된 UI 요소가 closeButton인지 확인합니다.
        if (eventData.pointerCurrentRaycast.gameObject == closeButton)
        {
            reviewCanvas.SetActive(false);
            canvasShown = false;
            //canInteract = true;
        }
    }

  /*  private void Update()
    {
        if (!canInteract)
        {
            // 리뷰 캔버스가 활성화된 상태에서는 리뷰 NPC의 클릭 이벤트를 무효화합니다.
            canInteract = false;
        }
    }*/

}