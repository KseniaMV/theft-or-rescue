using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonConfirmSelectedAvatar : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private Button _button;

    private int _numberSelectedAvatar;
    private bool _canPress;
    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<Button>();
        
        _button.interactable = false;
    }
    public void SelectAvatar(int number)
    {
        _button.interactable = true;
        _canPress = true;
        _numberSelectedAvatar = number;
    }
    private void ConfirmSelectedAvatar()
    {
        mainManager.allDataSave.ConfirmSelectedNumberAvatar(_numberSelectedAvatar);
        _button.interactable = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_canPress)
        {
            mainManager.options.ClickAudio();
            ConfirmSelectedAvatar();
            base.OpenNewPanel();
        }
    }
}
