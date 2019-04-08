using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMedia9
{

    public class ClickEffect : MonoBehaviour
    {
        public enum CClickType { LeftMouse, MiddleMouse, RightMouse, Touch }
        public CClickType ClickTrigger;
        public Camera MainCamera;
        public GameObject TargetEffect;
        public int DestroyDelay = 5;

        Vector3 Destination;
        RaycastHit raycastHit;
        Ray ray;

        [Header("Debug Variable")]
        public string RaycastTag;
        public string RayObjectTag;

        KeyCode GetTriggerKey()
        {
            KeyCode Result = KeyCode.None;
            if (ClickTrigger == CClickType.LeftMouse) Result = KeyCode.Mouse0;
            if (ClickTrigger == CClickType.RightMouse) Result = KeyCode.Mouse1;
            if (ClickTrigger == CClickType.MiddleMouse) Result = KeyCode.Mouse2;
            if (ClickTrigger == CClickType.Touch) Result = KeyCode.Mouse0;
            return Result;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(GetTriggerKey()))
            {
                ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit))
                {
                    Destination = raycastHit.point;
                    
                    GameObject temp = GameObject.Instantiate(TargetEffect, raycastHit.point, raycastHit.transform.rotation);
                    Destroy(temp, DestroyDelay);
                    RaycastTag = raycastHit.collider.tag;
                    RayObjectTag = raycastHit.collider.gameObject.name;
                }
            }
        }
    }

}
