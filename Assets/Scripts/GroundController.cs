using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField]
    private Transform pumpkin;
    [SerializeField]
    private GameObject plane;
    private GameObject planeBL;
    private GameObject planeTL;
    private GameObject planeBM;
    private GameObject planeTM;
    private GameObject planeBR;
    private GameObject planeTR;
    private float currentZThreshold = 500f;
    private float currentXThreshold = 500f;
    private float increment = 1000f;

    void Start()
    {
        planeBL = Instantiate(plane, new Vector3(-1000, 0, 0), Quaternion.identity);
        planeTL = Instantiate(plane, new Vector3(-1000, 0, 1000), Quaternion.identity);
        planeBM = Instantiate(plane, new Vector3(0, 0, 0), Quaternion.identity);
        planeTM = Instantiate(plane, new Vector3(0, 0, 1000), Quaternion.identity);
        planeBR = Instantiate(plane, new Vector3(1000, 0, 0), Quaternion.identity);
        planeTR = Instantiate(plane, new Vector3(1000, 0, 1000), Quaternion.identity);
    }

    void Update()
    {
        if (pumpkin.position.z > currentZThreshold)
        {
            planeBL.transform.position = new Vector3(planeBL.transform.position.x, 0, planeBL.transform.position.z + increment);
            planeTL.transform.position = new Vector3(planeTL.transform.position.x, 0, planeTL.transform.position.z + increment);
            planeBM.transform.position = new Vector3(planeBM.transform.position.x, 0, planeBM.transform.position.z + increment);
            planeTM.transform.position = new Vector3(planeTM.transform.position.x, 0, planeTM.transform.position.z + increment);
            planeBR.transform.position = new Vector3(planeBR.transform.position.x, 0, planeBR.transform.position.z + increment);
            planeTR.transform.position = new Vector3(planeTR.transform.position.x, 0, planeTR.transform.position.z + increment);
            currentZThreshold += increment;
        }

        if (pumpkin.position.x > currentXThreshold)
        {
            planeBL.transform.position = new Vector3(planeBL.transform.position.x + increment, 0, planeBL.transform.position.z);
            planeTL.transform.position = new Vector3(planeTL.transform.position.x + increment, 0, planeTL.transform.position.z);
            planeBM.transform.position = new Vector3(planeBM.transform.position.x + increment, 0, planeBM.transform.position.z);
            planeTM.transform.position = new Vector3(planeTM.transform.position.x + increment, 0, planeTM.transform.position.z);
            planeBR.transform.position = new Vector3(planeBR.transform.position.x + increment, 0, planeBR.transform.position.z);
            planeTR.transform.position = new Vector3(planeTR.transform.position.x + increment, 0, planeTR.transform.position.z);
            currentXThreshold += increment;
        }

        if (pumpkin.position.x < currentXThreshold - increment)
        {
            planeBL.transform.position = new Vector3(planeBL.transform.position.x - increment, 0, planeBL.transform.position.z);
            planeTL.transform.position = new Vector3(planeTL.transform.position.x - increment, 0, planeTL.transform.position.z);
            planeBM.transform.position = new Vector3(planeBM.transform.position.x - increment, 0, planeBM.transform.position.z);
            planeTM.transform.position = new Vector3(planeTM.transform.position.x - increment, 0, planeTM.transform.position.z);
            planeBR.transform.position = new Vector3(planeBR.transform.position.x - increment, 0, planeBR.transform.position.z);
            planeTR.transform.position = new Vector3(planeTR.transform.position.x - increment, 0, planeTR.transform.position.z);
            currentXThreshold -= increment;
        }
    }
}
