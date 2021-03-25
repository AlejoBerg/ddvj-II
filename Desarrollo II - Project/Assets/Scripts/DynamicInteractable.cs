using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInteractable : MonoBehaviour, IInteractable
{
    private Animator _objectAnimator;
    private bool _hadInteracted = false;

    private void Start()
    {
        _objectAnimator = GetComponent<Animator>();
    }

    public void Interact()
    {
        print($"Interactuando con {this.gameObject.name}");
        ExecuteObjectAnimation();
    }

    private void ExecuteObjectAnimation()
    {
        if (!_hadInteracted)
        {
            _objectAnimator.SetInteger("Open", 1);
            _hadInteracted = true;
            print("_objectAnimator.SetBool(Open, true)");
        }
        else
        {
            _objectAnimator.SetInteger("Open", -1);
            _hadInteracted = false;
            print("_objectAnimator.SetBool(Open, false)");
        }
    }
}
