using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class ButtonAchievement : AbstractButton, IPointerDownHandler
{
    [Header("Button Data")]
    [SerializeField] private Button button;
    [SerializeField] private TypeAchievement _type;
    [SerializeField] private AchievementPanels _openPanel;
    [SerializeField] private Sprite _spriteClosedMedal;
    [SerializeField] private Sprite _spriteOpenedMedal;
    [SerializeField] private Image _imageMedal;
    [SerializeField] private Text _textName;
    [SerializeField] private RectTransform _rectImage;
    [SerializeField] private float _time;
    [SerializeField] private InfoAchievement _infoAchievement;

    private Vector2 _startPos;
    private float _endPos;
    private bool _isShining;
    private int _numberOpenedAchievement;

    private void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        if (_rectImage == null)
            _rectImage = transform.Find("Image Glow").GetComponent<RectTransform>();

        if (_imageMedal == null)
            _imageMedal = transform.Find("Image medal").GetComponent<Image>();

        if (_textName == null)
            _textName = transform.Find("Text name").GetComponent<Text>();

        _startPos = _rectImage.transform.localPosition;
        _endPos = -_startPos.x;
        _rectImage.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (_isShining)
            Shining(true);
    }
    private void OnDisable()
    {
        if (!_isShining)
            Shining(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.achievementsManager.OpenPanel(_openPanel, true);

        if (_numberOpenedAchievement >= 0)
        {
            _isShining = false;
            Shining(false);
            mainManager.achievementsManager.TranslateTextPanel(_infoAchievement.KeyForTranslate);

            AcceptAchievement();
        }
        else
        {
            UpdateUIAchievement('0');
            mainManager.achievementsManager.TranslateTextPanel(null);
        }
    }

    public void Shining(bool play)
    {
        if (play)
        {
            _isShining = true;
            _rectImage.gameObject.SetActive(true);
            _rectImage.transform.localPosition = _startPos;
            _rectImage.DOAnchorPosX(_endPos, _time, true);

            DOVirtual.DelayedCall(_time, () =>
             {
                 _rectImage.DOAnchorPosX(_startPos.x, _time, true);
             });
            DOVirtual.DelayedCall(_time * 2, () =>
            {
                Shining(true);
            });
        }
        else if (!_isShining && !play)
        {
            DOTween.Kill(gameObject);
            _rectImage.gameObject.SetActive(false);
            _rectImage.transform.localPosition = _startPos;
        }
    }
    public void UpdateUIAchievement(char type)
    {
        switch (type)
        {
            case '0':
                _textName.text = "**********";
                _imageMedal.sprite = _spriteClosedMedal;
                break;

            case '1':
                Shining(true);
                _textName.text = "**********";
                _imageMedal.sprite = _spriteOpenedMedal;
                break;

            case '2':
                _textName.text = transform.name;
                _imageMedal.sprite = _spriteOpenedMedal;
                break;

            default:
                break;
        }
    }
    private void AcceptAchievement()
    {
        if (_type == TypeAchievement.Gold)
        {
            string golden = mainManager.allDataSave.GoldenAchievements;
            golden = golden.Remove(_numberOpenedAchievement, 1).Insert(_numberOpenedAchievement, "2");

            mainManager.allDataSave.SaveGoldenAchievement(golden);
        }
        else if (_type == TypeAchievement.Silver)
        {
            string silver = mainManager.allDataSave.SilverAchievements;
            silver = silver.Remove(_numberOpenedAchievement, 1).Insert(_numberOpenedAchievement, "2");

            mainManager.allDataSave.SaveSilverAchievement(silver);
        }

        UpdateUIAchievement('2');
    }

    public void NumberOpenedAchievement(int number)
    {
        _numberOpenedAchievement = number;
    }
}
