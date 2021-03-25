using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        print($"Interactuaste con {this.gameObject.name} , sera destruido...");
        Destroy(this.gameObject);
    }
}
