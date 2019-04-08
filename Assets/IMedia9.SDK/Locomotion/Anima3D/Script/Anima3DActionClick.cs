using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMedia9
{

    public class Anima3DActionClick : MonoBehaviour
    {

        public enum CCompareType { Greater, Equal, Less }
        public enum CClickType { LeftMouse, MiddleMouse, RightMouse, Touch }
        public enum CParameterType { Int, Float, Bool, Trigger }

        public bool isEnabled;
        public Animator TargetAnimator;
        public Anima3DClick TargetAnimaClick;
        public Mechanim3DClick TargetMechanimClick;
        [HideInInspector]
        public Vector3 RotationStatus;
        Vector3 delta, lastpos;

        [Header("Target Enemy Settings")]
        public string EnemyTag;
        public float AttackRange;
        public bool IgnoreMechanimRaycast;

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
            public CClickType TriggerKey;
            [Header("Sound Settings")]
            public bool usingSound;
            public AudioSource animaAudioSource;
            public AudioClip animaAudioClip;

            public KeyCode GetTriggerKey()
            {
                KeyCode Result = KeyCode.None;
                if (TriggerKey == CClickType.LeftMouse) Result = KeyCode.Mouse0;
                if (TriggerKey == CClickType.RightMouse) Result = KeyCode.Mouse1;
                if (TriggerKey == CClickType.MiddleMouse) Result = KeyCode.Mouse2;
                if (TriggerKey == CClickType.Touch) Result = KeyCode.Mouse0;
                return Result;
            }
        }

        public CAnimationState3D[] AnimationState3D;

        // Use this for initialization
        void Start()
        {

        }

        public bool isCanAttack()
        {
            bool result = false;
            if (EnemyTag == TargetMechanimClick.GetRaycastTag())
            {
                GameObject temp = GameObject.Find(TargetMechanimClick.GetRayObjectTag());
                if (temp != null)
                {
                    float distance = Vector3.Distance(this.transform.position, temp.transform.position);
                    if (distance <= AttackRange)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                for (int i = 0; i < AnimationState3D.Length; i++)
                {
                    if (Input.GetKeyUp(AnimationState3D[i].GetTriggerKey()) && (isCanAttack() || IgnoreMechanimRaycast))
                    {
                        if (AnimationState3D[i].ParameterType == CParameterType.Float)
                        {
                            float dummyvalue = float.Parse(AnimationState3D[i].PositiveValue);
                            TargetAnimator.SetFloat(AnimationState3D[i].ParameterName, dummyvalue);
                            TargetMechanimClick.ForceStopMechanim();
                            TargetAnimaClick.ForceStopAnima();
                            ExecuteSound(i);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Int)
                        {
                            int dummyvalue = int.Parse(AnimationState3D[i].PositiveValue);
                            TargetAnimator.SetInteger(AnimationState3D[i].ParameterName, dummyvalue);
                            TargetMechanimClick.ForceStopMechanim();
                            TargetAnimaClick.ForceStopAnima();
                            ExecuteSound(i);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Bool)
                        {
                            bool dummyvalue = bool.Parse(AnimationState3D[i].PositiveValue);
                            TargetAnimator.SetBool(AnimationState3D[i].ParameterName, dummyvalue);
                            TargetMechanimClick.ForceStopMechanim();
                            TargetAnimaClick.ForceStopAnima();
                            ExecuteSound(i);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Trigger)
                        {
                            TargetAnimator.SetTrigger(AnimationState3D[i].ParameterName);
                            TargetMechanimClick.ForceStopMechanim();
                            TargetAnimaClick.ForceStopAnima();
                            ExecuteSound(i);
                        }
                    } else if (isCanAttack())
                    {
                        if (AnimationState3D[i].ParameterType == CParameterType.Float)
                        {
                            float dummyvalue = float.Parse(AnimationState3D[i].PositiveValue);
                            TargetAnimator.SetFloat(AnimationState3D[i].ParameterName, dummyvalue);
                            TargetMechanimClick.ForceStopMechanim();
                            TargetAnimaClick.ForceStopAnima();
                            ExecuteSound(i);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Int)
                        {
                            int dummyvalue = int.Parse(AnimationState3D[i].PositiveValue);
                            TargetAnimator.SetInteger(AnimationState3D[i].ParameterName, dummyvalue);
                            TargetMechanimClick.ForceStopMechanim();
                            TargetAnimaClick.ForceStopAnima();
                            ExecuteSound(i);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Bool)
                        {
                            bool dummyvalue = bool.Parse(AnimationState3D[i].PositiveValue);
                            TargetAnimator.SetBool(AnimationState3D[i].ParameterName, dummyvalue);
                            TargetMechanimClick.ForceStopMechanim();
                            TargetAnimaClick.ForceStopAnima();
                            ExecuteSound(i);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Trigger)
                        {
                            TargetAnimator.SetTrigger(AnimationState3D[i].ParameterName);
                            TargetMechanimClick.ForceStopMechanim();
                            TargetAnimaClick.ForceStopAnima();
                            ExecuteSound(i);
                        }
                    }
                }
            }
        }

        void LateUpdate()
        {
            if (isEnabled)
            {
                /*
                for (int i = 0; i < AnimationState3D.Length; i++)
                {
                    if (Input.GetKeyUp(AnimationState3D[i].GetTriggerKey()))
                    {
                        if (AnimationState3D[i].ParameterType == CParameterType.Float)
                        {
                            float dummyvalue = float.Parse(AnimationState3D[i].NegativeValue);
                            TargetAnimator.SetFloat(AnimationState3D[i].ParameterName, dummyvalue);
                            ExecuteSound(i, false);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Int)
                        {
                            int dummyvalue = int.Parse(AnimationState3D[i].NegativeValue);
                            TargetAnimator.SetInteger(AnimationState3D[i].ParameterName, dummyvalue);
                            ExecuteSound(i, false);
                        }
                        if (AnimationState3D[i].ParameterType == CParameterType.Bool)
                        {
                            bool dummyvalue = bool.Parse(AnimationState3D[i].NegativeValue);
                            TargetAnimator.SetBool(AnimationState3D[i].ParameterName, dummyvalue);
                            ExecuteSound(i, false);
                        }
                    }
                }
                */
            }
        }

        void Shutdown(bool aValue)
        {
            isEnabled = false;
        }

        void ExecuteSound(int index, bool plays = true)
        {
            if (AnimationState3D[index].usingSound)
            {
                if (!AnimationState3D[index].animaAudioSource.isPlaying && plays)
                {
                    AnimationState3D[index].animaAudioSource.clip = AnimationState3D[index].animaAudioClip;
                    AnimationState3D[index].animaAudioSource.Play();
                }
                else if (!plays)
                {
                    AnimationState3D[index].animaAudioSource.clip = AnimationState3D[index].animaAudioClip;
                    AnimationState3D[index].animaAudioSource.Stop();
                }
            }
        }
    }

}