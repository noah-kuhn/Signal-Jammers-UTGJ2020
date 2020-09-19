using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //here's our property: now we can say PlayerManager.Instance to get the instance
    //without worrying about making multiple PlayerManagers
    public static PlayerManager Instance { get; private set; }

    //make this the instance
    void Awake() {
        if (Instance == null)
        {
            Instance = this; //there is no PlayerManager-- so this can be it
            DontDestroyOnLoad(gameObject); //pls don't destroy thanks
        } else {
            Destroy(gameObject); //ok there's already a PlayerManager. so die
        }
    }

    //okay let's list the data we need to transfer between scenes!
    public static ColorIDs.Colors currCol{ get; set; }
    public static List<ColorIDs.Colors> availableColors{ get; set; }
    public static Camera mainCamera{ get; set; }
    public static GameObject player{ get; set; }

}
