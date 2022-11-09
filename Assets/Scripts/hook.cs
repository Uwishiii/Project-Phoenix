using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class hook : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private GameObject checkingRay;
    [SerializeField] private GameObject pressedButton;
    [SerializeField] private GameObject pressedButton2;
    [SerializeField] private Transform defaultRotation;
    private bool allow_hooking = false;
    //public bool hooking = false;
    private float strengthOfAttraction = 7.0f;

    private Vector3 hookHit;

    // Update is called once per frame
    void Update()
    {
        if (checkingRay.activeSelf && !allow_hooking)
        {
            allow_hooking = true;
            hookHit = checkingRay.GetComponent<Transform>().position;
        }else if (!checkingRay.activeSelf && allow_hooking)
        {
            allow_hooking = false;
        }
    }

    private void FixedUpdate()
    {
        var button_vroom = pressedButton.GetComponent<handInput>().hookStart;
        var button_vroom2 = pressedButton2.GetComponent<handInput>().hookStart;
        if (allow_hooking && (button_vroom || button_vroom2))
        {
            //hooking = true;
            transform.LookAt(hookHit);
            if((transform.position - hookHit).magnitude>Mathf.Epsilon)
            {
                transform.Translate(0.0f,0.0f,strengthOfAttraction*Time.deltaTime);
            }
            pressedButton.GetComponent<handInput>().hookStart = false;
            pressedButton2.GetComponent<handInput>().hookStart = false;
        }

        //hooking = false;
        
        if(this.transform.rotation!=defaultRotation.rotation)
        {
            this.transform.rotation = defaultRotation.rotation;
        }
        
    }
}
