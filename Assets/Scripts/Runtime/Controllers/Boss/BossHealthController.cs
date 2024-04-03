using System.Collections;
using TMPro;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    [SerializeField] private GameObject fillRateHealtObj;
    [SerializeField] private TextMeshPro HealthText;
    [SerializeField] private GameObject HealthTextObj; 
    [SerializeField] private BossManager bossManager;

    public void UpdateHealScaleText(int heal)
    {
        HealthText.text = heal.ToString();
        var scalex = (heal / 500f);
        fillRateHealtObj.transform.localScale = new Vector3(scalex, 1, 1);
    }

    public void StartRegeneration()
    {
        StartCoroutine(Regeneration());
    }

    IEnumerator Regeneration()
    {
        WaitForSeconds waiter = new WaitForSeconds(0.1f);

        while (true)
        {
            if (bossManager.Heal == 500)
            {
                HealthTextObj.SetActive(false);
                break;
            }

            bossManager.Heal++;
            yield return waiter;
        }
    }
}
