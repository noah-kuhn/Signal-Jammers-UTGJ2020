using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //here's our property: now we can say UIManager.Instance to get the instance
    //without worrying about making multiple UIManagers.
    public static UIManager Instance { get; private set; }

    void Awake() {
        //basic singleton stuff-- make sure there's only one instance, and it's this one!
        if (Instance == null)
        {
            Instance = this; //there is no UIManager-- so this can be it
            DontDestroyOnLoad(gameObject); //pls don't destroy thanks
        } else {
            Destroy(gameObject); //ok there's already a UIManager. so die
        }
    }

    public float fadeOutSpeed = 1f;
    public Image fadeImage;
    private Fade fadeUI;
    //public Image currentColorIndicator;

    void Start()
    {
        fadeUI = FindObjectOfType<Fade>();
        fadeImage = fadeUI.gameObject.GetComponent<Image>();
    }

    public void FadeOut(){
        StartCoroutine(DoFadeOut());
    }

    IEnumerator DoFadeOut() {
        fadeUI.FadeOut(fadeOutSpeed);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
    }

    // public void setCurrentColorIndicator(ColorIDs.Colors c){
    //     switch(c){
    //         case ColorIDs.Colors.Green:
    //             //set the currentColorIndicator
    //         case ColorIDs.Colors.Blue:
                
    //         case ColorIDs.Colors.Red:
                
    //         default:
    //             Debug.Log("bad color argument-- commence panic");
                
    //     }
    // }

}
