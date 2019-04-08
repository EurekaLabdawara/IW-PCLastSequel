using UnityEngine;
using System.Collections;

namespace IMedia9
{

    public class Mechanim3DClick : MonoBehaviour
    {
        public enum CMovementType { Click, Tap, Swipe }
        public enum CClickType { LeftMouse, MiddleMouse, RightMouse, Touch }
        public enum CMovementQuadran { None, UpLeft, UpRight, DownLeft, DownRight }


        public CharacterController TargetController;
        public Camera MainCamera;
        public CMovementType MovementType;
        public CClickType ClickTrigger;
        [HideInInspector]
        public CMovementQuadran MovementQuadran;

    
        public float MoveSpeed = 200;
        public float RotateSpeed = 5;
        public float jumpSpeed = 8.0F;
        public float gravity = 20.0F;
        public float StopDistance = 0.2f;
        private Vector3 moveDirection = Vector3.zero;
        [Space(10)]

        [Header("Canvas Skill Settings")]
        public bool isCanvasSkillEnabled;
        public Mechanim3DClickCanvasSkill CanvasSkill;

        [Header("Debug Variable")]
        public string RaycastTag;
        public string RayObjectTag;
        public int DownStatus;
        public float CurrentDistance;

        bool initialValue = true;
        bool isMoving;
        RaycastHit raycastHit;
        Ray ray;
        Vector3 Destination, FirstTouch, SecondTouch, Direction;
        public Vector3 RotationStatus;
        Vector3 delta, lastpos;

        KeyCode GetTriggerKey()
        {
            KeyCode Result = KeyCode.None;
            if (ClickTrigger == CClickType.LeftMouse) Result = KeyCode.Mouse0;
            if (ClickTrigger == CClickType.RightMouse) Result = KeyCode.Mouse1;
            if (ClickTrigger == CClickType.MiddleMouse) Result = KeyCode.Mouse2;
            if (ClickTrigger == CClickType.Touch) Result = KeyCode.Mouse0;
            return Result;
        }

        void Start()
        {
            isMoving = false;
            Destination = TargetController.transform.position;
            MovementQuadran = CMovementQuadran.None;
            if (isCanvasSkillEnabled)
            {
                CanvasSkill.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            if (MovementType == CMovementType.Tap || MovementType == CMovementType.Click)
            {

               moveDirection = Vector3.zero;
               if (TargetController.isGrounded)
               {

                    if (Input.GetKeyDown(GetTriggerKey()))
                    {
                        lastpos = Input.mousePosition;
                        if (isCanvasSkillEnabled)
                        {
                            CanvasSkill.gameObject.SetActive(true);
                        }
                    }
                    if (Input.GetKey(GetTriggerKey()))
                    {
                        delta = Input.mousePosition - lastpos;
                        RotationStatus = delta;
                        lastpos = Input.mousePosition;
                        Vector3 resultRotation = new Vector3(0, -RotationStatus.y, 0);
                        this.transform.Rotate(resultRotation);
                    }
                    if (Input.GetKeyUp(GetTriggerKey()))
                    {
                        RotationStatus = Vector3.zero;
                        if (isCanvasSkillEnabled)
                        {
                            CanvasSkill.gameObject.SetActive(false);
                        }
                    }

                    if (Input.GetKeyUp(GetTriggerKey())) { 
                        ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out raycastHit)) {
                            Destination = raycastHit.point;
                            Destination.y = TargetController.transform.position.y;
                            TargetController.transform.LookAt(Destination);

                            RaycastTag = raycastHit.collider.tag;
                            RayObjectTag = raycastHit.collider.gameObject.name;
                        }
                        initialValue = false;
                    }



                    if (!initialValue)
                    {
                        CurrentDistance = Vector3.Distance(Destination, TargetController.transform.position);
                        if (CurrentDistance > StopDistance)
                        {
                            moveDirection = transform.forward * MoveSpeed * Time.deltaTime;
                            TargetController.Move(moveDirection);
                            isMoving = true;
                        }
                        else
                        {
                            isMoving = false;

                        }
                    }
                    
                }

                    moveDirection.y -= gravity * Time.deltaTime;
                    TargetController.Move(moveDirection * Time.deltaTime);
                
            } else
            if (MovementType == CMovementType.Swipe)
            {
                if (TargetController.isGrounded)
                {

                    MovementQuadran = SwipeValue();
                    ray = MainCamera.ScreenPointToRay(SecondTouch);
                    if (Physics.Raycast(ray, out raycastHit))
                    {
                        Destination = raycastHit.point;
                        Destination.y = TargetController.transform.position.y;
                        TargetController.transform.LookAt(Destination);
                        moveDirection = transform.forward * MoveSpeed * Time.deltaTime;
                    }
                }

                moveDirection.y -= gravity * Time.deltaTime;
                TargetController.Move(moveDirection * Time.deltaTime);
            }
        }

        CMovementQuadran SwipeValue()
        {
            CMovementQuadran result = CMovementQuadran.None;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                switch (touch.phase) { 
                    case TouchPhase.Began:
                        FirstTouch = touch.position;
                    break;

                    case TouchPhase.Moved:
                        SecondTouch = touch.position;
                        Direction = SecondTouch - FirstTouch;
                    break;

                    case TouchPhase.Ended:
                        if (Direction.x >= 0 && Direction.y >= 0)
                        {
                            result = CMovementQuadran.UpRight;
                        }
                        if (Direction.x >= 0 && Direction.y < 0)
                        {
                            result = CMovementQuadran.DownRight;
                        }
                        if (Direction.x < 0 && Direction.y >= 0)
                        {
                            result = CMovementQuadran.UpLeft;
                        }
                        if (Direction.x < 0 && Direction.y < 0)
                        {
                            result = CMovementQuadran.DownLeft;
                        }
                        
                        break;
                }
            }

            return result;
        }

        public bool GetIsMoving()
        {
            return isMoving;
        }

        public void ForceStopMechanim()
        {
            isMoving = false;
            initialValue = true;
            Destination = TargetController.transform.position;
        }

        public string GetRaycastTag()
        {
            return RaycastTag;
        }

        public string GetRayObjectTag()
        {
            return RayObjectTag;
        }

    }


}