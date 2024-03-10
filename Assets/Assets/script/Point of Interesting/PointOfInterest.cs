using UnityEngine;
using UnityEngine.Events;

public class PointOfInterest : MonoBehaviour
{
    [SerializeField] private UnityEvent onActivated;
    [SerializeField] private UnityEvent onDeactivated;
    [SerializeField] private Transform target;
    [Range(-1f,1f)][SerializeField] private float actovationThreshold;

    private void Update()
    {
        var direction = this.target.position - this.transform.position;

        var targetForward = this.target.forward;

        if(Vector3.Dot(direction.normalized, targetForward.normalized)<= this.actovationThreshold)
            this.onActivated?.Invoke();
        else
           this.onDeactivated?.Invoke();
    }
}
