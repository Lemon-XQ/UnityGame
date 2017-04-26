using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnityChan
{
	[ExecuteInEditMode]
	public class SplashScreen : MonoBehaviour
	{
		void NextLevel ()
		{
			Application.LoadLevel (Application.loadedLevel + 1);
            //SceneManager.LoadScene(SceneManager.sceneloaded + 1);
		}
	}
}