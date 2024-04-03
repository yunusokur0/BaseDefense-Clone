using System.Collections;
using TMPro;
using UnityEngine;


public class BorderAreaManager : MonoBehaviour
{
    [SerializeField] private GameObject borderArea;
    [SerializeField] private TextMeshPro borderBuyText;
    [SerializeField] private BuildEnum buildEnum;

    private void Awake()
    {
        if (ES3.KeyExists(gameObject.name + "areaBuildEnum1"))
        {
            buildEnum = ES3.Load<BuildEnum>(gameObject.name + "areaBuildEnum1");
        }
    }
    private void Start()
    {
        ToggleArea();
    }

    private void ToggleArea()
    {
        switch (buildEnum)
        {
            case BuildEnum.Close:
                borderArea.SetActive(false);
                break;

            case BuildEnum.Open:
                borderArea.SetActive(true);
                break;
        }
    }
    public void StartCor()
    {
        StartCoroutine(DecreaseValueOverTime());
    }
    public void StopCoroutine()
    {
        StopAllCoroutines();
    }
    public IEnumerator DecreaseValueOverTime()
    {
        while (true)
        {
            if (int.Parse(borderBuyText.text) <= 0)
            {
                buildEnum = BuildEnum.Close;
                ES3.Save(gameObject.name + "areaBuildEnum1", buildEnum);
                borderArea.SetActive(false);
            }
            int value = int.Parse(borderBuyText.text);
            value -= 1;
            borderBuyText.text = value.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
