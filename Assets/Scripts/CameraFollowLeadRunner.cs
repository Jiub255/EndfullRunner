using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollowLeadRunner : MonoBehaviour
{
	[SerializeField]
	private Vector3 offset = new Vector3(-22.42f, 9f, -10f);

    [SerializeField]
    private RunnerQueue _runnerQueue;

    [SerializeField]
    private float _smoothTime = 0.3f; 

	private Transform _leadRunner;
    private Transform _transform;
    private Vector3 _runnerSpawnPosition;
    private Vector3 _velocity = Vector3.zero; 
    private Vector3 _velocity2 = Vector3.zero; 

    private void OnEnable()
    {
        _transform = transform;
        _runnerSpawnPosition = FindObjectOfType<RunnerSpawner>().transform.position;

        // Set camera to look at spawn point until the first runner instantiates. 
        _transform.position = _runnerSpawnPosition + offset; 

        Run.OnKnockedOff += HandleKnockOff;
    }

    private void OnDisable()
    {
        Run.OnKnockedOff -= HandleKnockOff;
    }

    private void Update()
    {
        // If there is a lead runner, follow them. 
        if (_leadRunner != null)
        {
            _transform.position = Vector3.SmoothDamp(_transform.position, _leadRunner.position + offset, ref _velocity, _smoothTime);
        }
        // If not, wait until next runner is instantiated, then make them _leadRunner. 
        else
        {
            // Do I need a separate velocity variable for this? 
            _transform.position = Vector3.SmoothDamp(_transform.position, _runnerSpawnPosition + offset, ref _velocity2, _smoothTime);

            if (_runnerQueue.Runners.Count > 0)
            {
                _leadRunner = _runnerQueue.Runners.Peek();
            }
        }
    }

    private void HandleKnockOff(Transform knockedOffTransform)
    {
        // If the lead runner is the one who got knocked off, set new lead runner. 
        if (knockedOffTransform == _runnerQueue.Runners.Peek())
        {
            if (_runnerQueue.Runners.Count <= 1)
            {
                Debug.Log("No next lead runner available, waiting until one instantiates."); 
            
                _runnerQueue.DQ(); 

                // Wait until next runner is instantiated, then make them _leadRunner (happens in update). 
                _leadRunner = null;
            }
            else
            {
                Debug.Log("Setting next lead runner."); 

                _runnerQueue.DQ(); 

                // Set next lead runner. 
                _leadRunner = _runnerQueue.Runners.Peek(); 
            }
        }
        // If it wasn't the lead runner, remove the appropriate index from runner queue. 
        else
        {
            Debug.Log("Runner knocked off wasn't lead. Removing them from queue.");

            _runnerQueue.Runners = new Queue<Transform>(_runnerQueue.Runners.Where(s => s != knockedOffTransform));
        }
    }
}