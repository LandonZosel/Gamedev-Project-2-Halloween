using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    private Image powerBar;
    [SerializeField]
    private Image powerBarBackground;
    [SerializeField]
    private PumpkinController pumpkin;
    private float currentPowerFill = 0f;
    private int rotationPolarity = 1;
    private int shootPolarity = 1;
    private float shootForce = 10;
    private bool step1Done = false;
    private bool step2Done = false;
    private bool step3Done = false;
    private bool shot = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePowerBar();
        powerBarBackground.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!step1Done)
        {
            transform.Rotate(new Vector3(0, 1 * rotationPolarity, 0));

            if (transform.eulerAngles.y >= 60 && transform.eulerAngles.y <= 180)
            {
                rotationPolarity = -1;
            } else if (transform.eulerAngles.y <= 300 && transform.eulerAngles.y >= 180)
            {
                rotationPolarity = 1;
            }
        } else if (!step2Done)
        {
            transform.Rotate(new Vector3(1 * rotationPolarity, 0, 0));

            if (transform.eulerAngles.x >= 10 && transform.eulerAngles.x <= 180)
            {
                rotationPolarity = -1;
            } else if (transform.eulerAngles.x <= 330 && transform.eulerAngles.x >= 180)
            {
                rotationPolarity = 1;
            }
        } else if (!step3Done)
        {
            shootForce += 1 * shootPolarity;

            if (shootForce >= 60)
            {
                shootForce = 60;
                shootPolarity = -1;
            } else if (shootForce <= 10)
            {
                shootForce = 10;
                shootPolarity = 1;
            }

            currentPowerFill = (shootForce-10)/50;
            UpdatePowerBar();
        } else if (!shot)
        {
            pumpkin.Shoot(shootForce);
            shot = true;
        }
        
    }

    public void OnSpacebarInput (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!step1Done)
            {
                step1Done = true;
            } else if (!step2Done)
            {
                step2Done = true;
                powerBarBackground.enabled = true;
            } else if (!step3Done)
            {
                step3Done = true;
                powerBar.enabled = false;
                powerBarBackground.enabled = false;
            }
        }
        
    }

    private void UpdatePowerBar()
    {
        powerBar.fillAmount = currentPowerFill;
    }
}
