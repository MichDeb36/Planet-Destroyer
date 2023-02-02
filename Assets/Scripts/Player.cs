using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera playerHead;

    public void ResetPosition()
    {
        float rotationAngleY = startingPoint.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngleY, 0);

        Vector3 differenceVector = startingPoint.position - playerHead.transform.position;
        player.transform.position += differenceVector;
    }
}
