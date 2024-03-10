using UnityEngine;

public class ObjectHold : MonoBehaviour
{
    public GameObject FRObject;
    public GameObject SNObject;
    public Transform PlayerTransform;
    public float range = 3f;
    public float Go = 100f;
    public Camera Camera;
    private Transform targetTransform;

    void Update()
    {
        if (Input.GetButtonDown("Fire7"))
            StartPickUp();

        if (Input.GetButtonUp("Fire7"))
            Carry(false, null);
    }

    void StartPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            targetTransform = hit.transform.GetComponent<Target>()?.transform;
            if (targetTransform != null)
                Carry(true, this.transform);
        }
    }

    void Carry(bool physicsEnabled, Transform parent)
    {
        if (targetTransform is not { }) return;

        targetTransform.GetComponentInChildren<Rigidbody>(true).isKinematic = physicsEnabled;
        targetTransform.SetParent(parent);
    }
}
