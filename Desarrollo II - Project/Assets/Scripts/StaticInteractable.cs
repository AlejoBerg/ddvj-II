using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StaticInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private float _rotationSpeed = 100;
    private bool _isDragging = false;
    private Rigidbody _objectRB = null;

    private void Start()
    {
        _objectRB = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1")) 
        {
            _isDragging = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isDragging)
        {
            float x = Input.GetAxis("Mouse X") * _rotationSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * _rotationSpeed * Time.fixedDeltaTime;

            _objectRB.AddTorque(Vector3.down * x);
            _objectRB.AddTorque(Vector3.right * y);
        }
    }

    public void Interact()
    {
        _isDragging = true;
    }
}
