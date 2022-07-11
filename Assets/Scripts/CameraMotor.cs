using System;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform _lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void Start()
    {
        _lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // This is to check if we're inside the bounds on the X axis
        var deltaX = _lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < _lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }
        // This is to check if we're inside the bounds on the Y axis
        var deltaY = _lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < _lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
