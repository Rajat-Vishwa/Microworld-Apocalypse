using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static void Play(){
        SceneManager.LoadScene("MainScene");
    }

    public static void Quit(){
        Application.Quit();
    }
    
}
