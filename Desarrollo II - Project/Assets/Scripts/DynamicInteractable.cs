using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _closeOnExit = false;

    public void Interact()
    {
        print($"Interactuando con {this.gameObject.name}");
        //Open Door animation
    }

    private void OnTriggerExit(Collider other)
    {
        if (_closeOnExit)
        {
            //Close door animation
        }
    }
}
