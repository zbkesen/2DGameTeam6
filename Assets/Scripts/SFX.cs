using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFX : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioStart; //intro music on main menu
    public AudioClip audioLevels; //bg music for level playthroughs
    public AudioClip audioCity; //city ambiance in levels

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioStart;
        audioSource.Play(); 


      
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
