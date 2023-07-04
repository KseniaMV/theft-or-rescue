using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAchievement : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    public Button button;
    public TypeAchievement type;
    public string puplicKeyForTranslate;//текущий общий
    public Sprite publicSpriteAchievement;

    [SerializeField] private Sprite _currentAchievementSprite;
    [SerializeField] private Sprite _closedSpriteAchievement;
    [SerializeField] private AchievementPanels _openPanel;
    [SerializeField] private Image _imageAchievement;
    //[SerializeField] private Image _imageOpenedAchievement;
    
    private string closedKeyForTranslate;
    private bool _achievementClosed;
    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        closedKeyForTranslate = puplicKeyForTranslate;
        publicSpriteAchievement = _currentAchievementSprite;

        if (_imageAchievement == null)
            _imageAchievement = GetComponent<Image>();

        _imageAchievement.sprite = publicSpriteAchievement;

        //if (_imageOpenedAchievement == null)
        //    _imageOpenedAchievement = GetComponentInChildren<Image>();
    }
    /// <summary>
    /// для выбора типа панели, если true, то малый тип
    /// </summary>
    /// <param name="isClosed"></param>
    public void AchievementClosed(bool isClosed)
    {
        _achievementClosed = isClosed;
    }
    /// <summary>
    /// публичный спрайт = закрытому
    /// </summary>
    public void GetClosedAchievementSprite()
    {
        publicSpriteAchievement = _closedSpriteAchievement;
        _imageAchievement.sprite = publicSpriteAchievement;
    }
    /// <summary>
    ///  публичный спрайт = изначальному
    /// </summary>
    public void GetCurrentAchievementSprite()
    {
        publicSpriteAchievement = _currentAchievementSprite;
        _imageAchievement.sprite = publicSpriteAchievement;
    }
    /// <summary>
    /// публичный ключ = изначальному
    /// </summary>
    public void GetCurrentKeyAChievement()
    {
        puplicKeyForTranslate = closedKeyForTranslate;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        AchievementPanels achievementPanels;

        if (_achievementClosed)
            achievementPanels = AchievementPanels.LittlePanel;
        else
            achievementPanels = _openPanel;

        mainManager.achievementsManager.OpenPanel(true, achievementPanels);
        mainManager.achievementsManager.descriptionAchevment.UpdateText(achievementPanels, puplicKeyForTranslate);
        base.OpenNewPanel();
    }
}
