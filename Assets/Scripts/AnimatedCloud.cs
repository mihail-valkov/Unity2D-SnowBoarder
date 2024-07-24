using UnityEngine;

public class AnimatedCloud : MonoBehaviour
{
    [SerializeField] private float _forceLeft = 0.00001f;
    [SerializeField] private float _forceUp = 0.00001f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Cloud: " + this.name);
        Rigidbody2D cloudRb = this.GetComponent<Rigidbody2D>();
        cloudRb.AddForce(Vector2.left * this._forceLeft, ForceMode2D.Force);
        cloudRb.AddForce(Vector2.up * this._forceUp, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
