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
    private bool ha = false;
    private float strengthOfAttraction = 0.5f;

    private Vector3 teleportation;

    private Vector3 defaultRotation = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkingRay.activeSelf && !ha)
        {
            Debug.Log("fsdhgkjdsg");
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
            //suck++;
            transform.LookAt(teleportation);
            if((transform.position - teleportation).magnitude>Mathf.Epsilon)
            {
                transform.Translate(0.0f,0.0f,strengthOfAttraction*Time.deltaTime);
            }

            button_vroom = false;
        }
        
    }
}
