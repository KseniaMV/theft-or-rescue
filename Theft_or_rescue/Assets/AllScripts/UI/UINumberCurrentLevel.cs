using UnityEngine;
using UnityEngine.UI;

public class UINumberCurrentLevel : MonoBehaviour
{
    [SerializeField] private AllDataSave _allDataSave;
    [SerializeField] private Text _text;
    private void Start()
    {
        if (_allDataSave == null)
            _allDataSave = GameObject.FindGameObjectWithTag("MainManager").transform.parent.GetComponentInChildren<AllDataSave>();

        if (_text == null)
            _text = GetComponent<Text>();

        _text.text = _allDataSave.NumberTotalWins/*NumberCompletedGames*/.ToString();
    }
}
