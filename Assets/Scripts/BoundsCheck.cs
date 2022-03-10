using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// предотвращает выход корабля за границы экрана.
/// работает только с ортографической камерой в позиции [0,0,0]
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    public float radius = 1f;
    [SerializeField] bool keepOnScreen = true;

    public bool isOnScreen = true;
    public float camHeight;
    public float camWidth;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }

        if (pos.y > camWidth - radius)
        {
            pos.y = camWidth - radius;
            offUp = true;
        }
        if (pos.y < -camWidth + radius)
        {
            pos.y = -camWidth + radius;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
