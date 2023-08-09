using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastToButton : MonoBehaviour, IPointerClickHandler
{
    public Camera cam; // 카메라를 여기에 할당하세요.
    public TextMeshProUGUI uiText; // 변경할 UI 텍스트를 여기에 할당하세요.
    public LayerMask chatLayer; // "chat" 레이어를 여기에 할당하세요.
    public GameObject[] goArr = null;
    private int index = 0;

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 눌렀을 때
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // 레이저를 쐈을 때 부딪힌 오브젝트를 의미함

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, chatLayer)) // 레이가 "chat" 레이어의 객체에 닿았을 때
            {
                uiText.text = "Chat object was clicked!"; // UI 텍스트 변경

            }
        }*/

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiText.text = "hello";

        goArr[index].SetActive(false);
        index++;
        if(index > goArr.Length-1)
        {
            index = 0;
        }
        goArr[index].SetActive(true);

    }
}
