using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    [SerializeField] ParticleSystem _finishEffect;
    private bool _finished;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !_finished)
        {
            _finished = true;
            //GameManager.Instance.FinishLevel();
            Debug.Log("Level Finished");
            //get the surface effector and stop it
            SurfaceEffector2D surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
            surfaceEffector.enabled = false;

            //play the finish effect
            _finishEffect.Play();

            //Get audio source and play the finish sound
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();

            FindObjectOfType<PlayerController>().DisableControls();

            //restart level using invoke after 3 seconds
            Invoke("RestartLevel", 5f);
        }
    }

    void Awake()
    {
        _finished = false;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
