using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerAnimController playerAnimController;
    [SerializeField] private StackManager stackManager;
    [SerializeField] private GunShopController gunShopController;
    [SerializeField] private GameObject GunShopUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DollarCollectable"))
        {
            stackManager.OnInteractionDollarCollectable(other.transform.gameObject);        
            
            if(stackManager.StackEnum == StackEnum.BulletBox)
            stackManager.StackEnum = StackEnum.Money;
        }
        if (other.CompareTag("Ammon"))
        {
            playerManager.FillBulletBoxStack();
            stackManager.StackEnum = StackEnum.BulletBox;
        }
        if (other.CompareTag("PK"))
        {
            playerManager.PKPlayerAnimated(other.gameObject);
        }
        if (other.CompareTag("Door"))
        {
            playerManager.MoneyorBulletBoxAnim();
          
        }
        if (other.CompareTag("DiamondBaseDoor"))
            DiamondAreaSignals.Instance.onSetMiner?.Invoke();

        if (other.CompareTag("GunShop"))
        {
            GunShopUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ammon"))
        {
            playerManager.StopAllCoroutines();
        }
        if (other.CompareTag("PK"))
        {
            playerManager.PKPlayerAnimatedOut();
        }
        if (other.CompareTag("Door"))
        {
            playerAnimController.ToggleAttackLayerWeight();
            playerManager.PerformAction();
            gunShopController.ActiveGun();
        }

        if (other.CompareTag("GunShop"))
        {
            GunShopUI.SetActive(false);
        }
    }
}
