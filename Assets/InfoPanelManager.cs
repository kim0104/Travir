using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanelManager : MonoBehaviour
{
    public Camera cam; // 카메라를 여기에 할당하세요.
    //public TMP_Text uiText; // 변경할 UI 텍스트를 여기에 할당하세요.
    public LayerMask panelLayer; // "chat" 레이어를 여기에 할당하세요.
    public GameObject infoText;
    public GameObject infoImage;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 눌렀을 때
        {
            if(cam != null)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, panelLayer)) // 레이가 "chat" 레이어의 객체에 닿았을 때
                {
                    infoImage.SetActive(!infoImage.activeSelf);
                    infoText.SetActive(!infoText.activeSelf);
                }

            }
            else
            {
                cam = Camera.main;
            }
        }
    }
}
