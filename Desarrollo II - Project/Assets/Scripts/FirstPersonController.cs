using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    #region VisibleOnInspector_Variables
    [Header("Object References")]
    [SerializeField] private Transform _cameraTransform = null;
    [SerializeField] private Rigidbody _rigidBody = null;

    [Header("Camera values")]
    [SerializeField] private float _camRotationSpeed = 5f;
    [SerializeField] private float _camRotationMinY = -60f;
    [SerializeField] private float _camRotationMaxY = 75f;
    [SerializeField] private float _camRotationSmoothSpeed = 10f;
    
    [Header("Player values")]
    [SerializeField] private float _walkSpeed = 9f;
    [SerializeField] private float _runSpeed = 14f;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _jumpPower = 10f;
    [SerializeField] private float _extraGravity = 30f;
    #endregion

    #region NotVisibleOnInspector_Variables
    private float _currentBodyRotationX;
    private float _currentCamRotationY;

    private float _speed;
    private bool _isGrounded;
    private bool _isRunning;

    public bool IsRunning { get => _isRunning; set => _isRunning = value; }
    #endregion

    private void Update()
    {
        LookRotation();
    }

    private void FixedUpdate()
    {
        AddExtraGravity();
    }

    private void LookRotation()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Get camera and body rotation values
        _currentBodyRotationX += Input.GetAxis("Mouse X") * _camRotationSpeed;
        _currentCamRotationY += Input.GetAxis("Mouse Y") * _camRotationSpeed;

        //Clamp Camera
        _currentCamRotationY = Mathf.Clamp(_currentCamRotationY, _camRotationMinY, _camRotationMaxY);

        //Handle rotation of the body and camera
        Quaternion cameraTargetRotation = Quaternion.Euler(-_currentCamRotationY, 0, 0);// --> Porque _currentCamRotationY tiene que ir negado?
        Quaternion bodyTargetRotation = Quaternion.Euler(0, _currentBodyRotationX, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargetRotation, _camRotationSmoothSpeed * Time.deltaTime);
        _cameraTransform.localRotation = Quaternion.Lerp(_cameraTransform.localRotation, cameraTargetRotation, _camRotationSmoothSpeed * Time.deltaTime);
    }

    public void Move()
    {
        if(_isRunning == true) { _speed = _runSpeed; }
        else { _speed = _walkSpeed; }

        Vector3 camX = _cameraTransform.right;
        camX.y = 0;

        Vector3 camY = _cameraTransform.forward;
        camY.y = 0;

        Vector3 newDirX = camX * Input.GetAxis("Horizontal") * _speed;
        Vector3 newDirY = camY * Input.GetAxis("Vertical") * _speed;

        _rigidBody.velocity = newDirX + newDirY + Vector3.up * _rigidBody.velocity.y;
        _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _maxSpeed);
    }

    public void Jump()
    {
        if(GroundCheck() == true)
        {
            _rigidBody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    private void AddExtraGravity()
    {
        _rigidBody.AddForce(Vector3.down * _extraGravity);
    }

    private bool GroundCheck()
    {
        RaycastHit groundHit;
        _isGrounded = Physics.Raycast(transform.position, -transform.up, out groundHit, 1.25f);
        return _isGrounded;
    }
}
