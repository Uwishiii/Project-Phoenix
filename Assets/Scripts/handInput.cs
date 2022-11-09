using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class handInput : MonoBehaviour
{
//Stores what kind of characteristics we’re looking for with our Input Device when we search for it later
    public InputDeviceCharacteristics inputDeviceCharacteristics;
    public GameObject hook;
    [SerializeField] private GameObject otherController;
    private bool putOnHook = false;
    public bool hookStart = false;

//Stores the InputDevice that we’re Targeting once we find it in InitializeHand()
    private InputDevice _targetDevice;

    void Start()
    {
        InitializeHand();
    }

    private void InitializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
//Call InputDevices to see if it can find any devices with the characteristics we’re looking for
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

//Our hands might not be active and so they will not be generated from the search.
//We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {
            _targetDevice = devices[0];
        }
    }


// Update is called once per frame
    void Update()
    {
//Since our target device might not register at the start of the scene, we continously check until one is found.
        if(!_targetDevice.isValid)
        {
            InitializeHand();
        }
        else
        {
            UpdateHand();
        }
    }

    private void UpdateHand()
    {
//This will get the value for our trigger from the target device and output a flaot into triggerValue
        if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            Debug.Log("trigger pressed");
            if (!hookStart)
            {
                hookStart = true;
            }
        }
//This will get the value for our grip from the target device and output a flaot into gripValue
        if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.1f)
        {
            Debug.Log("grip pressed");
            hookStart = false;
        }
        if (_targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryValue) && primaryValue)
        {
            Debug.Log("primary pressed");
            if (!putOnHook && otherController.GetComponent<handInput>().hook.activeSelf == false)
            {
                hook.SetActive(true);
                
                putOnHook = true;
            }
            else
            {
                hook.SetActive(false);
                putOnHook = false;
            }
        }
    }
}
