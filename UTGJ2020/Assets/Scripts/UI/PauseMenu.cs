using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool paused = false;
    public Canvas pauseMenu;

    void Start() {
        pauseMenu.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.enabled = false;
        Time.timeScale = 1;
        paused = false;
    }

    void Pause() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.enabled = true;
        Time.timeScale = 0;
        paused = true;
    }

    public void QuitToMenu() {
        // SHOULD boot to menu
        SceneManager.LoadScene("Main menu");
    }
    public void QuitGame() {
        Application.Quit();
    }

}
