using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _hasMultipleAnimations = false;

    private Animator _objectAnimator;
    private bool _hadInteracted = false;

    private void Start()
    {
        _objectAnimator = GetComponent<Animator>();
    }

    public void Interact(GameObject playerRef)
    {
        if (_hasMultipleAnimations) { ExecuteObjectMultiplesAnimations(); }
        else { ExecuteObjectSingleAnimation(); }
    }

    private void ExecuteObjectMultiplesAnimations()
    {
        if (!_hadInteracted)
        {
            _objectAnimator.SetInteger("Open", 1);
            _hadInteracted = true;
        }
        else
        {
            _objectAnimator.SetInteger("Open", -1);
            _hadInteracted = false;
        }
    }

    private void ExecuteObjectSingleAnimation()
    {
        _objectAnimator.Play("BaseAnimation");
    }
}
