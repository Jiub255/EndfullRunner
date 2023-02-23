using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

	private Vector3 _launchPosition;

    [SerializeField]
    private float _timerLength = 1f;
    private float _timer;

    private void OnEnable()
    {
        _launchPosition = transform.GetChild(0).position;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _timerLength && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_projectilePrefab, _launchPosition, Quaternion.identity);
            _timer = 0f;
        }
    }
}