using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDOTAFollow : MonoBehaviour
{

    public GameObject TargetCamera;
    public GameObject[] TargetObject;
    public float Delay = 0.1f;
    Vector3 targetDistance;
    Vector3 firstPosition;

    // Use this for initialization
    void Awake()
    {
        firstPosition = TargetCamera.transform.position;
    }

    void Start()
    {
        Invoke("SetFirstPosition", Delay);
    }

    void SetFirstPosition()
    {
        TargetCamera.transform.position = firstPosition;
        for (int i = 0; i < TargetObject.Length; i++)
        {
            if (TargetObject[i].activeSelf)
            {
                targetDistance = TargetCamera.transform.position - TargetObject[i].transform.position;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < TargetObject.Length; i++)
        {
            if (TargetObject[i].activeSelf)
            {
                TargetCamera.transform.position = TargetObject[i].transform.position + targetDistance;
            }
        }
    }
}
