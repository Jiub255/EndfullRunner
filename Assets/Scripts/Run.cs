using System;
using UnityEngine;

public class Run : MonoBehaviour
{
    public static event Action<Transform> OnKnockedOff;

	[SerializeField]
	private float _speed = 5f;

	private Rigidbody _rb;
    private bool _alive = true; 

    private void OnEnable()
    {
        _rb = GetComponentInParent<Rigidbody>();

        _rb.AddForce(Vector3.left * _speed, ForceMode.Impulse);
    }

    private void Update()
    {
        // TODO: Problems when knocking off a runner who isn't in the lead. SetNewLeadRunner gets called and the queue gets dequeued, 
        // but it should remove the queue entry at the correct index instead, and only call set new lead if the lead gets knocked off. 

        // Just testing, put this on another script if it gets used. 
        // If this runner gets knocked off, let camera know to start following the next runner in line (Or to stop and wait until another spawns). 
        if ((transform.position.y < -1.5f || Mathf.Abs(transform.position.z) > 1.5f) && _alive)
        {
            Debug.Log("Runner fell");
            // Null reference. How? 
            OnKnockedOff?.Invoke(transform.parent);
            _alive = false;
            Destroy(transform.parent.gameObject, 1f);
        }
    }
}