using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IMedia9
{

    public class DPadCanvasSkill : MonoBehaviour
    {

        public GameObject CanvasSkill;
        public DPadTouchAction ActionButton;
        float xMovementRightJoystick, zMovementRightJoystick;
        float rotationSpeed = 8;
        public bool isDebug = false;

        // Use this for initialization
        void Awake()
        {
            
        }

        // Use this for initialization
        void Start()
        {
            CanvasSkill.SetActive(false);
        }

        void Update()
        {
            if (isDebug) Debug.Log(ActionButton.isButtonStatusDown());   
            if (ActionButton.isButtonStatusDown())
            {
                CanvasSkill.SetActive(true);
            }
            else
            {
                CanvasSkill.SetActive(false);
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        }
    }

}
