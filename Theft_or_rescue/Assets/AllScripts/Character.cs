using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Range(0f, .1f)] [SerializeField] private float _runSpeed;
    [Range(0f, .1f)] [SerializeField] private float _stopSpeed;

    public Animator animator;

    private Vector2 _startPosition;
    private Coroutine _timerStartRunning;
    private Coroutine _timerStopRunnig;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        _startPosition = transform.position;
    }
    private IEnumerator StartRunning()
    {
        while (transform.position.x < 0)
        {
            transform.Translate(Vector2.right * _runSpeed);
            yield return null;
        }

        if (transform.position.x >= 0)
        {
            Coroutines.StopRoutine(_timerStartRunning);
        }
    }
    public void StartAnim(bool lose)
    {
        if (lose)
            animator.SetTrigger("Win");
        else
            animator.SetTrigger("Lose");
    }
    public void Run(bool start)
    {
        if(start)
            _timerStartRunning = Coroutines.StartRoutine(StartRunning());
        else
            _timerStopRunnig = Coroutines.StartRoutine(StopRunning());
    }
    private IEnumerator StopRunning()
    {
        while (transform.position.x > _startPosition.x)
        {
            transform.Translate(Vector2.left * _stopSpeed);
            yield return null;
        }

        if (transform.position.x <= _startPosition.x)
        {
            gameObject.SetActive(false);
            Coroutines.StopRoutine(_timerStopRunnig);
        }
    }
}
