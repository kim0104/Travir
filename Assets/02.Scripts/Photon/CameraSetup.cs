using Cinemachine; // 시네머신 관련 코드
using Photon.Pun; // PUN 관련 코드
using UnityEngine;

// 시네머신 카메라가 로컬 플레이어를 추적하도록 설정
public class CameraSetup : MonoBehaviourPun
{
    public Camera playerCamera;
    public GameObject nameTag;
    // public Transform nameTagTransform;

    void Start()
    {
        // 로컬 플레이어만 카메라 활성화
        playerCamera.enabled = photonView.IsMine;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            // Make the name tag face the camera
            nameTag.transform.LookAt(nameTag.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

}