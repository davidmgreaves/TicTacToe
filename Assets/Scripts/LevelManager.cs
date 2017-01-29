using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	int currentLevel;

	// load scene by build order
	public void LoadNext( )
	{
		currentLevel = SceneManager.GetActiveScene ( ).buildIndex;
		SceneManager.LoadScene ( currentLevel + 1 );
	}

	// load scene by level name
	public void LoadLevel(string levelName)
	{
		SceneManager.LoadScene ( levelName );
	}

	// load scene by build index
	public void LoadLevel(int levelIndex )
	{
		SceneManager.LoadScene ( levelIndex );
	}
}
