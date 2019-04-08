using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMedia9
{

    public class CharacterSelectionManager : MonoBehaviour
    {
        public bool ActiveDefaultCharacter;
        public GameObject DefaultCharacter;
        public GameObject[] AllCharacters;
        [Header("Trigger Value")]
        public string CharacterActive;

        void HideAllCharacter()
        {
            for (int i=0; i< AllCharacters.Length; i++)
            {
                AllCharacters[i].SetActive(false);
            }
            if (ActiveDefaultCharacter)
            {
                DefaultCharacter.SetActive(true);
            }
        }

        void Awake()
        {
        }


        // Use this for initialization
        void Start()
        {
            CharacterActive = PlayerPrefs.GetString("CharacterActive");
            SetActiveCharacter();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectCharacter(string aCharName)
        {
            CharacterActive = aCharName;
            PlayerPrefs.SetString("CharacterActive", aCharName);
        }

        public void SetActiveCharacter()
        {
            HideAllCharacter();
            for (int i = 0; i < AllCharacters.Length; i++)
            {
                if (AllCharacters[i].name == CharacterActive) { 
                    AllCharacters[i].SetActive(true);
                }
            }
        }
    }

}
