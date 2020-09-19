using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool paused = false;
    public Canvas pauseMenu;
    public PlayerManager pm;
    void Start() {
        pauseMenu.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            if (paused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        // locks cursor control
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        // Disables menu UI
        pauseMenu.enabled = false;

        // Resumes time
        Time.timeScale = 1;

        paused = false;
        
        // Helps unlock camera and colorer controls
        PlayerManager.isPaused = false;
    }

    void Pause() {
        // Unlocks cursor control
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // Enables menu UI
        pauseMenu.enabled = true;
        
        // Pauses time
        Time.timeScale = 0;

        paused = true;

        // Helps lock camera and colorer controls
        PlayerManager.isPaused = true;
    }

    public void QuitToMenu() {
        // SHOULD boot to menu
        Resume();
        SceneManager.LoadScene("Main menu", LoadSceneMode.Single);
    }
    public void QuitGame() {
        Application.Quit();
    }

}
