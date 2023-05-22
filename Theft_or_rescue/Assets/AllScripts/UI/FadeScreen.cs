using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Transform _holder;

    public void StatFadeScreen()
    {
        _holder.gameObject.SetActive(true);
        StartCoroutine(FadingScreen());
    }
    private IEnumerator FadingScreen()
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
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}
