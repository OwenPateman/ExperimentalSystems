using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class IgnoreCollisionWithSocket : MonoBehaviour
{

    XRSocketInteractor _socket;

    [SerializeField]
    Collider _ourCollider = null;
    Collider _theirCollider;

    private void Awake()
    {
        _socket = GetComponent<XRSocketInteractor>();
        Assert.IsNotNull( _socket );

        if (_ourCollider == null)
        {
            _ourCollider = GetComponent<Collider>();
        }

        _socket.selectEntered.AddListener(OnSelectEntered);
        _socket.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject other = args.interactableObject.transform.gameObject;
        _theirCollider = other.GetComponent<Collider>();

        //Ignore On
        Physics.IgnoreCollision(_ourCollider, _theirCollider, true);
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        //Ignore Off
        Physics.IgnoreCollision(_ourCollider, _theirCollider, false);
    }
}
