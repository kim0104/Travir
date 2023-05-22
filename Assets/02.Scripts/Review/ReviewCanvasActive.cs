using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewCanvasActive : MonoBehaviour
{
    public GameObject reviewCanvas;

    private bool canvasShown = false; // 캔버스가 뜨도록 상태를 추적하는 변수

    private void OnMouseDown()
    {
        if (!canvasShown)
        {
            if (reviewCanvas != null)
            {
                reviewCanvas.SetActive(true);
                canvasShown = true;
            }
        }
        else
        {
            if (reviewCanvas != null)
            {
                reviewCanvas.SetActive(false);
                canvasShown = false;
            }
        }
    }
}