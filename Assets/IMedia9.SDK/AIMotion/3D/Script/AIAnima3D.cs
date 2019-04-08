using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMedia9
{

    public class AIAnima3D : MonoBehaviour
    {
        bool isCooldown = false;

        public enum CCompareType { Greater, Equal, Less }
        public enum CParameterType { Int, Float, Bool, Trigger }

        public bool isEnabled;
        public Animator TargetAnimator;

        [System.Serializable]
        public class CMovingState3D
        {
            [Header("Animation Settings")]
            public string StateNow;
            public string StateNext;
            public CParameterType ParameterType;
            public string ParameterName;
            public string PositiveValue;
            public string NegativeValue;
            public AIMechanim3D TriggerMechanim;

            [Header("Sound Settings")]
            public bool usingMovingSound;
            public AudioSource movingAudioSource;
            public AudioClip movingAudioClip;
        }

        [System.Serializable]
        public class CAttackState3D
        {
            [Header("Animation Settings")]
            public string StateNow;
            public string StateNext;
            public CParameterType ParameterType;
            public string ParameterName;
            public string PositiveValue;
            public string NegativeValue;
            public AIMechanim3D TriggerMechanim;
            public int AttackDelay;

            [Header("Sound Settings")]
            public bool usingAttackSound;
            public AudioSource attackAudioSource;
            public AudioClip attackAudioClip;
        }

        public CMovingState3D[] MovingState3D;
        public CAttackState3D[] AttackState3D;

        string paramName;
        float floatValue;
        int intValue;
        bool boolValue;
        int indexSound;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                for (int i = 0; i < MovingState3D.Length; i++)
                {
                        if (MovingState3D[i].TriggerMechanim.GetMovingStatus())
                        {
                            if (MovingState3D[i].ParameterType == CParameterType.Float)
                            {
                                float dummyvalue = float.Parse(MovingState3D[i].PositiveValue);
                                TargetAnimator.SetFloat(MovingState3D[i].ParameterName, dummyvalue);
                            }
                            if (MovingState3D[i].ParameterType == CParameterType.Int)
                            {
                                int dummyvalue = int.Parse(MovingState3D[i].PositiveValue);
                                TargetAnimator.SetInteger(MovingState3D[i].ParameterName, dummyvalue);
                            }
                            if (MovingState3D[i].ParameterType == CParameterType.Bool)
                            {
                                bool dummyvalue = bool.Parse(MovingState3D[i].PositiveValue);
                                TargetAnimator.SetBool(MovingState3D[i].ParameterName, dummyvalue);
                            }
                            if (MovingState3D[i].ParameterType == CParameterType.Trigger)
                            {
                                TargetAnimator.SetTrigger(MovingState3D[i].ParameterName);
                            }
                        }
                }

                for (int i = 0; i < AttackState3D.Length; i++)
                {
                    if (AttackState3D[i].TriggerMechanim.GetAttackStatus())
                    {
                        if (AttackState3D[i].ParameterType == CParameterType.Float)
                        {
                            if (!isCooldown)
                            {
                                indexSound = i;
                                isCooldown = true;
                                floatValue = float.Parse(AttackState3D[i].PositiveValue);
                                paramName = AttackState3D[i].ParameterName;
                                Invoke("ExecuteAttackFloat", AttackState3D[i].AttackDelay);
                            }
                        }
                        if (AttackState3D[i].ParameterType == CParameterType.Int)
                        {
                            if (!isCooldown)
                            {
                                indexSound = i;
                                isCooldown = true;
                                intValue = int.Parse(AttackState3D[i].PositiveValue);
                                paramName = AttackState3D[i].ParameterName;
                                Invoke("ExecuteAttackInt", AttackState3D[i].AttackDelay);
                            }
                        }
                        if (AttackState3D[i].ParameterType == CParameterType.Bool)
                        {
                            if (!isCooldown)
                            {
                                indexSound = i;
                                isCooldown = true;
                                boolValue = bool.Parse(AttackState3D[i].PositiveValue);
                                paramName = AttackState3D[i].ParameterName;
                                Invoke("ExecuteAttackBool", AttackState3D[i].AttackDelay);
                            }
                        }
                        if (AttackState3D[i].ParameterType == CParameterType.Trigger)
                        {
                            if (!isCooldown)
                            {
                                indexSound = i;
                                isCooldown = true;
                                paramName = AttackState3D[i].ParameterName;
                                Invoke("ExecuteAttackTrigger", AttackState3D[i].AttackDelay);
                            }
                        }
                    }
                }
            }
        }

        void ExecuteAttackFloat()
        {
            isCooldown = false;
            TargetAnimator.SetFloat(paramName, floatValue);
            ExecuteAttackSound(indexSound);
        }

        void ExecuteAttackInt()
        {
            isCooldown = false;
            TargetAnimator.SetInteger(paramName, intValue);
            ExecuteAttackSound(indexSound);
        }

        void ExecuteAttackBool()
        {
            isCooldown = false;
            TargetAnimator.SetBool(paramName, boolValue);
            ExecuteAttackSound(indexSound);
        }

        void ExecuteAttackTrigger()
        {
            isCooldown = false;
            TargetAnimator.SetTrigger(paramName);
            ExecuteAttackSound(indexSound);
        }

        void LateUpdate()
        {
            if (isEnabled)
            {
                for (int i = 0; i < MovingState3D.Length; i++)
                {
                        if (!MovingState3D[i].TriggerMechanim.GetMovingStatus())
                        {
                            if (MovingState3D[i].ParameterType == CParameterType.Float)
                            {
                                float dummyvalue = float.Parse(MovingState3D[i].NegativeValue);
                                TargetAnimator.SetFloat(MovingState3D[i].ParameterName, dummyvalue);
                            }
                            if (MovingState3D[i].ParameterType == CParameterType.Int)
                            {
                                int dummyvalue = int.Parse(MovingState3D[i].NegativeValue);
                                TargetAnimator.SetInteger(MovingState3D[i].ParameterName, dummyvalue);
                            }
                            if (MovingState3D[i].ParameterType == CParameterType.Bool)
                            {
                                bool dummyvalue = bool.Parse(MovingState3D[i].NegativeValue);
                                TargetAnimator.SetBool(MovingState3D[i].ParameterName, dummyvalue);
                            }
                            if (MovingState3D[i].ParameterType == CParameterType.Trigger)
                            {
                                TargetAnimator.SetTrigger(MovingState3D[i].ParameterName);
                            }

                        }

                }

                for (int i = 0; i < AttackState3D.Length; i++)
                {
                    if (!AttackState3D[i].TriggerMechanim.GetAttackStatus())
                    {
                        if (AttackState3D[i].ParameterType == CParameterType.Float)
                        {
                            float dummyvalue = float.Parse(AttackState3D[i].NegativeValue);
                            TargetAnimator.SetFloat(AttackState3D[i].ParameterName, dummyvalue);
                        }
                        if (AttackState3D[i].ParameterType == CParameterType.Int)
                        {
                            int dummyvalue = int.Parse(AttackState3D[i].NegativeValue);
                            TargetAnimator.SetInteger(AttackState3D[i].ParameterName, dummyvalue);
                        }
                        if (AttackState3D[i].ParameterType == CParameterType.Bool)
                        {
                            bool dummyvalue = bool.Parse(AttackState3D[i].NegativeValue);
                            TargetAnimator.SetBool(AttackState3D[i].ParameterName, dummyvalue);
                        }
                    }

                }
            }
        }

        void Shutdown(bool aValue)
        {
            isEnabled = false;
        }

        void ExecuteMovingSound(int index, bool plays = true)
        {
            if (MovingState3D[index].usingMovingSound)
            {
                if (!MovingState3D[index].movingAudioSource.isPlaying && plays)
                {
                    MovingState3D[index].movingAudioSource.clip = MovingState3D[index].movingAudioClip;
                    MovingState3D[index].movingAudioSource.Play();
                }
                else if (!plays)
                {
                    MovingState3D[index].movingAudioSource.clip = MovingState3D[index].movingAudioClip;
                    MovingState3D[index].movingAudioSource.Stop();
                }
            }
        }

        void ExecuteAttackSound(int index, bool plays = true)
        {
            if (AttackState3D[index].usingAttackSound)
            {
                if (!AttackState3D[index].attackAudioSource.isPlaying && plays)
                {
                    AttackState3D[index].attackAudioSource.clip = AttackState3D[index].attackAudioClip;
                    AttackState3D[index].attackAudioSource.Play();
                }
                else if (!plays)
                {
                    AttackState3D[index].attackAudioSource.clip = AttackState3D[index].attackAudioClip;
                    AttackState3D[index].attackAudioSource.Stop();
                }
            }
        }

    }

}