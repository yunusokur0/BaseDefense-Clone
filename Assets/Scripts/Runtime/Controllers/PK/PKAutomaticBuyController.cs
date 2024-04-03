using UnityEngine;
using TMPro;
using System.Collections;

public class PKAutomaticBuyController : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject PKPlayer;
    [SerializeField] private GameObject textObject;
    public BuildEnum buildStatus;

    private void Awake()
    {
        LoadBuildStatus();
    }

    private void Start()
    {
        ActivatePKPlayerBasedOnStatus();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DecreaseNumberCoroutine());
        }
    }
    private void LoadBuildStatus()
    {
        string key = gameObject.name + "pkAreaBuildStatus";
        if (ES3.KeyExists(key))
        {
            buildStatus = ES3.Load<BuildEnum>(key);
        }
    }

    private void SaveBuildStatus()
    {
        string key = gameObject.name + "pkAreaBuildStatus";
        ES3.Save(key, buildStatus);
    }

    private void ActivatePKPlayerBasedOnStatus()
    {
        bool isBuildOpen = buildStatus == BuildEnum.Open;
        PKPlayer.SetActive(isBuildOpen);
        textObject.SetActive(!isBuildOpen);
    }

    private IEnumerator DecreaseNumberCoroutine()
    {
        if (int.TryParse(text.text, out int number))
        {
            while (number > 0)
            {
                number -= 5;
                text.text = number.ToString();
                yield return new WaitForSeconds(0.1f);

                if (number <= 0)
                {
                    StopAllCoroutines();
                    buildStatus = BuildEnum.Open;
                    SaveBuildStatus();
                    ActivatePKPlayerBasedOnStatus();
                    break;
                }
            }
        }
    }
}
