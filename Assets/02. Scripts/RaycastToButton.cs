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
    public Camera cam; // ī�޶� ���⿡ �Ҵ��ϼ���.
    public TextMeshProUGUI uiText; // ������ UI �ؽ�Ʈ�� ���⿡ �Ҵ��ϼ���.
    public LayerMask chatLayer; // "chat" ���̾ ���⿡ �Ҵ��ϼ���.
    public GameObject[] goArr = null;
    private int index = 0;

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� ������ ��
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; // �������� ���� �� �ε��� ������Ʈ�� �ǹ���

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, chatLayer)) // ���̰� "chat" ���̾��� ��ü�� ����� ��
            {
                uiText.text = "Chat object was clicked!"; // UI �ؽ�Ʈ ����

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
