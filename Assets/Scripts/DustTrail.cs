using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dustTrail;

    //audio clips for random snowboarding effects
    [SerializeField] private AudioClip[] _boardSFX;
    private PlayerController _playerController;

    public bool PlayerEnabled
    {
        get
        {
            if (_playerController == null)
            {
                _playerController = FindObjectOfType<PlayerController>();
            }

            return _playerController.enabled;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && PlayerEnabled)
        {
            _dustTrail.Play();
            //play a random snowboarding sound effect
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(_boardSFX[Random.Range(0, _boardSFX.Length)]);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _dustTrail.Stop();
        }
    }
}
