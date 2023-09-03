using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance { get; private set; }

    private string _currentLanguage;
    private Dictionary<string, string> _localizedText;

    public delegate void ChangeLangText();
    public event ChangeLangText ChangeLangTextEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        SelectLanguage();
    }
    public void  SelectLanguage()
    { 
         if (!PlayerPrefs.HasKey("Language"))
         {
            if (Application.systemLanguage == SystemLanguage.Russian ||
                Application.systemLanguage == SystemLanguage.Ukrainian ||
                Application.systemLanguage == SystemLanguage.Belarusian)

                PlayerPrefs.SetString("Language", "ru_RU");
            else
                PlayerPrefs.SetString("Language", "en_US");
         }

         _currentLanguage = PlayerPrefs.GetString("Language");
         LoadLocalizedText(_currentLanguage);
    }
    public string CurrentLanguage
    {
        get
        {
            return _currentLanguage;
        }
        set
        {
            PlayerPrefs.SetString("Language", value);
            _currentLanguage = PlayerPrefs.GetString("Language");
        }
    }
    public void LoadLocalizedText(string langName)
    {
        string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json";

        if (File.Exists(path))
        {
            string dataAsJson;
#if UNITY_ANDROID && !UNITY_EDITOR
            WWW reader = new WWW(path);
            UnityWebRequest unityWebRequest = new UnityWebRequest(path);
            while(!reader.isDone){}
            dataAsJson = reader.text;
#else
            dataAsJson = File.ReadAllText(path);
#endif

            LocalizationData localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            _localizedText = new Dictionary<string, string>();

            for (int i = 0; i < localizationData.items.Length; i++)
                _localizedText.Add(localizationData.items[i].key, localizationData.items[i].value);

            CurrentLanguage = langName;

            if (langName != null)
                ChangeLangTextEvent?.Invoke();
            else
                Debug.Log($"ключ для перевода отсутствует на {transform.parent.gameObject.name}");
        }
        else
        {
            throw new System.Exception("Cannot find file!");
        }
    }
    public string GetLocalizedValue(string key)
    {
        if (_localizedText.ContainsKey(key))
            return _localizedText[key];
        else
            throw new System.Exception("Localization text with key \"" + key + "\" not found");
    }
}
