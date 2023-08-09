using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanelManager : MonoBehaviour
{
    public Camera cam; // ī�޶� ���⿡ �Ҵ��ϼ���.
    //public TMP_Text uiText; // ������ UI �ؽ�Ʈ�� ���⿡ �Ҵ��ϼ���.
    public LayerMask panelLayer; // "chat" ���̾ ���⿡ �Ҵ��ϼ���.
    public GameObject infoText;
    public GameObject infoImage;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� ������ ��
        {
            if(cam != null)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, panelLayer)) // ���̰� "chat" ���̾��� ��ü�� ����� ��
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
