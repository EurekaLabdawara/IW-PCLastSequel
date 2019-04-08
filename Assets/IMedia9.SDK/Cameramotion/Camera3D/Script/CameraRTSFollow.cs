using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRTSFollow : MonoBehaviour
{

    public GameObject TargetCamera;
    public bool autoPan;
    public Rect ScreenConstraint;
    public float CameraSpeed = 5;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (autoPan)
        {
            if (Input.mousePosition.x < ScreenConstraint.xMin)
            {
                this.transform.position += (Vector3.left * CameraSpeed * Time.deltaTime);
            }
            if (Input.mousePosition.x > ScreenConstraint.xMax)
            {
                this.transform.position += (Vector3.right * CameraSpeed * Time.deltaTime);
            }
            if (Input.mousePosition.y < ScreenConstraint.yMin)
            {
                this.transform.position += (Vector3.back * CameraSpeed * Time.deltaTime);
            }
            if (Input.mousePosition.y > ScreenConstraint.yMax)
            {
                this.transform.position += (Vector3.forward * CameraSpeed * Time.deltaTime);
            }
        }
    }
}
