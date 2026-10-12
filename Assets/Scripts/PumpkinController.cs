using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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
    [SerializeField]
    private BottomTextController bottomText;
    private double startTime;
    private Vector2 moveInput;
    private bool shootInput;
    private bool leftInput;
    private Skill? leftSkill = Skill.Hover;
    private bool upInput;
    private Skill? upSkill = Skill.Jump;
    private bool rightInput;
    private Skill? rightSkill = Skill.Arrow;
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
    private LineRenderer lineRenderer;
    [SerializeField]
    private int resolution = 30;
    [SerializeField]
    private float timeStep = 0.1f;


    [SerializeField]
    private GameObject trampoline;
    private float nextSpawnThreshold = 10f;
    private float spawnInterval = 10f;
    private float lastSpawnZ;
    private int trampolineNumber = 0;
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
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
        if (Time.time <= arrowTime)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = resolution;
            Vector3[] points = new Vector3[resolution];

            for (int i = 0; i < resolution; i++)
            {
                float time = i * timeStep;
                
                Vector3 movement = rig.linearVelocity * time + 0.5f * Physics.gravity * time * time;
                points[i] = rig.position + movement;
            }

            lineRenderer.SetPositions(points);
        } else
        {
            lineRenderer.enabled = false;
        }
        
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
            GameObject obj = Instantiate(trampoline, spawnPosition, Quaternion.identity);
            Destroy(obj, 20.0f);

            lastSpawnZ = rig.position.z;
        }

        IsGrounded();
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
                bottomText.AddText("Bounced on trampoline!");
            } else
            {
                //hit ground :(
                grounded = true;
                bottomText.AddText("Landed on ground :(");
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
                    bottomText.AddText("Activated Jump!");
                    SkillJump(); 
                }
                break;
            case Skill.Arrow:
                if (Time.time >= arrowCooldown)
                {
                    bottomText.AddText("Activated Arrow!");
                    SkillArrow(); 
                }
                break;
            case Skill.Magnet:
                if (Time.time >= magnetCooldown)
                {
                    bottomText.AddText("Activated Magnet!");
                    SkillMagnet(); 
                }
                break;
            case Skill.Split:
                if (Time.time >= splitCooldown)
                {
                    bottomText.AddText("Activated Split!");
                    SkillSplit(); 
                }
                break;
            case Skill.Hover:
                if (Time.time >= hoverCooldown)
                {
                    bottomText.AddText("Activated Hover!");
                    SkillHover();
                }
                break;
            case Skill.Speed:
                if (Time.time >= speedCooldown)
                {
                    bottomText.AddText("Activated Speed!");
                    SkillSpeed(); 
                }
                break;
            case Skill.Fire:
                if (Time.time >= fireCooldown)
                {
                    bottomText.AddText("Activated Fire!");
                    SkillFire(); 
                }
                break;
            case Skill.Explode:
                if (Time.time >= explodeCooldown)
                {
                    bottomText.AddText("Activated Explode!");
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
        arrowCooldown = Time.time + 30f;
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
