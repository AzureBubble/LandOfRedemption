using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 相机新位置
    private float newX, newY;
    //获取被监视对象
    private GameObject moniteredObject;
    [Header("Camera Moving Regulation")]
    [Tooltip("跟踪对象名")]
    [SerializeField]
    private string moniteredRoleName;
    [Tooltip("相机移动速度")]
    [SerializeField]
    private float cameraSpeed;
    //相机发生变化时与主角的偏移距离
    private float offset;
    //相机长宽比,默认16：9
    private float scale;


    // Start is called before the first frame update
    void Start()
    {
        this.newX = this.transform.position.x;
        this.newY = this.transform.position.y;
        this.offset = GetComponent<Camera>().orthographicSize;
        this.scale = GetComponent<Camera>().aspect;
        CheckMoniteredObject();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoniteredObject();
        CheckCameraPosition();
        CameraMove();
    }

    void CheckMoniteredObject()
    {
        if (!moniteredObject)
        {
            GameObject obj = GameObject.Find(moniteredRoleName);
            moniteredObject = obj;
        }
    }

    void CheckCameraPosition()
    {
        float roleX = moniteredObject.transform.position.x;
        float roleY = moniteredObject.transform.position.y;

        if (roleX - newX > offset * scale)
        {
            newX += offset * scale * 2;
        }
        else if (newX - roleX > offset * scale)
        {
            newX -= offset * scale * 2;
        }
        if (roleY - newY > offset)
        {
            newY += offset * 2;
        }
        else if (newY - roleY > offset)
        {
            newY -= offset * 2;
        }
    }

    void CameraMove()
    {
        float distanceX = Time.deltaTime * cameraSpeed * scale;
        float distanceY = Time.deltaTime * cameraSpeed;
        Vector3 newPos = this.transform.position;
        newPos.x = Mathf.Lerp(this.transform.position.x, newX, distanceX);
        newPos.y = Mathf.Lerp(this.transform.position.y, newY, distanceY);
        this.transform.position = newPos;
    }
}

