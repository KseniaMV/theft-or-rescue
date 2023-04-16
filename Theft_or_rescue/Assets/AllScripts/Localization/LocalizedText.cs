using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private LocalizationManager localizationManager;
    private Text text;
    private void Start()
    {
        if (localizationManager == null)
            localizationManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>().localizationManager;

        if (text == null)
            text = GetComponent<Text>();

        localizationManager.ChangeLangTextEvent += UpdateText;

        UpdateText();
    }
    public void UpdateText()
    {
        if (gameObject == null) return;

        if (localizationManager == null)
            localizationManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>().localizationManager;

        if (text == null)
            text = GetComponent<Text>();

        text.text = localizationManager.GetLocalizedValue(key);
    }
    private void OnDestroy()
    {
        localizationManager.ChangeLangTextEvent -= UpdateText;
    }
}
