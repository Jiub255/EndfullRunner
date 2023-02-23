using UnityEngine;

public class RunnerSpawner : MonoBehaviour
{
	[SerializeField]
	private float _timerLength = 2f;
	private float _timer;

	[SerializeField]
	private GameObject _runnerPrefab;

	[SerializeField]
	private RunnerQueue _runnerQueue; 

    private void Update()
    {
		_timer += Time.deltaTime;
		if (_timer > _timerLength)
        {
			Transform runner = Instantiate(_runnerPrefab).transform;
			_runnerQueue.NQ(runner);
			_timer = 0;
        }
    }
}