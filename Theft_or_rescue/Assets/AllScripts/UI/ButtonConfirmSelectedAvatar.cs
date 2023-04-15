using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonConfirmSelectedAvatar : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private Button _button;
    [SerializeField] private int _numberSelectedAvatar;
    [SerializeField] private GameObject _welcomePanel;

    private bool _canPress;
    private void Awake()
    {
        if (_welcomePanel == null)
            _welcomePanel = transform.parent.Find("WelcomePanel").gameObject;
        
        if (_button == null)
            _button = GetComponent<Button>();
        
        _welcomePanel.SetActive(false);
        _button.interactable = false;
    }
    private void Start()
    {
        StartCoroutine(OpenWelcomePanel());
    }
    private IEnumerator OpenWelcomePanel()
    {
        if (AllDataSave.NumberAvatar != 0)
        { 
            yield return new WaitForSeconds(.5f);

            _welcomePanel.SetActive(true);

            yield return new WaitForSeconds(3f);

            _welcomePanel.SetActive(false);
        }
        StopCoroutine(OpenWelcomePanel());
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
