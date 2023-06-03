using UnityEngine;

public class NPCRotation : MonoBehaviour
{

    public Transform targetObject;

    public void RotateNPC(float angle)
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y = angle;
        transform.eulerAngles = currentRotation;
    }

    public void SetNPCPosition()
    {
        Vector3 newPosition = new Vector3(271f, 13f, 137f); // 변경하고자 하는 새로운 위치 좌표
        targetObject.position = newPosition;
    }
}
