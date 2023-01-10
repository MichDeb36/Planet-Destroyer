using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class TeleportController : MonoBehaviour
{

    public GameObject controller;
    public GameObject teleportation;

    public InputActionReference teleportActivationReference;

    public UnityEvent onTeleportActivate;
    public UnityEvent onTeleportCancel;

    // Start is called before the first frame update
    void Start()
    {
        teleportActivationReference.action.performed += TeportModeActivate;
        teleportActivationReference.action.canceled += TeportModeCancel;
    }

    private void TeportModeActivate(InputAction.CallbackContext obj)
    {
        onTeleportActivate.Invoke();
    }

    private void TeportModeCancel(InputAction.CallbackContext obj)
    {
        Invoke("DeactiveteTeleport", 0.1f);
    }

    void DeactiveteTeleport()
    {
        onTeleportCancel.Invoke();
    }
}
