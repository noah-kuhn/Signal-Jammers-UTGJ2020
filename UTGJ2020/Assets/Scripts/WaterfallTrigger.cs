using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterfallTrigger : MonoBehaviour
{
    public Waterfall lowerFall;
    public Waterfall midFall;
    public Waterfall upperFall;
    public bool active;
    // Start is called before the first frame update
    //void Start()
    //{
    //    lowerFall.Switch(active);
    //    midFall.Switch(active);
    //    upperFall.Switch(active);
    //}

    //private void OnTriggerEnter(Collider other) {
    //    if (other.transform.CompareTag("Burst") && PlayerManager.CurrentColor == ColorIDs.Colors.Blue) {
    //        active = !active;
    //        lowerFall.Switch(active);
    //        midFall.Switch(active);
    //        upperFall.Switch(active);
    //    }
    //}
}
