using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //here's our property: now we can say PlayerManager.Instance to get the instance
    //without worrying about making multiple PlayerManagers. Note that most of the methods
    //and stuff inside of this script are also static, so PlayerManager.<func name> is
    //typically also valid.
    public static PlayerManager Instance { get; private set; }

    void Awake() {
        //basic singleton stuff-- make sure there's only one instance, and it's this one!
        if (Instance == null)
        {
            Instance = this; //there is no PlayerManager-- so this can be it
            DontDestroyOnLoad(gameObject); //pls don't destroy thanks
            index = 0;
            AvailableColors = new List<ColorIDs.Colors>();
            AvailableColors.Add(ColorIDs.Colors.NONE);
            CurrentColor = AvailableColors[index];
            //for testing color switching: give us all colors
            AddColor(ColorIDs.Colors.Green);
            AddColor(ColorIDs.Colors.Blue);
            AddColor(ColorIDs.Colors.Red);
        } else {
            Destroy(gameObject); //ok there's already a PlayerManager. so die
        }

        //on Awake, find the player
        player = GameObject.FindWithTag("Player");

        //The following block is for loading a scene's data when restarting it

        //we have a LoadSceneData and its scene number is the same as our scene
        if(lsd != null && lsd.sceneName == LevelManager.CurrentSceneName){
            //so we must be reloading this scene-- let's set all this data back
            //to where it was when we started this scene!
            CurrentColor = lsd.saved_CurrentColor;
            AvailableColors = lsd.saved_AvailableColors;
            index = lsd.saved_index;
        }else{
            //ok either we have no LoadSceneData or its sceneName is different
            //from our last one-- save current info as our new LoadSceneData
            lsd = new LoadSceneData(LevelManager.CurrentSceneName,
                        CurrentColor, AvailableColors, index);
        }
        //a quick note on the else block: we could add an UpdateData() function to the
        //LoadSceneData class if so desired (would maybe save some space)
    }

    //okay let's list the data we need to transfer between scenes!

    //here is the current color
    public static ColorIDs.Colors CurrentColor{ get; set; }

    //here is a list of colors we've unlocked
    public static List<ColorIDs.Colors> AvailableColors{ get; set; }

    // Communicates to MouseOrbit and Colorer to lock them when the game is paused
    public static bool isPaused;

    //indicates which element of our available colors list is the current one (useful for switching)
    private static int index;

    //here's the player! very important
    public static GameObject player{ get; set; }

    //and this LSD is not for tripping, but rather for loading in the data from the beginning
    //of the scene in the event our player dies and needs to reset the scene and themself
    private static LoadSceneData lsd;

    //adds the color specified, switches to it, special case for first color added
    public static void AddColor(ColorIDs.Colors c){
        AvailableColors.Add(c);
        SwitchColor();
        if(AvailableColors.Contains(ColorIDs.Colors.NONE)){
            //special case: for the first color added, remove NONE from the available colors
            //list and force the index back down to the first element (which is now our color)
            AvailableColors.Remove(ColorIDs.Colors.NONE);
            index = 0;
        }
    }

    //this one is easy: if we have multiple colors, switch to the next one in the list, wrapping
    //back to the beginning of it if necessary
    public static void SwitchColor(){
        if(AvailableColors.Count > 1){ //C# uses .Count??? is this some kind of sick joke?? what???
            index = (index + 1) % AvailableColors.Count; //this is the increment/wrap-around logic
            CurrentColor = AvailableColors[index];
        }
    }

    //this is just a switch statement. return the right Color for the right ColorIDs.Colors value
    public static Color MakeColor(ColorIDs.Colors c){
        switch(c){
            case ColorIDs.Colors.Green:
                return Color.green;
            case ColorIDs.Colors.Blue:
                return Color.blue;
            case ColorIDs.Colors.Red:
                return Color.red;
            default:
                Debug.Log("bad color argument-- commence panic");
                return Color.gray;
        }
    }

    //this is a private class because we really only need it inside the PlayerManager
    private class LoadSceneData{
        public string sceneName;
        public ColorIDs.Colors saved_CurrentColor;
        public List<ColorIDs.Colors> saved_AvailableColors;
        public int saved_index;

        //in most cases we should pass in our scene number, current color, available colors, and index
        public LoadSceneData(string _s, ColorIDs.Colors _currC, List<ColorIDs.Colors> _avC, int _i){
            sceneName = _s;
            saved_CurrentColor = _currC;
            saved_AvailableColors = _avC;
            saved_index = _i;
        }
        
    }

}
