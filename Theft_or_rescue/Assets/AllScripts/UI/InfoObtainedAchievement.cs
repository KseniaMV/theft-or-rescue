using UnityEngine;
using UnityEngine.UI;

public class InfoObtainedAchievement : MonoBehaviour
{
    [SerializeField] private AllDataSave _allDataSave;

    [Header("Level")]
    [SerializeField] private Text _textValueLevel;

    [Header("Level")]
    [SerializeField] private Text _textValueResult;

    [Header("Phrase")]
    [SerializeField] private Transform _phrase;

    [Header("ImageAchievement")]
    [SerializeField] private Image _imageAchievement;

    private void Awake()
    {
        if (_textValueLevel == null)
            _textValueLevel = transform.Find("Text value level").GetComponent<Text>();

        if (_textValueResult == null)
            _textValueResult = transform.Find("Text value result").GetComponent<Text>();

        if (_imageAchievement == null)
            _imageAchievement = transform.Find("Image Achievement").GetComponent<Image>();

        if (_phrase == null)
            _phrase = transform.Find("Text phrase");

        if (_allDataSave == null)
            _allDataSave = GameObject.FindGameObjectWithTag("MainManager").GetComponentInChildren<AllDataSave>();
    }
    private void Start()
    {
        CheckLastAchievement();
    }
    private void CheckLastAchievement()
    {
        ButtonAchievement buttonAchievement = null;
        LocalizedText localText = _phrase.gameObject.AddComponent<LocalizedText>();

        if (_allDataSave.LastAchievement != null)
        {
            string[] info = _allDataSave.LastAchievement.Split("_");

            if (info[0] == "1")
                buttonAchievement = Resources.Load<ButtonAchievement>($"Achievements/Gold/GoldAchievement_{info[1]}");
            else if (info[0] == "2")
                buttonAchievement = Resources.Load<ButtonAchievement>($"Achievements/Silver/SilverAchievement_{info[1]}");

            _textValueResult.text = $"{_allDataSave.NumberCurrentWins} / 10";
            _textValueLevel.text = $"{_allDataSave.NumberCompletedGames + 1}";
            localText.key = buttonAchievement._keyForTranslate;
            _imageAchievement.sprite = buttonAchievement._spriteAchievement;
        }
        else
            localText.key = "NoAchievemets";
    }
}
