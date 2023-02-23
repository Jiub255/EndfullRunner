using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField]
	private float _launchForce = 25f;

    [SerializeField]
    private Vector3 _direction = Vector3.forward;

    private Rigidbody _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        Launch();
    }

    private void Launch()
    {
        _rb.AddForce(_direction * _launchForce, ForceMode.Impulse);
    }
}