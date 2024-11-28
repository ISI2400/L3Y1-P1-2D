using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas main;
    public Canvas options;




    void Start()
    {
        main.enabled = true;
        options.enabled = false;
    }

    public void onPlay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onOptions(){
        main.enabled = false;
        options.enabled = true;
    }

    public void onExit(){
        Application.Quit();
    }

    public void onBackToMain(){
        main.enabled = true;
        options.enabled = false;
    }




}
