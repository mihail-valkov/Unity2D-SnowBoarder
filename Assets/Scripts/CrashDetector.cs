using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem _crashEffect;
    [SerializeField] private AudioClip _crashSFX;


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Entered Trigger: " + other.tag + " / " + other.name);
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Crash Detected");

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
