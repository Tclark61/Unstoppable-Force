using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			if (SceneManager.GetActiveScene ().name == "Level0")
				SceneManager.LoadScene ("Level1");
			else if (SceneManager.GetActiveScene ().name == "Level1")
				SceneManager.LoadScene ("Level2");
			else if (SceneManager.GetActiveScene ().name == "Level2")
				SceneManager.LoadScene ("Credits");
		}
	}
}
