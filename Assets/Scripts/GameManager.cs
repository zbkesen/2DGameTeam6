using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //SFX
    private AudioSource audioSource;
    public AudioClip audioButtonclick;

    [SerializeField] private GameObject helpScreen;
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioButtonclick;

    }
    private void Awake()
    {
        helpScreen.SetActive(false);
    }

    public void OnClickPlayButton()
    {
        
        SceneManager.LoadScene("LevelOne");
        
    }

    public void OnClickHelpButton()
    {
        helpScreen.SetActive(true);
    }

    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
        Debug.Log("Quit button pressed.");
    }

    public void OnClickReloadButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void OnClickBackButton()
    {
        helpScreen.SetActive(false);
    }

    public void OnClickNextLevelButton()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "LevelOne")
        {
            SceneManager.LoadScene("LevelTwo");
        }
        if(currentScene == "LevelTwo")
        {
            SceneManager.LoadScene("FinalScore");
        }
    }
}
