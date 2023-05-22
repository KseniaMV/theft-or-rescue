using UnityEngine;
using UnityEngine.UI;
public class ImageAvatar : MonoBehaviour
{
    [SerializeField] private Image _imageAvatar;
    [SerializeField] private MainManager _mainManager;

    private void OnEnable()
    {
        if (_mainManager == null)
            _mainManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();

        if (_imageAvatar == null)
            _imageAvatar = GetComponent<Image>();
    }
    private void Start()
    {
        
        if(_mainManager.allDataSave.NumberAvatar != 0)
            _imageAvatar.sprite = Resources.Load<SpriteRenderer>($"Avatars/Avatar_{_mainManager.allDataSave.NumberAvatar}").sprite;
    }
}
