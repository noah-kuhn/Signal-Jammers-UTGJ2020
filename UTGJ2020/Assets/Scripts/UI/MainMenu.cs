using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Speed at which the screen will fade out when going to next scene. 1 is normal speed")]
    public float fadeOutSpeed = 1f;
    public Image fadeImage;
    private Fade fadeUI;
    // Start is called before the first frame update
    void Start()
    {
        fadeUI = FindObjectOfType<Fade>();
        fadeImage = fadeUI.gameObject.GetComponent<Image>();
        AudioManager.Instance.PlaySound(AudioManager.SoundIDs.TITLE);
    }

    public void RunFirstScene() {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        fadeUI.FadeOut(fadeOutSpeed);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        AudioManager.Instance.FadeSound(AudioManager.SoundIDs.TITLE);
        SceneManager.LoadScene("Scene 1");
    }
}
