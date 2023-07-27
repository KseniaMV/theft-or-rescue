using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Character : MonoBehaviour
{
     [SerializeField] private float _runSpeed;
     [SerializeField] private float _stopSpeed;
    [Space(5)]
    [SerializeField] private DisappearanceTypes _disappearanceTypes;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _particleSystemPosition;
    [Space(5)]
    public Animator animator;
    [SerializeField] private AudioClip _winAudio;
    [SerializeField] private AudioClip _loseAudio;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SpriteRenderer _thingHolder;

    private Vector2 _startPosition;
    private Coroutine _timerStartRunning;
    private Coroutine _timerStopRunnig;
    private bool _canDisappearance;
    private enum DisappearanceTypes { RunLeft = 1, RunRight, Disappearance = 3}
    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _startPosition = transform.position;
    }
    private IEnumerator StartRunning()
    {
        while (transform.position.x < 0)
        {
            transform.Translate(Vector2.right * _runSpeed * Time.deltaTime);
            yield return null;
        }

        if (transform.position.x >= 0)
            Coroutines.StopRoutine(_timerStartRunning);
    }
    private void PlayAudio(bool win)
    {
        if (_audioSource != null)
        {
            if (win)
                _audioSource.clip = _winAudio;
            else
                _audioSource.clip = _loseAudio;

            _audioSource.Play();
        }
    }
    public void StartAnimAndAudio(bool lose)
    {
        if (lose)
        {
            PlayAudio(lose);
            animator.SetTrigger("Win");
        }
        else
        {
            PlayAudio(lose);
            animator.SetTrigger("Lose");
        }
    }
    public void Run(bool start)
    {
        if (start)
            _timerStartRunning = Coroutines.StartRoutine(StartRunning());
        else
        {
            switch (_disappearanceTypes)
            {
                case DisappearanceTypes.RunLeft:
                    _timerStopRunnig = Coroutines.StartRoutine(StopRunningLeft());
                    break;

                case DisappearanceTypes.RunRight:
                    _timerStopRunnig = Coroutines.StartRoutine(StopRunningRight());
                    break;

                case DisappearanceTypes.Disappearance:
                    _canDisappearance = true;
                    break;

                default:
                    _canDisappearance = true;
                    break;
            }
        }
    }
    public void GetPosition()
    {
        transform.localPosition =  Vector3.zero;
    }
    public void OffThing()
    {
        _thingHolder.sprite = null;
    }
    private IEnumerator StopRunningLeft()
    {
        while (transform.position.x > _startPosition.x)
        {
            transform.Translate(Vector2.left * _stopSpeed * Time.deltaTime);
            yield return null;
        }

        if (transform.position.x <= _startPosition.x)
        {
            gameObject.SetActive(false);
            Coroutines.StopRoutine(_timerStopRunnig);
        }
    }
    private IEnumerator StopRunningRight()
    {
        while (transform.position.x < Mathf.Abs(_startPosition.x))
        {
            transform.Translate(Vector2.right * _runSpeed * Time.deltaTime);
            yield return null;
        }

        if (transform.position.x >= Mathf.Abs(_startPosition.x))
        {
            gameObject.SetActive(false);
            Coroutines.StopRoutine(_timerStopRunnig);
        }
    }
    private void  DisappearanceAnimEvent()
    {
        if (_particleSystem != null && _canDisappearance)
        {
            var exp = Instantiate(_particleSystem, _particleSystemPosition);
            exp.transform.parent = null;
            exp.Play();
        }
        gameObject.SetActive(false);
    }
}
