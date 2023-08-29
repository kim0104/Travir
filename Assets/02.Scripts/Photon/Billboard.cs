using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class Billboard : MonoBehaviourPun
{
    [SerializeField]
    private TextMesh textMesh;

    private void Start()
    {

    }
    
    private void Awake()
    {
        if (photonView.IsMine)
        {
            if(textMesh != null) // null 체크 추가
            {
                textMesh.text = PhotonNetwork.LocalPlayer.NickName;
            }
            textMesh.text = PhotonNetwork.NickName;
            textMesh.color = Color.white;
        }
        else
        {
            textMesh.text = photonView.Owner.NickName;
            textMesh.color = Color.red;
        }
     }

    // private void Update()
    // {
    //     if (!photonView.IsMine && playerCamera != null)
    //     {
    //         transform.rotation = playerCamera.transform.rotation;
    //     }
    // }
}
