using UnityEngine;

public class NPCRotation : MonoBehaviour
{

    public void RotateNPC(float angle)
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y = angle;
        transform.eulerAngles = currentRotation;
    }
}
