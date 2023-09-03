using UnityEngine;
using UnityEngine.UI;

public class InfoObtainedAchievement : MonoBehaviour// окно продолжить/нет в игре
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
        InfoAchievement achievement = null;
        LocalizedText localText = _phrase.gameObject.AddComponent<LocalizedText>();

        if (_allDataSave.LastCurrentAchievement == null)
        {
            if (numChance == 1)
                achievement = Resources.Load<InfoAchievement>($"Achievements/Gold/GoldAchievement_{numWins}");
            else if (numChance == 2)
                achievement = Resources.Load<InfoAchievement>($"Achievements/Silver/SilverAchievement_{numWins}");

            if(_textValueResult != null)
                _textValueResult.text = $"{numWins} / 10";

            _textValueLevel.text = $"{_allDataSave.NumberCompletedGames + 1}";

            localText.key = achievement.KeyForTranslateDescription;
            _imageAchievement.sprite = achievement.Sprite;
        }
        else
            localText.key = "NoAchievemets";
    }
}
