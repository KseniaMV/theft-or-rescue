using UnityEngine;
public class LastAchievementUI : MonoBehaviour
{
    [SerializeField] private Sprite _spriteClosedAchievement;
    [SerializeField] private AllDataSave _allDataSave;
    [SerializeField] private GameObject[] _oldPanels;
    [SerializeField] private GameObject[] _newPanels;
    private void Awake()
    {
        if(_allDataSave == null)
            _allDataSave = GameObject.FindGameObjectWithTag("MainManager").GetComponent<AllDataSave>();
    }
    private void Start()
    {
        if (_allDataSave.NameTotalLastAchievement != null)
        {
            string lastAch = _allDataSave.NameTotalLastAchievement;
            string[] type = lastAch.Split("_");

            ButtonAchievement ach = null;

            if (type[0] == "Gold")
                ach = Resources.Load<ButtonAchievement>($"Achievements/Gold/GoldAchievement_{type[1]}");
            else if (type[0] == "Silver")
                ach = Resources.Load<ButtonAchievement>($"Achievements/Silver/SilverAchievement_{type[1]}");

            var buttonAch = Instantiate(ach, transform);
            buttonAch.oldPanels = _oldPanels;
            buttonAch.newPanels = _newPanels;
        }
    }
}
