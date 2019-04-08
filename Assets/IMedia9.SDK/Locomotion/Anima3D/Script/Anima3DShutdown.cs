using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMedia9
{

    public class Anima3DShutdown : MonoBehaviour
    {

        public enum CCompareType { Greater, Equal, Less }
        public enum CParameterType { Int, Float, Bool, Trigger }

        public Animator TargetAnimator;

        [System.Serializable]
        public class CAnimationState3D
        {
            [Header("Animation Settings")]
            public string StateNow;
            public string StateNext;
            public CParameterType ParameterType;
            public string ParameterName;
            public string PositiveValue;
            public string NegativeValue;
        }

        public CAnimationState3D AnimationState3D;

        [Header("Sound Settings")]
        public bool usingSound;
        public AudioSource animaAudioSource;
        public AudioClip animaAudioClip;

        [Header("Disable Input Settings")]
        public bool DisabledInputWhenDeath;
        public Mechanim3DKeyboard InputKeyboard;
        public Mechanim3DClick InputMouse;
        public Mechanim3DDpad InputDPad;

        void Shutdown(bool aValue)
        {
            if (AnimationState3D.ParameterType == CParameterType.Float)
            {
                float dummyvalue = float.Parse(AnimationState3D.PositiveValue);
                TargetAnimator.SetFloat(AnimationState3D.ParameterName, dummyvalue);
                ExecuteSound();
            }
            if (AnimationState3D.ParameterType == CParameterType.Int)
            {
                int dummyvalue = int.Parse(AnimationState3D.PositiveValue);
                TargetAnimator.SetInteger(AnimationState3D.ParameterName, dummyvalue);
                ExecuteSound();
            }
            if (AnimationState3D.ParameterType == CParameterType.Bool)
            {
                bool dummyvalue = bool.Parse(AnimationState3D.PositiveValue);
                TargetAnimator.SetBool(AnimationState3D.ParameterName, dummyvalue);
                ExecuteSound();
            }
            if (AnimationState3D.ParameterType == CParameterType.Trigger)
            {
                TargetAnimator.SetTrigger(AnimationState3D.ParameterName);
                ExecuteSound();
            }

            if (DisabledInputWhenDeath)
            {
                if (InputKeyboard != null)
                {
                    InputKeyboard.enabled = false;
                }
                if (InputMouse != null)
                {
                    InputMouse.enabled = false;
                }
                if (InputDPad != null)
                {
                    InputDPad.enabled = false;
                }
            }
        }

        void ExecuteSound(bool plays = true)
        {
            if (usingSound)
            {
                if (!animaAudioSource.isPlaying && plays)
                {
                    animaAudioSource.clip = animaAudioClip;
                    animaAudioSource.Play();
                }
                else if (!plays)
                {
                    animaAudioSource.clip = animaAudioClip;
                    animaAudioSource.Stop();
                }
            }
        }
    }

}