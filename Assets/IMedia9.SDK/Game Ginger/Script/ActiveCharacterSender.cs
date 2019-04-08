using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMedia9
{

    public class ActiveCharacterSender : MonoBehaviour
    {
        public GameObject[] AllCharacters;
        [Header("Trigger Value")]
        public string CharacterActive;

        void HideAllCharacter()
        {
            for (int i = 0; i < AllCharacters.Length; i++)
            {
                AllCharacters[i].SetActive(false);
            }
        }

        // Use this for initialization
        void Start()
        {
            HideAllCharacter();
            SetZeroCharacter();
        }

        public void SelectCharacter(string aCharName)
        {
            CharacterActive = aCharName;
            PlayerPrefs.SetString("CharacterActive", aCharName);
        }

        public void SetZeroCharacter()
        {
            AllCharacters[0].SetActive(true);
        }

        public void SetActiveCharacter()
        {
            HideAllCharacter();
            for (int i = 0; i < AllCharacters.Length; i++)
            {
                if (AllCharacters[i].name == CharacterActive)
                {
                    AllCharacters[i].SetActive(true);
                }
            }
        }
    }

}
