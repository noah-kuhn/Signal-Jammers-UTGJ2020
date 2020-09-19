using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //here's our property: now we can say LevelManager.Instance to get the instance
    //without worrying about making multiple LevelManagers.
    public static LevelManager Instance { get; private set; }

    void Awake() {
        //basic singleton stuff-- make sure there's only one instance, and it's this one!
        if (Instance == null)
        {
            Instance = this; //there is no LevelManager-- so this can be it
            DontDestroyOnLoad(gameObject); //pls don't destroy thanks
        } else {
            Destroy(gameObject); //ok there's already a LevelManager. so die
        }
        //store our current scene's name every time we load a scene
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    //this is the name of our current scene
    public static string CurrentSceneName{ get; set; }

    public static void RestartScene(){
        ProgressToScene(SceneManager.GetActiveScene().name);
    }

    //uncomment when UIManager is ready to be used
    public static void ProgressToScene(string name){
        UIManager.Instance.FadeOut();
        SceneManager.LoadScene(name);
    }
}
