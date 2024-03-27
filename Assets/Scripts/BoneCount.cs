using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCount : MonoBehaviour
{
    public static BoneCount Instance;
    public int levelOneBones;
    public int levelTwoBones;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
