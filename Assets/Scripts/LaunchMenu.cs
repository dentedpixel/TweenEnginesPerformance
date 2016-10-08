using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaunchMenu : MonoBehaviour {

	public void launchLevel( int lvl ){
		SceneManager.LoadScene(lvl);
	}
}
