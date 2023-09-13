using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AudioControl : MonoBehaviourPun
{
    private AudioSource myAudioSource;
    private AudioListener myAudioListener;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAudioListener = GetComponent<AudioListener>();

        if (myAudioListener != null)
        {
            myAudioListener.enabled = photonView.IsMine;
        }

        if (photonView.IsMine && myAudioSource != null)
        {
            myAudioSource.Play();
        }
    }
}
