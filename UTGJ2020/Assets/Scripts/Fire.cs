using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private GameObject front;
    private GameObject back;
    private Animator frontanim;
    private Animator backanim;
    public BoxCollider death;
    public bool active;

    private void Start()
    {
        front = transform.GetChild(0).gameObject;
        back = transform.GetChild(1).gameObject;
        frontanim = front.GetComponent<Animator>();
        backanim = back.GetComponent<Animator>();
        death = front.GetComponent<BoxCollider>();
        death.enabled = active;
        frontanim.SetBool("active",active);
        backanim.SetBool("active",active);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Burst")&& PlayerManager.CurrentColor == ColorIDs.Colors.Red)
        {
            active = !active;
            death.enabled = active;
            frontanim.SetBool("active",active);
            backanim.SetBool("active",active);
        }
    }
}
