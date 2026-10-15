using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField]
    private PumpkinController pumpkin;
    [SerializeField]
    private GameObject ghost;
    private bool disabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pumpkin") && !disabled)
        {
            disabled = true;
            pumpkin.Ghost(ghost);
        }
    }
}
