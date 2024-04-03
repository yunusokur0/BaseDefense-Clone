using System.Collections;
using TMPro;
using UnityEngine;

public class PKAreaManager : MonoBehaviour
{
    [SerializeField] private GameObject PK;
    [SerializeField] private GameObject PKBuy;
    [SerializeField] private TextMeshPro pkBuyText;
    [SerializeField] private BuildEnum buildEnum;

    private void Awake()
    {
        LoadBuildEnum();
    }
    private void Start()
    {
        ToggleArea();
    }

    private void LoadBuildEnum()
    {
        string key = gameObject.name + "pkAreaBuildEnum1";
        if (ES3.KeyExists(key))
        {
            buildEnum = ES3.Load<BuildEnum>(key);
        }
    }

    private void ToggleArea()
    {
        bool isAreaOpen = buildEnum == BuildEnum.Open;
        PKBuy.SetActive(!isAreaOpen);
        PK.SetActive(isAreaOpen);
    }

    public void StartDecreaseNumberCoroutine()
    {
        StartCoroutine(DecreaseNumberCoroutine());
    }
    public void StopAllCoroutinesSafe()
    {
        StopAllCoroutines();
    }

    private IEnumerator DecreaseNumberCoroutine()
    {
        if (int.TryParse(pkBuyText.text, out int number))
        {
            while (number > 0)
            {
                number -= 5;
                pkBuyText.text = number.ToString();
                yield return new WaitForSeconds(0.1f);

                if (number <= 0)
                {
                    StopAllCoroutines();
                    buildEnum = BuildEnum.Open;
                    ES3.Save(gameObject.name + "pkAreaBuildEnum1", buildEnum);
                    ToggleArea();
                    break;
                }
            }
        }
    }
}
