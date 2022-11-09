using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class hook : MonoBehaviour
{
    [SerializeField] private GameObject checkingRay;
    [SerializeField] private GameObject pressedButton;
    [SerializeField] private Transform defaultRotation;
    public bool ha = false;
    private float strengthOfAttraction = 5.0f;

    private Vector3 teleportation;

    // Update is called once per frame
    void Update()
    {
        if (checkingRay.activeSelf && !ha)
        {
            ha = true;
            teleportation = checkingRay.GetComponent<Transform>().position;
        }else if (!checkingRay.activeSelf && ha)
        {
            ha = false;
        }
    }

    private void FixedUpdate()
    {
        var button_vroom = pressedButton.GetComponent<handInput>().vroom;
        if (ha && button_vroom)
        {
            transform.LookAt(teleportation);
            if((transform.position - teleportation).magnitude>Mathf.Epsilon)
            {
                transform.Translate(0.0f,0.0f,strengthOfAttraction*Time.deltaTime);
            }
            pressedButton.GetComponent<handInput>().vroom = false;
        }
        if(this.transform.rotation!=defaultRotation.rotation)
        {
            this.transform.rotation = defaultRotation.rotation;
        }
        
    }
}
