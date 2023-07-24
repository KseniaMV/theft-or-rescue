using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleChangeLanguage : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] Sprite _ruSprite;
    [SerializeField] Sprite _enSprite;
    [SerializeField] private Image _image;

    private string _currentLAnguage;
    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeActiveToogle();
    }
    private void Awake()
    {
         if (_image == null)
            _image = GetComponent<Image>();
    }
    private void Start()
    {
        string currentLanguage = PlayerPrefs.GetString("Language");

        if (currentLanguage == "ru_RU")
        {
            _currentLAnguage = "ru_RU";
            _image.sprite = _ruSprite;
        }
        else if (currentLanguage == "en_US")
        {
            _currentLAnguage = "en_US";
            _image.sprite = _enSprite;
        }
    }
    private void ChangeActiveToogle()
    {
        if (_image.sprite == _ruSprite)
        {
            _image.sprite = _enSprite;
            _currentLAnguage = "en_US";
        }
        else if (_image.sprite == _enSprite)
        {
            _image.sprite = _ruSprite;
            _currentLAnguage = "ru_RU";
        }

        mainManager.options.ToggleVolumeEffects();

        mainManager.localizationManager.CurrentLanguage = _currentLAnguage;
        mainManager.localizationManager.LoadLocalizedText(_currentLAnguage);
    }
}
