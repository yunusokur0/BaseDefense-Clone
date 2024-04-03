using System.Collections;
using TMPro;
using UnityEngine;


public class AreaManager : MonoBehaviour
{
    [SerializeField] private GameObject openArea;
    [SerializeField] private GameObject closeArea;
    [SerializeField] private TextMeshPro areaText;
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
        string key = gameObject.name + "areaBuildEnum";
        if (ES3.KeyExists(key))
        {
            buildEnum = ES3.Load<BuildEnum>(key);
        }
    }
    private void ToggleArea()
    {
        bool isOpen = buildEnum == BuildEnum.Open;
        closeArea.SetActive(!isOpen);
        openArea.SetActive(isOpen);
    }

    public void StartDecreaseValueCoroutine()
    {
        StartCoroutine(DecreaseNumberCoroutine());
    }
    public void StopAllCoroutinesSafe()
    {
        StopAllCoroutines();
    }

    private IEnumerator DecreaseNumberCoroutine()
    {
        if (int.TryParse(areaText.text, out int number))
        {
            while (number > 0)
            {
                number -= 5;
                areaText.text = number.ToString();
                yield return new WaitForSeconds(0.1f);
                if (number <= 0)
                {
                    StopAllCoroutines();
                    buildEnum = BuildEnum.Open;
                    ES3.Save(gameObject.name + "areaBuildEnum", buildEnum);
                    ToggleArea();
                    break;
                }
            }
        }
    }
}
