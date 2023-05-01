using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;
    public Text localizedText;
    [SerializeField] private LocalizationManager localizationManager;
    private void Start()
    {
        if (localizationManager == null)
            localizationManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>().localizationManager;

        if (localizedText == null)
            localizedText = GetComponent<Text>();

        localizationManager.ChangeLangTextEvent += UpdateText;

        UpdateText();
    }
    public void UpdateText()
    {
        if (gameObject == null) return;

        if (localizationManager == null)
            localizationManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>().localizationManager;

        if (localizedText == null)
            localizedText = GetComponent<Text>();

        localizedText.text = localizationManager.GetLocalizedValue(key);
    }
    private void OnDestroy()
    {
        localizationManager.ChangeLangTextEvent -= UpdateText;
    }
}
