using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAchievement : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    public Button button;
    public TypeAchievement type;
    public string _keyForTranslate;
    public Sprite _spriteAchievement;

    [SerializeField] private AchievementPanels _openPanel;
    [SerializeField] private Image _imageAchievement;
    [SerializeField] private Image _imageOpenedAchievement;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        if (_imageAchievement == null)
            _imageAchievement = GetComponent<Image>();

        _imageAchievement.sprite = _spriteAchievement;

        if (_imageOpenedAchievement == null)
            _imageOpenedAchievement = GetComponentInChildren<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.achievementsManager.OpenPanel(true, _openPanel);
        mainManager.achievementsManager.descriptionAchevment.UpdateText(_openPanel, _keyForTranslate);
        PressButton();
    }
    private void PressButton()
    {
        base.OpenNewPanel();
    }
}
