using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField]
    private PumpkinController pumpkin;
    private bool disabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pumpkin") && !disabled)
        {
            disabled = true;
            pumpkin.Ghost();
        }
    }
}
