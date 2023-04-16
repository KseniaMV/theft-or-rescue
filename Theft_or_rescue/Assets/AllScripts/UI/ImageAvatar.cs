using UnityEngine;
using UnityEngine.UI;
public class ImageAvatar : MonoBehaviour
{
    [SerializeField] private Image _imageAvatar;
    private void Start()
    {
        if (_imageAvatar == null)
            _imageAvatar = GetComponent<Image>();
    }
    private void OnEnable()
    {
        if(AllDataSave.NumberAvatar != 0)
            _imageAvatar.sprite = Resources.Load<SpriteRenderer>($"Avatars/Avatar_{AllDataSave.NumberAvatar}").sprite;
    }
}
