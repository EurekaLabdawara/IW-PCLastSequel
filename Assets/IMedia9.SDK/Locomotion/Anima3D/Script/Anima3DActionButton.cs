using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMedia9
{

    public class Anima3DActionDpad : MonoBehaviour
    {

        public enum CCompareType { Greater, Equal, Less }
        public enum CParameterType { Int, Float, Bool, Trigger }

        public bool isEnabled;
        public Animator TargetAnimator;
        public GameObject TargetObject;

        [System.Serializable]
        public class CAnimationState3D
        {
            [Header("Action Settings")]
            public string StateNow;
            public string StateNext;
            public CParameterType ParameterType;
            public string ParameterName;
            public string PositiveValue;
            public string NegativeValue;
            public DPadActionController DPadActionController;
            public DPadTouchAction DPadActionButton;
            [Header("Sound Settings")]
            public bool usingSound;
            public AudioSource animaAudioSource;
            public AudioClip animaAudioClip;
        }

        bool isCooldown = false;
        private Vector3 actionJoystickInput; 

        public CAnimationState3D AnimationState3D;

        [Header("Calculation Settings")]
        public bool isFlipDirection;
        public float rotationSpeed = 100;
        float moveSpeed = 100;

        // Use this for initialization
        void Start()
        {

        }

        void SyncronizeRotation()
        {
            Quaternion tempRotation = TargetObject.transform.rotation;
            TargetAnimator.transform.rotation = TargetObject.transform.rotation;
            TargetObject.transform.rotation = tempRotation;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (isEnabled)
            {
                if (isFlipDirection)
                {
                    actionJoystickInput = -1 * AnimationState3D.DPadActionButton.GetInputDirection();
                }
                else
                {
                    actionJoystickInput = AnimationState3D.DPadActionButton.GetInputDirection();
                }

                float xMovementActionJoystick = actionJoystickInput.x;
                float zMovementActionJoystick = actionJoystickInput.y;

                if (actionJoystickInput != Vector3.zero)
                {

                    //== ACTION MOVEMENT BEGIN
                    //TargetObject.transform.Translate(xMovementLeftJoystick * touchSensitivity, zMovementLeftJoystick * touchSensitivity, 0);
                    float tempAngle = Mathf.Atan2(zMovementActionJoystick, xMovementActionJoystick);
                    xMovementActionJoystick *= Mathf.Abs(Mathf.Cos(tempAngle));
                    zMovementActionJoystick *= Mathf.Abs(Mathf.Sin(tempAngle));

                    // calculate the player's direction based on angle

                    actionJoystickInput = new Vector3(xMovementActionJoystick, 0, zMovementActionJoystick);
                    actionJoystickInput = TargetObject.transform.TransformDirection(actionJoystickInput);
                    actionJoystickInput *= moveSpeed;

                    // rotate the player to face the direction of input
                    Vector3 temp = TargetObject.transform.position;
                    temp.x += xMovementActionJoystick;
                    temp.z += zMovementActionJoystick;
                    Vector3 lookDirection = temp - TargetObject.transform.position;
                    if (lookDirection != Vector3.zero)
                    {
                        TargetObject.transform.localRotation = Quaternion.Slerp(TargetObject.transform.localRotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
                    }

                }

                if (AnimationState3D.DPadActionButton.isActionButtonUp() && !isCooldown)
                {

                    isCooldown = true;
                    Invoke("Cooldown", 1);

                    if (AnimationState3D.ParameterType == CParameterType.Float)
                    {
                        float dummyvalue = float.Parse(AnimationState3D.PositiveValue);
                        TargetAnimator.SetFloat(AnimationState3D.ParameterName, dummyvalue);
                        SyncronizeRotation();
                        ExecuteSound(0);
                    }
                    if (AnimationState3D.ParameterType == CParameterType.Trigger)
                    {
                        TargetAnimator.SetTrigger(AnimationState3D.ParameterName);
                        SyncronizeRotation();
                        ExecuteSound(0);
                    }
                }

            }

            /*
  
                for (int i = 0; i < AnimationState3D.Length; i++)
                {
                    if (AnimationState3D[i].TriggerButton.isActionButtonUp() && !isCooldown)
                    {

                        isCooldown = true;
                        Invoke("Cooldown", 1);

                        if (AnimationState3D[i].ParameterType == CParameterType.Float)
                        {
                            float dummyvalue = float.Parse(AnimationState3D[i].PositiveValue);
                            TargetAnimator.SetFloat(AnimationState3D[i].ParameterName, dummyvalue);
                            ExecuteSound(i);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Trigger)
                        {
                            TargetAnimator.SetTrigger(AnimationState3D[i].ParameterName);
                            ExecuteSound(i);
                        }
                    }
                }
            }
            */
        }

        void LateUpdate()
        {
            /*
            if (isEnabled)
            {
                for (int i = 0; i < AnimationState3D.Length; i++)
                {
                    if (AnimationState3D[i].TriggerButton.isActionButtonUp())
                    {
                        if (AnimationState3D[i].ParameterType == CParameterType.Float)
                        {
                            //float dummyvalue = float.Parse(AnimationState3D[i].NegativeValue);
                           // TargetAnimator.SetFloat(AnimationState3D[i].ParameterName, dummyvalue);
                        }
                    }
                }
            }
            */
        }

        void ExecuteSound(int index, bool plays = true)
        {
            if (AnimationState3D.usingSound)
            {
                if (!AnimationState3D.animaAudioSource.isPlaying && plays)
                {
                    AnimationState3D.animaAudioSource.clip = AnimationState3D.animaAudioClip;
                    AnimationState3D.animaAudioSource.Play();
                }
                else if (!plays)
                {
                    AnimationState3D.animaAudioSource.clip = AnimationState3D.animaAudioClip;
                    AnimationState3D.animaAudioSource.Stop();
                }
            }
        }


        void Shutdown(bool aValue)
        {
            isEnabled = false;
        }

        void Cooldown()
        {
            isCooldown = false;
        }

    }

}