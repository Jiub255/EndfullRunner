using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Runner Queue", fileName = "New Runner Queue")]
public class RunnerQueue : ScriptableObject
{
	public Queue<Transform> Runners = new Queue<Transform>();

	public void NQ(Transform transform)
    {
        Runners.Enqueue(transform);
        //Debug.Log($"Enqueued {transform.name}, Count: {Runners.Count}");
    }

    public Transform DQ()
    {
        //Debug.Log($"About to try to dequeue, Runners count: {Runners.Count}");
        if (Runners.Count > 0)
        {
            return Runners.Dequeue();
        }
        return null;
    }
}