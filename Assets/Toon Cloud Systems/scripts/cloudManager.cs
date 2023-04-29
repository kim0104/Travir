using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudManager : MonoBehaviour
{
    public ParticleSystem cloudGeneratorFx;
     

     void Start()
     {
        Generate();
     }
    void Generate()
    {
        cloudGeneratorFx.Play();
    }

    
}