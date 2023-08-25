using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        switch (Data.spawnType)
        {
            case Data.SpawnType.Gimpo:
                player.transform.position = gimpoTr.position;
                break;         
            case Data.SpawnType.Lotteworld:
                transform.position = lotteworldTr.position;
                break;
            case Data.SpawnType.Lottetower:
                transform.position = lottetowerTr.position;
                break;
            case Data.SpawnType.Namsan:
                transform.position = namsanTr.position;
                break;
            case Data.SpawnType.Gwanghwa:
                transform.position = gwanghwaTr.position;
                break;
            case Data.SpawnType.Worldcup:
                transform.position = worldcupTr.position;
                break;
            default:
                break;
        }
    }

}

