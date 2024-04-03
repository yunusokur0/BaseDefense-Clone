using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerHealthController : MonoBehaviour
{
    public GameObject fillRateHealtObj;
    public TextMeshPro HealthText;

    public GameObject HealthTextObj;
    public PlayerManager pm;

    public void UpdateHealScaleText(int heal)
    {
        HealthText.text = heal.ToString();
        var scalex = (heal / 100f);
        fillRateHealtObj.transform.localScale = new Vector3(scalex, 1, 1);
    }
    public void StartCoroutine()
    {
        StartCoroutine(Regeneration());
    }
    private IEnumerator Regeneration()
    {
        WaitForSeconds waiter = new WaitForSeconds(0.1f);

        while (true)
        {
            if (pm.Heal >= 100)
            {
                HealthTextObj.SetActive(!HealthTextObj.activeSelf);
                yield break; 
            }

            pm.Heal++;
            yield return waiter;
        }
    }
}
