using UnityEngine;

public class HouseAlarm : MonoBehaviour 
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _fadeDuration = 5.0f;

    private float _targetVolume;
    private float _currentVolume;

    private void Start()
    {
        _audioSource.volume = 0f;
        _currentVolume = _audioSource.volume;
    }

    private void Update()
    {
        if (_audioSource != null)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _targetVolume, Time.deltaTime / _fadeDuration);
            _audioSource.volume = _currentVolume;

            if (_currentVolume == 0f && _targetVolume == 0f && _audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }

    public void AdjustVolume(bool increase)
    {
        _targetVolume = increase ? 1f : 0f;

        if (increase && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
}
