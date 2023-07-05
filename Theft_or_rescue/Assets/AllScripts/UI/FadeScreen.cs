using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Transform _holder;

    public void StatFadeScreen()
    {
        _holder.gameObject.SetActive(true);
        StartCoroutine(FadingScreen());
    }
    private IEnumerator FadingScreen()
    {
        foreach (var item in _images)
            item.transform.parent.gameObject.SetActive(true);

        float time = 0;
        float alfa = 0;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            alfa = time / _fadeDuration;

            foreach (var item in _images)
                item.color = new Color(item.color.r, item.color.g, item.color.b, alfa);
            yield return null;
        }
        if (time >= _fadeDuration)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}
