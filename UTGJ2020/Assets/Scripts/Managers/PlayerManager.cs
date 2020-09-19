﻿using System.Collections;
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
    }

    //okay let's list the data we need to transfer between scenes!

    //here is the current color
    public static ColorIDs.Colors CurrentColor{ get; set; }
    //here is a list of colors we've unlocked
    public static List<ColorIDs.Colors> AvailableColors{ get; set; }
    private static int index;

    public static void AddColor(ColorIDs.Colors c){
        AvailableColors.Add(c);
        SwitchColor();
        if(AvailableColors.Contains(ColorIDs.Colors.NONE)){
            AvailableColors.Remove(ColorIDs.Colors.NONE);
            index = 0;
        }
    }

    public static void SwitchColor(){
        if(AvailableColors.Count > 1){
            index = (index + 1) % AvailableColors.Count;
            CurrentColor = AvailableColors[index];
        }
    }

    public static Color MakeColor(ColorIDs.Colors c){
        switch(c){
            case ColorIDs.Colors.Green:
                return Color.green;
            case ColorIDs.Colors.Blue:
                return Color.blue;
            case ColorIDs.Colors.Red:
                return Color.red;
            default:
                return Color.clear;
        }
    }

}
