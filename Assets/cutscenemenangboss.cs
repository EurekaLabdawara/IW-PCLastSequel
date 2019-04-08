using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IMedia9
{
	public class cutscenemenangboss : MonoBehaviour {

		public GlobalFloatVar FloatVariables;
		public float FloatValue;
		public string NextSceneName;
		
		void Update(){
			if (FloatVariables.CurrentValue == FloatValue) {
				SceneManager.LoadScene(NextSceneName);
			}
		}
	}
}