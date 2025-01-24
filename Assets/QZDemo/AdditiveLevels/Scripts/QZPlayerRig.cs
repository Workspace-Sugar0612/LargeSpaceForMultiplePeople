using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class QZPlayerRig : MonoBehaviour
{
    public Transform rHandTransform;
    public Transform lHandTransform;
    public Transform headTransform;
    public Transform playerTransform;

    public QZPlayerScript localQZPlayerScript;

    // switch to Late/Fixed Update if weirdness happens
    private void Update()
    {
        if (localQZPlayerScript)
        {
            // presuming you want a head object to sync, optional, same as hands.
            localQZPlayerScript.headTransform.position = this.headTransform.position;
            localQZPlayerScript.headTransform.rotation = this.headTransform.rotation;
            localQZPlayerScript.rHandTransform.position = this.rHandTransform.position;
            localQZPlayerScript.rHandTransform.rotation = this.rHandTransform.rotation;
            localQZPlayerScript.lHandTransform.position = this.lHandTransform.position;
            localQZPlayerScript.lHandTransform.rotation = this.lHandTransform.rotation;
            localQZPlayerScript.playerTransform.position = this.playerTransform.position;
        }
    }

    // Simple movement for testing on PC/Editor/Controller joystick
    // helps if you cannot use headset directly in Unity Editor  (W A S D)
    private void FixedUpdate()
    {
        HandleMovement();
        HandleInput();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 100.0f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);
    }

    private void HandleInput()
    {
        // take input from focused window only
        if (!Application.isFocused)
            return;

        // input for local player
        if (localQZPlayerScript && localQZPlayerScript.isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
            }
        }

        //bool pressed;
        //rightHand.inputDevice.IsPressed(button, out pressed);

        //if (pressed)
        //{
        //    Debug.Log("Hello - " + button);
        //    audioCue.Play();
        //    localVRNetworkPlayerScript.Fire();
        //}

        if (localQZPlayerScript && localQZPlayerScript.isLocalPlayer)
        {

        }
    }
    public XRBaseController rightHand;
    public XRBaseController leftHand;

    //private void OnEnable()
    //{
    //    shootButtonRightHand.asset.Enable();
    //    shootButtonLeftHand.asset.Enable();
    //    testReference.asset.Enable();
    //}
    //private void OnDisable()
    //{
    //    shootButtonRightHand.asset.Disable();
    //    shootButtonLeftHand.asset.Disable();
    //    testReference.asset.Disable();
    //}

    private void InputActionShootButton(InputAction.CallbackContext context)
    {
    }

    private void Start()
    {
        //shootButtonLeftHand.action.performed += InputActionShootButton;
        //shootButtonRightHand.action.started += InputActionShootButton;
        //testReference.action.performed += DoChangeThing;
        //var inputDevices = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        //foreach (var device in inputDevices)
        //{
        //    Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        //}
    }

    private void DoChangeThing(InputAction.CallbackContext context)
    {
    }

    public void SetHandStatus(int _status)
    {
        //Debug.Log("Mirror SetHandStatus: " + _status);
        VRStaticVariables.handValue = _status;
    }
}
