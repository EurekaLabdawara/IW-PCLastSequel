using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IMedia9
{
    public class ActiveCanvasCharacter : MonoBehaviour
    {

        [System.Serializable]
        public class CCharacterSelect
        {
            public Toggle ToggleButton;
            public GameObject CharacterObject;
            public AudioClip CharacterAudio;
            public Animator CharacterAnimator;
            public string AnimationStateName;
        }

        public ActiveCharacterSender CharacterActive;
        public CCharacterSelect[] CharacterSelect;

        // Use this for initialization
        void Start()
        {
            SetDefault();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetDefault()
        {
            if (CharacterSelect.Length > 0)
            {
                CharacterSelect[0].ToggleButton.isOn = true;
                CharacterSelect[0].CharacterObject.SetActive(true);
                CharacterActive.SelectCharacter(CharacterSelect[0].CharacterObject.name);
            }

            for (int i = 1; i <= CharacterSelect.Length - 1; i++)
            {
                CharacterSelect[i].ToggleButton.isOn = false;
                CharacterSelect[i].CharacterObject.SetActive(false);
            }
        }

        public void HideAllCharacter()
        {
            for (int i = 0; i <= CharacterSelect.Length - 1; i++)
            {
                CharacterSelect[i].CharacterObject.SetActive(false);
            }
        }

        public void ShowCharacter(Toggle ToggleButton)
        {
            HideAllCharacter();
            for (int i = 0; i <= CharacterSelect.Length - 1; i++)
            {
                if (ToggleButton.name == CharacterSelect[i].ToggleButton.name)
                {
                    CharacterSelect[i].CharacterObject.SetActive(true);

                    CharacterSelect[i].CharacterAnimator.Play(CharacterSelect[i].AnimationStateName);

                    if (CharacterSelect[i].CharacterAudio != null)
                    {
                        GetComponent<AudioSource>().clip = CharacterSelect[i].CharacterAudio;
                        GetComponent<AudioSource>().Play();
                    }

                    CharacterActive.SelectCharacter(CharacterSelect[i].CharacterObject.name);
                }
            }
        }
    }
}
