using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private float _rotationSpeed = 100;

    private FirstPersonController _fpcRef;
    private bool _isInteracting = false;
    private Vector3 _initialPosition = Vector3.zero;

    private void Update()
    {
        if (_isInteracting)
        {
            _fpcRef.ChangeFSMState(FSMStates.EXAMINE);
            this.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * _rotationSpeed);
        }
    }

    IEnumerator MoveObject(Vector3 endPos, float time = 0.2f)
    {
        while (Vector3.Distance(endPos, this.transform.position) > 0.1f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, endPos, time);
            yield return null;
        }
    }

    public void Interact(GameObject playerRef)
    {
        _fpcRef = playerRef.GetComponent<FirstPersonController>();

        if (!_isInteracting)
        {
            _initialPosition = transform.position;

            StartCoroutine(MoveObject(_fpcRef.ExamineObjectSocket.position));

            _isInteracting = true;

        }
        else
        {
            StartCoroutine(MoveObject(_initialPosition));
            _isInteracting = false;

            _fpcRef.ChangeFSMState(FSMStates.IDLE);
        }
        
    }
}
