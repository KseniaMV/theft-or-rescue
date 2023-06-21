using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAction : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private bool _answer;      //thief = false; resc = true;
    [SerializeField] private Button _button;
    [SerializeField] private MainManager _mainManager;
    private void Start()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        if (_mainManager == null)
            _mainManager = GameObject.FindGameObjectWithTag("MainManager")
                .transform.parent.GetComponentInChildren<MainManager>();

        _mainManager.eventManager.ButtonsActionInteractableEvent += ButtonInteractable;
    }
    private void ButtonInteractable(bool interactable)
    {
        _button.interactable = interactable;    
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            mainManager.options.ClickAudio(true);
            mainManager.eventManager.ButtonActionPressed(_answer); 
        }
    }
    private void OnDestroy()
    {
        _mainManager.eventManager.ButtonsActionInteractableEvent -= ButtonInteractable;
    }
}
