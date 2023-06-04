using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCam : MonoBehaviour
{
    public Transform Player;
    public float mouseSmoothTime = 0.03f;

    private Vector3 currentCamera = Vector3.zero;
    private Vector3 currentCameraDeltaVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        currentCamera = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCameraDelta = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        currentCamera = Vector3.SmoothDamp(currentCamera, targetCameraDelta, ref currentCameraDeltaVelocity, mouseSmoothTime);
     
        transform.position = currentCamera;
    }
}
