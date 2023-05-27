using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotate : MonoBehaviour
{
    public PlayerCam playerCamera;
    public float xRotation;
    public float yRotaiton;
    public float mouseSmoothTime = 0.03f;

    Vector3 currentMouseDelta = Vector3.zero;
    Vector3 currentMouseDeltaVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xRotation = playerCamera.xRotation;
        yRotaiton = playerCamera.yRotaiton;

        Vector3 targetMouseDelta = new Vector3(xRotation, yRotaiton);
        currentMouseDelta = Vector3.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        transform.rotation = Quaternion.Euler(0, currentMouseDelta.y, 0); // 設定攝影機角度
    }
}
