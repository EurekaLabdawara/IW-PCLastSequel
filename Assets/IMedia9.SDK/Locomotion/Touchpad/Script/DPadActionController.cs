/*
about this script: 

if a joystick is not set to stay in a fixed position
 for the Action joystick, this script makes it appear and disappear within the Action-side half of the screen where the screen was touched 
 for the right joystick, this script makes it appear and  disappear within the right-side half of the screen where the screen was touched 

if a joystick is set to stay in a fixed position
 for the Action joystick, this script makes it appear and disappear if the user touches within the area of its background image (even if it is not currently visible)
 for the right joystick, this script makes it appear and disappear if the user touches within the area of its background image (even if it is not currently visible)
 
this script also keeps one or both joysticks always visible

modified by: Roedavan
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace IMedia9
{

    public class DPadActionController : MonoBehaviour
    {
        [System.Serializable]
        public class CDPadTouchAction
        {
            public bool isEnabled = false;
            public bool isAlwaysVisible = false; // value from Action joystick that determines if the Action joystick should be always visible or not
            public bool isFixedPosition = false;
            public GameObject ActionJoystick; // background image of the Action joystick (the joystick's handle (knob) is a child of this image and moves along with it)
        }
        public CDPadTouchAction DPadTouchActionJoystick;

        private Image DPadTouchActionHandleImage; // handle (knob) image of the Action joystick
        private DPadTouchAction DPadTouchAction; // script component attached to the Action joystick's background image
        private int ActionSideFingerID = 0; // unique finger id for touches on the Action-side half of the screen

        void Start()
        {

            if (DPadTouchActionJoystick.ActionJoystick.GetComponent<DPadTouchAction>() == null)
            {
                Debug.LogError("There is no DPadTouchAction script attached to the Action Joystick game object.");
            }
            else
            {
                DPadTouchAction = DPadTouchActionJoystick.ActionJoystick.GetComponent<DPadTouchAction>(); // gets the Action joystick script
                DPadTouchAction.joystickStaysInFixedPosition = DPadTouchActionJoystick.isFixedPosition;
                DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().enabled = DPadTouchActionJoystick.isAlwaysVisible; // sets Action joystick background image to be always visible or not
            }

            if (DPadTouchAction.transform.GetChild(0).GetComponent<Image>() == null)
            {
                Debug.LogError("There is no Action joystick handle image attached to this script.");
            }
            else
            {
                DPadTouchActionHandleImage = DPadTouchAction.transform.GetChild(0).GetComponent<Image>(); // gets the handle (knob) image of the Action joystick
                DPadTouchActionHandleImage.enabled = DPadTouchActionJoystick.isAlwaysVisible; // sets Action joystick handle (knob) image to be always visible or not
            }


            if (!DPadTouchActionJoystick.isEnabled)
            {
                DPadTouchActionJoystick.ActionJoystick.SetActive(false);
            }
        }

        void Update()
        {
            // can move code from FixedUpdate() to Update() if your controlled object does not use physics
            // can move code from Update() to FixedUpdate() if your controlled object does use physics
            // can see which one works best for your project
        }

        void FixedUpdate()
        {
            // if the screen has been touched
            if (Input.touchCount > 0)
            {
                Touch[] myTouches = Input.touches; // gets all the touches and stores them in an array

                // loops through all the current touches
                for (int i = 0; i < Input.touchCount; i++)
                {
                    // if this touch just started (finger is down for the first time), for this particular touch 
                    if (myTouches[i].phase == TouchPhase.Began)
                    {
                        // if this touch is on the Action-side half of screen
                        if (myTouches[i].position.x > Screen.width / 2)
                        {
                            ActionSideFingerID = myTouches[i].fingerId; // stores the unique id for this touch that happened on the Action-side half of the screen

                            // if the Action joystick will drag with any touch (Action joystick is not set to stay in a fixed position)
                            if (DPadTouchActionJoystick.isEnabled)
                            {
                                if (DPadTouchAction.joystickStaysInFixedPosition == false)
                                {
                                    var currentPosition = DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.position; // gets the current position of the Action joystick
                                    currentPosition.x = myTouches[i].position.x + DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.sizeDelta.x / 2; // calculates the x position of the Action joystick to where the screen was touched
                                    currentPosition.y = myTouches[i].position.y - DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.sizeDelta.y / 2; // calculates the y position of the Action joystick to where the screen was touched

                                    // keeps the Action joystick on the Action-side half of the screen
                                    currentPosition.x = Mathf.Clamp(currentPosition.x, 0 + DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.sizeDelta.x, Screen.width / 2);
                                    currentPosition.y = Mathf.Clamp(currentPosition.y, 0, Screen.height - DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.sizeDelta.y);

                                    DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.position = currentPosition; // sets the position of the Action joystick to where the screen was touched (limited to the Action half of the screen)

                                    // enables Action joystick on touch
                                    DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().enabled = true;
                                    DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                                }
                                else
                                {
                                    // Action joystick stays fixed, does not set position of Action joystick on touch

                                    // if the touch happens within the fixed area of the Action joystick's background image within the x coordinate
                                    if ((myTouches[i].position.x <= DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.position.x) && (myTouches[i].position.x >= (DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.position.x - DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.sizeDelta.x)))
                                    {
                                        // and the touch also happens within the Action joystick's background image y coordinate
                                        if ((myTouches[i].position.y >= DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.position.y) && (myTouches[i].position.y <= (DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.position.y + DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.sizeDelta.y)))
                                        {
                                            // makes the Action joystick appear 
                                            DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().enabled = true;
                                            DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                                        }
                                    }
                                }
                            }
                        }

                    }

                    // if this touch has ended (finger is up and now off of the screen), for this particular touch 
                    if (myTouches[i].phase == TouchPhase.Ended)
                    {
                        // if this touch is the touch that began on the Action half of the screen
                        if (myTouches[i].fingerId == ActionSideFingerID)
                        {
                            // makes the Action joystick disappear or stay visible
                            DPadTouchActionJoystick.ActionJoystick.GetComponent<Image>().enabled = DPadTouchActionJoystick.isAlwaysVisible;
                            DPadTouchActionHandleImage.enabled = DPadTouchActionJoystick.isAlwaysVisible;
                        }

                    }
                }
            }
        }
    }

}