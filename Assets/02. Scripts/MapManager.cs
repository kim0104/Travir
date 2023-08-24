using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform seoulTr;
    public Transform jejuTr;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = FindAnyObjectByType<PlayerController>().gameObject;
        switch (Data.spawnType)
        {
            case Data.SpawnType.Seoul:
                go.transform.position = seoulTr.position;
                break;
            case Data.SpawnType.Jeju:
                go.transform.position = jejuTr.position;
                break;
            default:
                break;
        }
    }
}
