using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectAvatar : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private int _numberAvatar;
    [SerializeField] private Image _imageSelectedAvatar;
    private void Awake()
    {
        _numberAvatar = transform.GetSiblingIndex() + 1;

        if (_imageSelectedAvatar == null)
            _imageSelectedAvatar = transform.Find("ImageSelectedAvatar").GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio();
        mainManager.SelectNumberAvatar(_numberAvatar);
        //open button confirm selecting
    }
}
