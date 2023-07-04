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

        if (_phrase == null )
            _phrase = transform.Find("Text phrase");

        if (_allDataSave == null)
            _allDataSave = GameObject.FindGameObjectWithTag("MainManager").transform.parent.GetComponentInChildren<AllDataSave>();
    }
    public void CheckLastAchievement(int numChance, int numWins)
    {
        ButtonAchievement buttonAchievement = null;
        LocalizedText localText = _phrase.gameObject.AddComponent<LocalizedText>();

        if (_allDataSave.LastCurrentAchievement == null)
        {
            if (numChance == 1)
                buttonAchievement = Resources.Load<ButtonAchievement>($"Achievements/Gold/GoldAchievement_{1}");
            else if (numChance == 2)
                buttonAchievement = Resources.Load<ButtonAchievement>($"Achievements/Silver/SilverAchievement_{1}");

            if(_textValueResult != null)
                _textValueResult.text = $"{numWins} / 10";

            _textValueLevel.text = $"{_allDataSave.NumberCompletedGames + 1}";
            localText.key = buttonAchievement.puplicKeyForTranslate;
            _imageAchievement.sprite = buttonAchievement.publicSpriteAchievement;
        }
        else
            localText.key = "NoAchievemets";
    }
}
