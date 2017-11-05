using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class LoadScene : MonoBehaviour
{

    private bool toggleAudioOn = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadSceneSelect()
    {
        SceneManager.LoadScene("Level0");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

	public void LoadCredits()
	{
		SceneManager.LoadScene("Credits");
	}

    public void returnFromOptions()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void toggleAudio()
    {
        if(toggleAudioOn)
        {
            Debug.Log("Audio off");
            toggleAudioOn = !toggleAudioOn;
        }
        else
        {
            Debug.Log("Audio on");
            toggleAudioOn = !toggleAudioOn;
        }

    }

    public void QuitGame()
    {
        Debug.Log("<i>Quitting!</i>");
        Application.Quit();
    }

}