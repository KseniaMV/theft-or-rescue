using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToogleSoindEffects : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] Sprite _activeSprite;
    [SerializeField] Sprite _notActiveSprite;
    [SerializeField] private Image _image;

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeActiveToogle();
    }

    private void Start()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        float valueVolumeEffects = PlayerPrefs.GetFloat("EffectsVolume");

        if (valueVolumeEffects == 0)
            _image.sprite = _activeSprite;
        else
            _image.sprite = _notActiveSprite;
    }
    private void ChangeActiveToogle()
    {
        if (_image.sprite == _activeSprite)
            _image.sprite = _notActiveSprite;
        else if (_image.sprite == _notActiveSprite)
            _image.sprite = _activeSprite;
        
        mainManager.options.ToggleVolumeEffects();
    }
}
