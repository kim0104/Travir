using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class SeoulMapManager : MonoBehaviour
{
    public Transform gimpoTr;
    public Transform lotteworldTr;
    public Transform lottetowerTr;
    public Transform namsanTr;
    public Transform gwanghwaTr;
    public Transform worldcupTr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangePlayerPosition(GameObject player)
    {
        PhotonView photonView = player.GetComponent<PhotonView>();

        if (photonView != null && photonView.IsMine)
        {
            switch (Data.spawnType)
            {
                case Data.SpawnType.Gimpo:
                    player.transform.position = gimpoTr.position;
                    break;         
                case Data.SpawnType.Lotteworld:
                    player.transform.position = lotteworldTr.position;
                    break;
                case Data.SpawnType.Lottetower:
                    player.transform.position = lottetowerTr.position;
                    break;
                case Data.SpawnType.Namsan:
                    player.transform.position = namsanTr.position;
                    break;
                case Data.SpawnType.Gwanghwa:
                    player.transform.position = gwanghwaTr.position;
                    break;
                case Data.SpawnType.Worldcup:
                    player.transform.position = worldcupTr.position;
                    break;
                default:
                    break;
            }
        }
    }

}

