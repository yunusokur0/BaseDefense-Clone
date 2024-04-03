using UnityEngine;

public class InputManager : MonoBehaviour
{
    private JoystickCommand _joystickCommand;
    private Vector3 _joystickPos, _moveVector, _mousePositon;
    private bool _isTouching, isReadyForTouch;
    [SerializeField] private Joystick joystick;
    public PlayerAnimController PlayerAnim;

    public bool _isPKInput;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _joystickCommand = new JoystickCommand(ref _joystickPos, ref _moveVector, ref joystick);
    }
    private void OnPlay()
    {
        isReadyForTouch = true;
    }
    private void Update()
    {
        if (!isReadyForTouch) return;
        if (Input.GetMouseButtonDown(0))
        {
            _isTouching = true;
            _mousePositon = Input.mousePosition;
            PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            if(!_isPKInput)
            PlayerAnim.OnChangeAnimationState(AnimEnum.Run);
            PlayerAnim.OnChangeAnimationState(AnimEnum.GunRun);
        }

        if (Input.GetMouseButton(0))
        {
            if (_isTouching)
            {
                if (_mousePositon != null)
                {
                    _joystickCommand.Execute();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isTouching = false;
            PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            if(!_isPKInput)
            PlayerAnim.OnChangeAnimationState(AnimEnum.Idle);
            PlayerAnim.OnChangeAnimationState(AnimEnum.GunIdle);
        }
    }
    private void OnPKInput(bool n) => _isPKInput = n;
    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
        InputSignals.Instance.onPKInput += OnPKInput;
    }
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        InputSignals.Instance.onPKInput -= OnPKInput;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
