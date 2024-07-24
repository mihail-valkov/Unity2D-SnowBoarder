using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem _crashEffect;
    [SerializeField] private AudioClip _crashSFX;
    private bool _hasCrashed;

    void Awake()
    {
        _hasCrashed = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Entered Trigger: " + other.tag + " / " + other.name);
        if (other.CompareTag("Ground") && !_hasCrashed)
        {
            _hasCrashed = true;
            Debug.Log("Crash Detected");

            //stop player input
            FindObjectOfType<PlayerController>().DisableControls();

            //get the surface effector and stop it
            SurfaceEffector2D surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
            surfaceEffector.enabled = false;

            //play the crash effect
            _crashEffect.Play();

            //Get audio source and play the crash sound
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(_crashSFX);

             //restart level in a coroutine after 3 seconds
            StartCoroutine(RestartLevel());
        }
    }
    
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
