using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLimit : MonoBehaviour
{
    [SerializeField]
    private float maxXMovement;
    [SerializeField]
    private float maxYMovement;
    private void Update()
    {
        if (transform.localPosition.x >= maxXMovement)
        {
            transform.localPosition = new Vector3(maxXMovement, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.x <= -maxXMovement)
        {
            transform.localPosition = new Vector3(-maxXMovement, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.y >= maxYMovement)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, maxYMovement, transform.localPosition.z);
        }
        if (transform.localPosition.y <= -maxYMovement)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -maxYMovement, transform.localPosition.z);
        }
    }
    private void OnEnable()
    {
        transform.position = Vector3.zero;
    }
}
