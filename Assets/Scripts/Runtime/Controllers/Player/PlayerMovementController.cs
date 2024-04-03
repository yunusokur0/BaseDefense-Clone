using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Vector3 _joystickValue;
    private bool _isReadyToMove, _isReadyToPlay;

    [SerializeField] private new Rigidbody rigidbody;

    public bool isTurret;
    public PlayerAttackController pk;
    public GameObject target;
    private float rotationSpeed = 4;
    public void UpdateInputValue(HorizontalInputParams inputParams)
    {
        _joystickValue = inputParams.joystickValue;
    }

    private void FixedUpdate()
    {
        if (pk.enemyObjectStack.Count > 0)
            rigidbody.transform.GetChild(0).LookAt(pk.enemyObjectStack[^1].transform);

        if (!_isReadyToPlay)
        {
            if (_isReadyToMove)
            {
                if(isTurret == false)
                {
                    MoveJoystick();
                    CameraSignals.Instance.onChangeCameraState?.Invoke(GameStates.Run);
                    CameraSignals.Instance.onSetCinemachineTarget?.Invoke(transform);
                }
                if (isTurret == true)
                {
                    TurretMove();
                   
                }            
            }
            else
            {
                Stop();                
            }
        }
    }
    private void TurretMove()
    {
        float rotationAmount = _joystickValue.x;
        float rotationAmounty = _joystickValue.z;
        target.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0,
                          transform.rotation.y,
                          0),
                      Quaternion.Euler(0,
                          Mathf.Clamp(rotationAmount * 60,
                              -60,
                              60),
                          0),
                      1f);
        if(rotationAmounty<-0.9f)
        {
            isTurret = false;
           
        }
    }
    private void MoveJoystick()
    {
        Vector3 direction = new Vector3(_joystickValue.x*2.5f, 0, _joystickValue.z*2.5f);
        rigidbody.velocity = direction;

        if (direction != Vector3.zero)
        {
            Transform firstChild = transform.GetChild(0);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            firstChild.rotation = Quaternion.Slerp(firstChild.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

    }


    public void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
    private void IsTurret(bool vas) => isTurret = vas;
    private void OnMoveConditionChanged(bool arg0) => _isReadyToMove = arg0;
    private void OnPlayerConditionChange(bool arg0) => _isReadyToPlay = arg0;
    private void OnEnable() => SubscribeEvents();
    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputDragged += UpdateInputValue;
        InputSignals.Instance.onPKInput += IsTurret;
        PlayerSignals.Instance.onPlayerConditionChanged += OnPlayerConditionChange;
        PlayerSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
    }
    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputDragged -= UpdateInputValue;
        InputSignals.Instance.onPKInput -= IsTurret;
        PlayerSignals.Instance.onPlayerConditionChanged -= OnPlayerConditionChange;
        PlayerSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
    }
    private void OnDisable() => UnSubscribeEvents();
}
