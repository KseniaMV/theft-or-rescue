using UnityEngine;
[CreateAssetMenu]
public class InfoAchievement : ScriptableObject
{
    [SerializeField] private string _keyForTranslateDescription;
    [SerializeField] private Sprite _sprite;

    public string KeyForTranslateDescription => _keyForTranslateDescription;
    public Sprite Sprite => _sprite;
}
