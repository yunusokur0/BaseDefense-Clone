using UnityEngine;
using UnityEngine.Events;

public class CameraSignals : MonoSignleton<CameraSignals>
{
    public UnityAction<GameStates> onChangeCameraState = delegate { };
    public UnityAction<Transform> onSetCinemachineTarget = delegate { };
}