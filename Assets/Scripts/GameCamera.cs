using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Transform target;


    private Camera gameCamera;

    private void Awake()
    {
        gameCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        var pos = target.position + offset;

        pos.y = (pos.y < 0) ? 0 : pos.y;

        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
