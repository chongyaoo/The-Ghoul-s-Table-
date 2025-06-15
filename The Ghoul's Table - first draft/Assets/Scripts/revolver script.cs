using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class Revolverscript : MonoBehaviour
{
    public enum Revolverstate { FIRING, IDLE };
    public Revolverstate currentstate;
    public float time = 0;
    private float waitTime = 1f;
    public GameObject triggerObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentstate = Revolverstate.IDLE;
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentstate)
        {
            case Revolverstate.IDLE:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Spacebar pressed!");
                    PlayAnimation();
                    currentstate = Revolverstate.FIRING;
                    time = 0f;
                }
                break;
            case Revolverstate.FIRING:
                Debug.Log("I am firing!");
                time += Time.deltaTime;
                if (time >= waitTime)
                {
                    StopAnimation();
                    time = 0f;
                    currentstate = Revolverstate.IDLE;
                }
                break;
        }
    }
    public void PlayAnimation()
    {
        triggerObject.GetComponent<Animator>().SetTrigger("Trigger");
    }
    public void StopAnimation()
    {

    }
}