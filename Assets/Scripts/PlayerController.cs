using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float _speedLimitForward = 60f;
    [SerializeField] private float _speedLimitBackward = -20f;
    [SerializeField] private float _speedGain = 2f;
    [SerializeField] private float _surfaceEffectorNormalSpeed = 50f;

    private SurfaceEffector2D _surfaceEffector;
    private Rigidbody2D rb;
    private Coroutine _resetSpeedCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
        _surfaceEffector.speed = _surfaceEffectorNormalSpeed;
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        HandleTorque();
        HandleSpeedAdjustment();
    }

    private void HandleTorque()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddTorque(-torqueAmount);
        }
    }

    private void HandleSpeedAdjustment()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            AdjustSpeed(-_speedGain, _speedLimitBackward);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            AdjustSpeed(_speedGain, _speedLimitForward);
        }
    }

    private void AdjustSpeed(float speedChange, float speedLimit)
    {
        _surfaceEffector.speed += speedChange;
        _surfaceEffector.speed = Mathf.Clamp(_surfaceEffector.speed, _speedLimitBackward, _speedLimitForward);

        if (_resetSpeedCoroutine != null)
        {
            StopCoroutine(_resetSpeedCoroutine);
        }
        _resetSpeedCoroutine = StartCoroutine(GraduallyResetSpeed());
    }

    private IEnumerator GraduallyResetSpeed()
    {
        yield return new WaitForSeconds(1.5f);

        while (Mathf.Abs(_surfaceEffector.speed - _surfaceEffectorNormalSpeed) > Mathf.Epsilon)
        {
            _surfaceEffector.speed = Mathf.MoveTowards(_surfaceEffector.speed, _surfaceEffectorNormalSpeed, _speedGain);
            yield return new WaitForSeconds(0.5f);
        }

        _surfaceEffector.speed = _surfaceEffectorNormalSpeed;
    }
}
