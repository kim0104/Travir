using UnityEngine;
using Photon.Pun;

public class NPCRotation : MonoBehaviourPunCallbacks
{
    private PlayerController playerController;
    public Canvas myCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void RotateNPC(float angle)
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y = angle;
        transform.eulerAngles = currentRotation;
    }

    public void SetPosition()
    {
        // Vector3 newPosition = new Vector3(271f, 13f, 137f); // 변경하고자 하는 새로운 위치 좌표
        if (playerController != null)
        {
            playerController.enabled = false; 
        }
    }

    public void ResetPosition()
    {
        if (playerController != null)
        {
            playerController.enabled = true; 
        }
    }

    public void StartConversation()
    {
        myCanvas.enabled = false;
    }

    public void EndConversation()
    {
        myCanvas.enabled = true;
    }

}

