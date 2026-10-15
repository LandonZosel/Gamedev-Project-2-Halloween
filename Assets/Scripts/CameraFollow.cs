using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 targetRotation;

    [SerializeField]
    private float smoothing;
    private bool shot = false;

    void FixedUpdate()
    {
        if (!shot)
        {
            return;
        }
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        // if (transform.rotation.eulerAngles != targetRotation)
        // {
            
        // }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), smoothing * Time.deltaTime);
        
    }

    public void Shoot()
    {
        shot = true;
    }
}
