using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform spawnTr;
    public Transform subwayTr;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = FindAnyObjectByType<CharacterController>().gameObject;
        switch (Data.spawnType)
        {
            case Data.SpawnType.Basic:
                go.transform.position = spawnTr.position;
                break;
            case Data.SpawnType.Subway:
                go.transform.position = subwayTr.position;
                break;
            default:
                go.transform.position = spawnTr.position;
                break;
        }
    }
}
