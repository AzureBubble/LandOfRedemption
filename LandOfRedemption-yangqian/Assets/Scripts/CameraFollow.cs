using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        var pos = _camera.transform.position;
        pos.x = player.position.x;
        pos.y = player.position.y + 3;
        transform.position = pos;
    }
}
