using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectAvatar : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private int _numberAvatar;
    [SerializeField] private Image _imageSelectedAvatar;
    [SerializeField] private ButtonConfirmSelectedAvatar _buttonConfirmSelected;
    private void Awake()
    {
        _numberAvatar = transform.GetSiblingIndex() + 1;
        _imageSelectedAvatar.gameObject.SetActive(false);

        if (_imageSelectedAvatar == null)
            _imageSelectedAvatar = transform.Find("ImageSelectedAvatar").GetComponent<Image>();

        if (_buttonConfirmSelected)
            _buttonConfirmSelected = transform.parent.parent.GetComponentInChildren<ButtonConfirmSelectedAvatar>();

    }
    private void Start()
    {
        mainManager.eventManager.ButtonSelectAvatarPressedEvent += CloseOtherSelectImageAvatar;
    }
    private void OnEnable()
    {
        CheckSelectingThisAvatar();
    }
    private void CheckSelectingThisAvatar()
    {
        if (mainManager.allDataSave.NumberAvatar == _numberAvatar)
            _imageSelectedAvatar.gameObject.SetActive(true);
    }
    private void CloseOtherSelectImageAvatar()
    {
        _imageSelectedAvatar.gameObject.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio();
        mainManager.eventManager.ButtonSelectAvatarPressed();
        _imageSelectedAvatar.gameObject.SetActive(true);
        _buttonConfirmSelected.SelectAvatar(_numberAvatar);
    }
    private void OnDestroy()
    {
        mainManager.eventManager.ButtonSelectAvatarPressedEvent -= CloseOtherSelectImageAvatar;
    }
}
