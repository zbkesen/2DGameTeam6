using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject helpScreen;
    [SerializeField] private TMP_Text levelOneBones;
    [SerializeField] private TMP_Text levelTwoBones;

    private void Awake()
    {
        helpScreen.SetActive(false);
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            levelOneBones.text = BoneCount.Instance.levelOneBones.ToString() + "/5";
            levelTwoBones.text = BoneCount.Instance.levelTwoBones.ToString() + "/9";
        }
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
            SceneManager.LoadScene("EndScene");
        }
    }
}
