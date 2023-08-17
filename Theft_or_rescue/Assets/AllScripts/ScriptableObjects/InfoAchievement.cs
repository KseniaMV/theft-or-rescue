using UnityEngine;
[CreateAssetMenu]
public class InfoAchievement : ScriptableObject
{
    [SerializeField] private string _keyForTranslate;
    [SerializeField] private Sprite _sprite;

    public string KeyForTranslate => _keyForTranslate;
    public Sprite Sprite => _sprite;
}
