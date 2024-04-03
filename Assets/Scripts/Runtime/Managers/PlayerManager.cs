using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamage
{
    #region Variable
    [SerializeField] private StackManager stackManager;
    [SerializeField] private PlayerMovementController playerMoveControll;
    [SerializeField] private PlayerAnimController playerAnimController;
    [SerializeField] private GameObject levelHolder;
    [SerializeField] private PlayerHealthController playerHealthController;
    private int _heal = 100;

    public int Heal
    {
        get => _heal;
        set
        {
            _heal = value;
            playerHealthController.UpdateHealScaleText(value);
        }
    }
    #endregion

    public void FillBulletBoxStack()
    {
        StartCoroutine(stackManager.FillBulletBoxStack());
    }
    public void PKPlayerAnimated(GameObject PK)
    {
        playerMoveControll.target = PK;
        transform.SetParent(PK.transform);
        transform.DOLocalMove(new Vector3(-0.026f, 0, -0.45f), .5f);
        transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, 0), 0.5f).SetDelay(0.2f);

        playerAnimController.OnChangeAnimationState(AnimEnum.Hold);
        InputSignals.Instance.onPKInput?.Invoke(true);
        CameraSignals.Instance.onSetCinemachineTarget?.Invoke(gameObject.transform);
        CameraSignals.Instance.onChangeCameraState?.Invoke(GameStates.Idle);

        PKManager pkManager = PK.GetComponent<PKManager>();
        StartCoroutine(pkManager.SpawnBullet());
    }

    public void PKPlayerAnimatedOut()
    {
        playerMoveControll.target = null;
        transform.SetParent(levelHolder.transform);
        InputSignals.Instance.onPKInput?.Invoke(false);
        playerAnimController.OnChangeAnimationState(AnimEnum.Run);
        StopAllCoroutines();
    }

    public void MoneyorBulletBoxAnim()
    {
        stackManager.MoneyorBulletBoxAnim();
    }
    public void PerformAction()
    {
        playerHealthController.StartCoroutine();
    }

    public void TakeDamage(int damage)
    {
        Heal -= damage;
        if (Heal <= 0)
        {
            Heal = 0;
        }
    }
    private void OnPlay()
    {
        PlayerSignals.Instance.onPlayerConditionChanged?.Invoke(false);
        CameraSignals.Instance.onSetCinemachineTarget?.Invoke(transform);
    }

    private void OnEnable() => SubscribeEvents();
    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
    }
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
    }
    private void OnDisable() => UnSubscribeEvents();
}
