using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JejuMapManager : MonoBehaviour
{
    public Transform jejuairportTr;
    public Transform museumTr;
    public Transform jeoliTr;
    public Transform seongsanTr;
    public Transform mandarinTr;
    public Transform hanraTr;

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
                case Data.SpawnType.Jejuairport:
                    player.transform.position = jejuairportTr.position;
                    break;
                case Data.SpawnType.Museum:
                    player.transform.position = museumTr.position;
                    break;
                case Data.SpawnType.Jeoli:
                    player.transform.position = jeoliTr.position;
                    break;
                case Data.SpawnType.Seongsan:
                    player.transform.position = seongsanTr.position;
                    break;
                case Data.SpawnType.Mandarin:
                    player.transform.position = mandarinTr.position;
                    break;
                case Data.SpawnType.Hanra:
                    player.transform.position = hanraTr.position;
                    break;
                default:
                    break;
            }
        }
    }

}
