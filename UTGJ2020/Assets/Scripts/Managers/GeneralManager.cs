using UnityEngine;

public abstract class GeneralManager : MonoBehaviour
{
    //here's our property: now we can say GameManager.Instance to get the instance
    //without worrying about making multiple GameManagers
    public static GeneralManager Instance { get; private set; }

    //make this the instance
    void Awake() {
        if (Instance == null)
        {
            Instance = this; //there is no GameManager-- so this can be it
            DontDestroyOnLoad(gameObject); //pls don't destroy thanks
        } else {
            Destroy(gameObject); //ok there's already a GameManager. so die
        }
    }

}
