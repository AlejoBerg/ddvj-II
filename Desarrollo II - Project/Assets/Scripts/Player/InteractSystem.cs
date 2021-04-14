using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(CharacterInput))]
public class InteractSystem : MonoBehaviour
{
    #region Private Variables
    [Header("Object References")]
    [SerializeField] private Camera _cameraRef = null;
    [SerializeField] private GameObject _pickupImageRoot = null;

    [Header("Extra Parameters")]
    [SerializeField] private LayerMask _layerMaskToDetect = 0;
    private GameObject _itemBeingPicked = null;
    private CharacterInput _characterInputRef;
    #endregion


    private void Start()
    {
        _characterInputRef = GetComponent<CharacterInput>();
        _characterInputRef.OnInteractButtonPressed += OnInteractButtonPressedHandler;
    }

    private void Update()
    {
        SelectItemBeingPickedUpFromRay();
    }

    private void OnInteractButtonPressedHandler()
    {
        if (HasItemTargetted())
        {
            _itemBeingPicked.GetComponent<IInteractable>().Interact(this.gameObject);
        }
    }

    private void SelectItemBeingPickedUpFromRay()
    {
        Ray ray = _cameraRef.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 2f, _layerMaskToDetect))
        {
            var hitItem = hitInfo.collider.gameObject;
            var interactableItem = hitItem.GetComponent<IInteractable>();

            if (interactableItem != null)
            {
                _itemBeingPicked = hitItem;
                _pickupImageRoot.SetActive(true);
            }
            else
            {
                _itemBeingPicked = null;
            }
        }
        else
        {
            _itemBeingPicked = null;
            _pickupImageRoot.SetActive(false);
        }
    }

    private bool HasItemTargetted()
    {
        return _itemBeingPicked != null;
    }
}
