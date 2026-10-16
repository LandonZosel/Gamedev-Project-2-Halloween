using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 winningOffset;
    [SerializeField]
    private Vector3 targetRotation;
    [SerializeField]
    private Vector3 winningTargetRotation;

    [SerializeField]
    private float smoothing;
    private bool shot = false;
    private bool winning = false;

    void FixedUpdate()
    {
        if (!shot)
        {
            return;
        }

        if (winning)
        {
            Vector3 winningTargetPosition = target.position + winningOffset;

            transform.position = Vector3.Lerp(transform.position, winningTargetPosition, 1 * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(winningTargetRotation), 1 * Time.deltaTime);
            return;
        }

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), smoothing * Time.deltaTime);
        
    }

    public void Shoot()
    {
        shot = true;
    }

    public void StartWinning()
    {
        winning = true;
    }
}
