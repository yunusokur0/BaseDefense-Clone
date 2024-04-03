using UnityEngine;

public class GunShopController : MonoBehaviour
{
    [SerializeField] private GameObject[] Guns;
    private int selectedGun;

    public void ActiveGun()
    {
        bool isGunActive = Guns[selectedGun].activeSelf;
        foreach (GameObject gun in Guns)
        {
            gun.SetActive(false);
        }
        Guns[selectedGun].SetActive(!isGunActive);
    }
}
