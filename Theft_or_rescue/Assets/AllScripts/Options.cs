using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Options : MonoBehaviour
{
    public static Options instance{ get; private set; }

    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private AudioSource _audioSourceMusicSceneAS;

    [Header("Effects")]
    private bool effectsVolumeEnabled = true;
    [SerializeField] private Slider _sliderMusic;

    [Header("UI Audio")]
    [SerializeField] private AudioSource _clickButtonAS;
    [SerializeField] private AudioClip _clipButton;
    [SerializeField] private AudioClip _clipButtonAction;

    private float valueVolumeMusic;
    private float valueVolumeEffects;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        StartMusicOnCurrentScene();
        LoadData();
    }
    private void LoadData()
    {
        valueVolumeMusic = PlayerPrefs.GetFloat("MusicVolume");
        ChangeVolumeMusic(valueVolumeMusic);
        _sliderMusic.value = valueVolumeMusic;

        valueVolumeEffects = PlayerPrefs.GetFloat("EffectsVolume");
        _mixer.audioMixer.SetFloat("EffectsVolume", valueVolumeEffects);

        if (valueVolumeEffects == 0)
            effectsVolumeEnabled = true;
        else
            effectsVolumeEnabled = false;
    }
    /// <summary>
    /// запуск музыки на текущей сцене
    /// </summary>
    private void StartMusicOnCurrentScene()
    {
        if(_audioSourceMusicSceneAS != null)
        _audioSourceMusicSceneAS.Play();
    }
    /// <summary>
    /// проигрывание звука нажатии кнопки
    /// </summary>
    public void ClickAudio(bool isActionButton = false)
    {
        if (_clickButtonAS != null && !isActionButton)
        {
            _clickButtonAS.clip = _clipButton;
            _clickButtonAS.Play();
        }
        else if (_clickButtonAS != null && isActionButton)
        {
            _clickButtonAS.clip = _clipButtonAction;
            _clickButtonAS.Play();
        }
    }
    /// <summary>
    /// выход из игры
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    ///  слайдер настройки громкости музыки
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeVolumeMusic(float volume)
    {
        _mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
        valueVolumeMusic = volume;

        PlayerPrefs.SetFloat("MusicVolume", valueVolumeMusic);
    }
    /// <summary>
    /// тумблер звука еффектов
    /// </summary>
    public void ToggleVolumeEffects()
    {
        if (!effectsVolumeEnabled)
        {
            valueVolumeEffects = 0;
            _mixer.audioMixer.SetFloat("EffectsVolume", valueVolumeEffects);
            effectsVolumeEnabled = true;
        }
        else
        {
            valueVolumeEffects = -80;
            _mixer.audioMixer.SetFloat("EffectsVolume", valueVolumeEffects);
            effectsVolumeEnabled = false;
        }

        PlayerPrefs.SetFloat("EffectsVolume", valueVolumeEffects);
        ClickAudio();
    }
}
