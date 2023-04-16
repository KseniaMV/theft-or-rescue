using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStartGame : AbstractButton, IPointerDownHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeDuration;
    private void OnEnable()
    {
        if (_image == null)
            _image = transform.parent.Find("Fade Screen Panel").GetComponentInChildren<Image>();

        _image.transform.parent.gameObject.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        mainManager.options.ClickAudio();

        mainManager.allDataSave.SaveAll();

        if (AllDataSave.NumberAvatar != 0)
            StartCoroutine(FadeScreen());
    }
    private IEnumerator FadeScreen()
    {
        _image.transform.parent.gameObject.SetActive(true);
        float time = 0;
        float alfa = 0;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            alfa = time / _fadeDuration;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alfa);
            yield return null;
        }
        if (time >= _fadeDuration)
            SceneManager.LoadScene("Game");
    }
}
