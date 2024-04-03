using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
    [SerializeField] private Animator _animator;
    private Vector3 _initialPosition;
    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        CameraSignals.Instance.onChangeCameraState += OnGetGameState;
        CoreGameSignals.Instance.onPlay += OnPlay;
        CameraSignals.Instance.onSetCinemachineTarget += OnSetCameraTarget;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnsubscribeEvents()
    {
        CameraSignals.Instance.onChangeCameraState += OnGetGameState;
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CameraSignals.Instance.onSetCinemachineTarget -= OnSetCameraTarget;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    private void Awake()
    {
        GetInitialPosition();
    }
    private void GetInitialPosition()
    {
        _initialPosition = transform.GetChild(0).localPosition;
    }

    private void MoveToInitialPosition()
    {
        transform.GetChild(0)
            .localPosition = _initialPosition;
    }
    private void OnSetCameraTarget(Transform _target)
    {
        stateDrivenCamera.Follow = _target;
        GetInitialPosition();
    }

    private void SetCameraState(CameraStates cameraState)
    {
        _animator.SetTrigger(cameraState.ToString());
    }

    private void OnGetGameState(GameStates states)
    {
        switch (states)
        {
            case GameStates.Run:
                SetCameraState(CameraStates.Run);
                break;
            case GameStates.Idle:
                SetCameraState(CameraStates.Idle);
                stateDrivenCamera.Follow = null;
                break;
        }
    }
    private void OnPlay()
    {

        GetInitialPosition();
    }
    private void OnReset()
    {
        stateDrivenCamera.Follow = null;
        stateDrivenCamera.LookAt = null;
        MoveToInitialPosition();
    }
}
