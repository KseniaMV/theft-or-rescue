using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;

    [SerializeField] private AudioClip _locationSound;

    [SerializeField] private Options _options;

    private void Awake()
    {
        if (_options == null)
            _options = transform.parent.GetComponentInChildren<Options>();
    }
}
