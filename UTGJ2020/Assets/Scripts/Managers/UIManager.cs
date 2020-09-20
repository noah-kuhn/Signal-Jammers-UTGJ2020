using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //here's our property: now we can say UIManager.Instance to get the instance
    //without worrying about making multiple UIManagers.
    public static UIManager Instance { get; private set; }
    private AudioManager _audioManager;

    void Awake() {
        //basic singleton stuff-- make sure there's only one instance, and it's this one!
        if (Instance == null)
        {
            Instance = this; //there is no UIManager-- so this can be it
            DontDestroyOnLoad(gameObject); //pls don't destroy thanks
        } else {
            Destroy(gameObject); //ok there's already a UIManager. so die
        }
        _audioManager = FindObjectOfType<AudioManager>();
        MakeUI();
    }

    public float fadeOutSpeed = 1f;
    public Image fadeImage;
    private Fade fadeUI;

    void MakeUI()
    {
        fadeUI = FindObjectOfType<Fade>();
        fadeImage = fadeUI.gameObject.GetComponent<Image>();
    }

    public void FadeOutToScene(string scene){
        StartCoroutine(DoFadeOutToScene(scene));
    }

    IEnumerator DoFadeOutToScene(string s) {
        if(fadeUI == null){
            MakeUI();
        }
        fadeUI.FadeOut(fadeOutSpeed);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        
        SceneManager.LoadScene(s);
    }

}
