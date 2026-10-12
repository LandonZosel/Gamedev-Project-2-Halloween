using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]

public class PumpkinController : MonoBehaviour
{
    public enum Skill
    { 
        Jump,
        Arrow,
        Magnet,
        Split,
        Hover,
        Speed,
        Fire,
        Explode
    }

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxXSpeed;
    [SerializeField]
    private float forwardAirResistance = 0.99f;
    [SerializeField]
    private float maxFallSpeed = 100f;
    [SerializeField]
    private TMP_Text heightText;
    [SerializeField]
    private TMP_Text distanceText;
    [SerializeField]
    private TMP_Text timeText;
    private double startTime;
    private Vector2 moveInput;
    private bool shootInput;
    private bool leftInput;
    private Skill? leftSkill = Skill.Hover;
    private bool upInput;
    private Skill? upSkill = Skill.Jump;
    private bool rightInput;
    private Skill? rightSkill = null;
    private bool downInput;
    private Skill? downSkill = Skill.Explode;
    private float jumpCooldown = 0f;
    private float arrowCooldown = 0f;
    private float arrowTime = 0f;
    private float magnetCooldown = 0f;
    private float magnetTime = 0f;
    private float splitCooldown = 0f;
    private float hoverCooldown = 0f;
    private float hoverTime = 0f;
    private float speedCooldown = 0f;
    private float speedTime = 0f;
    private float fireCooldown = 0f;
    private float fireTime = 0f;
    private float explodeCooldown = 0f;
    private bool hasShot = false;
    private bool grounded = false;
    private Rigidbody rig;


    [SerializeField]
    private GameObject trampoline;
    private float nextSpawnThreshold = 10f;
    private float spawnInterval = 10f;
    private float lastSpawnZ;
    private int trampolineNumber = 0;
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heightText.text = "Height\n0";
        distanceText.text = "Distance\n0";
        timeText.text = "Time\n0";
        lastSpawnZ = rig.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate ()
    {
        if (grounded || !hasShot)
        {
            rig.linearVelocity = Vector3.zero;
            return;
        }

        heightText.text = "Height\n" + ((int)transform.position.y);
        distanceText.text = "Distance\n" + ((int)transform.position.z);
        timeText.text = "Time\n" + ((int)(Time.timeAsDouble - startTime));

        Vector3 velocity = rig.linearVelocity;

        // velocity.z *= forwardAirResistance;
        
        
        velocity.x += moveInput.x * moveSpeed;
        velocity.x = Mathf.Clamp(velocity.x, -maxXSpeed, maxXSpeed);

        if (velocity.y < -maxFallSpeed)
        {
            velocity.y = -maxFallSpeed;
        }

        if (Time.time <= hoverTime && velocity.y <= 0)
        {
            velocity.y = 0;
        }

        transform.Rotate(velocity);

        rig.linearVelocity = velocity;

        rig.AddForce(Vector3.back * forwardAirResistance, ForceMode.Force);

        if (leftInput)
        {
            CallSkill(leftSkill);
        }

        if (upInput)
        {
            CallSkill(upSkill);
        }

        if (rightInput)
        {
            CallSkill(rightSkill);
        }

        if (downInput)
        {
            CallSkill(downSkill);
        }

        if (rig.position.z > lastSpawnZ + spawnInterval)
        {
            Vector3 spawnPosition = new Vector3(rig.position.x + Random.Range(-40f, 40f), rig.position.y + Random.Range(-50f, 10f), rig.position.z + 100f);
            if (spawnPosition.y <= 0)
            {
                spawnPosition.y = 1;
            }
            
            trampolineNumber++;
            // Debug.Log("Spawned trampoline #" + trampolineNumber);
            Instantiate(trampoline, spawnPosition, Quaternion.identity);

            lastSpawnZ = rig.position.z;
        }

        IsGrounded();
    }

    public void OnShootInput (InputAction.CallbackContext context)
    {
        // shootInput = context.ReadValueAsButton();
    }

    public void OnMoveInput (InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLeftInput (InputAction.CallbackContext context)
    {
        leftInput = context.ReadValueAsButton();
    }

    public void OnUpInput (InputAction.CallbackContext context)
    {
        upInput = context.ReadValueAsButton();
    }

    public void OnRightInput (InputAction.CallbackContext context)
    {
        rightInput = context.ReadValueAsButton();
    }

    public void OnDownInput (InputAction.CallbackContext context)
    {
        downInput = context.ReadValueAsButton();
    }

    public void Shoot (float shootForce)
    {
        shootInput = false;
        hasShot = true;
        startTime = Time.timeAsDouble;
        rig.AddForce(transform.up * shootForce, ForceMode.VelocityChange);
        rig.useGravity = true;
    }

    void IsGrounded ()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 0.6f))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Hit!: " + hitObject.name);
            if (hitObject.CompareTag("Trampoline"))
            {
                //bounce
                rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
                rig.AddForce(new Vector3(0, 1, 1) * 60, ForceMode.VelocityChange);
            } else
            {
                //hit ground :(
                grounded = true;
            }
        }
    }

    private void CallSkill(Skill? skill)
    {
        switch (skill)
        {
            case Skill.Jump:
                if (Time.time >= jumpCooldown)
                {
                   SkillJump(); 
                }
                break;
            case Skill.Arrow:
                if (Time.time >= arrowCooldown)
                {
                   SkillArrow(); 
                }
                break;
            case Skill.Magnet:
                if (Time.time >= magnetCooldown)
                {
                   SkillMagnet(); 
                }
                break;
            case Skill.Split:
                if (Time.time >= splitCooldown)
                {
                   SkillSplit(); 
                }
                break;
            case Skill.Hover:
                if (Time.time >= hoverCooldown)
                {
                   SkillHover();
                }
                break;
            case Skill.Speed:
                if (Time.time >= speedCooldown)
                {
                   SkillSpeed(); 
                }
                break;
            case Skill.Fire:
                if (Time.time >= fireCooldown)
                {
                   SkillFire(); 
                }
                break;
            case Skill.Explode:
                if (Time.time >= explodeCooldown)
                {
                   SkillExplode(); 
                }
                break;
        }
    }

    private void SkillJump()
    {
        rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
        rig.AddForce(new Vector3(0, 2, 1) * 30, ForceMode.VelocityChange);
        jumpCooldown = Time.time + 2.5f;
    }

    private void SkillArrow()
    {
        // implement arrow
        arrowTime = Time.time + 20f;
    }

    private void SkillMagnet()
    {
        // implement magnet
        magnetTime = Time.time + 20f;
    }

    private void SkillSplit()
    {
        // implement split
        splitCooldown = Time.time + 20f;
    }

    private void SkillHover()
    {
        hoverTime = Time.time + 5f;
        hoverCooldown = Time.time + 10f;
    }

    private void SkillSpeed()
    {
        // implement speed
        speedTime = Time.time + 20f;
    }

    private void SkillFire()
    {
        // implement fire
        fireTime = Time.time + 20f;
    }

    private void SkillExplode()
    {
        rig.AddForce(new Vector3(0, 1, 0) * 200, ForceMode.VelocityChange);
        explodeCooldown = Time.time + 20f;
    }
}
