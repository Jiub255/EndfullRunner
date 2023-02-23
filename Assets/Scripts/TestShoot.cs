using UnityEngine;

public class TestShoot : MonoBehaviour
{
    [SerializeField]
    private float _speed = 15f;

	private Rigidbody _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.forward * _speed, ForceMode.Impulse);
        }
    }
}