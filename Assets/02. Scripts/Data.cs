using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static SpawnType spawnType = SpawnType.Basic;
    public enum SpawnType
    {
        Basic,
        Seoul,
        Jeju,
        Gimpo,
        Lotteworld,
        Lottetower,
        Namsan,
        Gwanghwa,
        Worldcup
    }
}
