using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField]
    private PumpkinController pumpkin;
    [SerializeField]
    private GameObject money;
    private bool disabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pumpkin") && !disabled)
        {
            disabled = true;
            pumpkin.Money();
            money.SetActive(false);
        }
    }
}
