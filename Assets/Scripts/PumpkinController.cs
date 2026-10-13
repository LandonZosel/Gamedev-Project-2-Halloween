using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

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

public enum Key
{ 
    Left,
    Up,
    Right,
    Down
}

public class SkillKey
{
    public Key key;
    public Skill skill;
    public float startCooldown;
    public float endCooldown;
    public Image fillReference;

    public SkillKey(Key key, Skill skill, Image fillReference)
    {
        this.key = key;
        this.skill = skill;
        startCooldown = 0;
        endCooldown = 0;
        this.fillReference = fillReference;
    }
}

[RequireComponent(typeof(Rigidbody))]

public class PumpkinController : MonoBehaviour
{
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
    private SkillKey leftSkill;
    [SerializeField]
    private Image leftFillReference;
    private bool upInput;
    private SkillKey upSkill;
    [SerializeField]
    private Image upFillReference;
    private bool rightInput;
    private SkillKey rightSkill;
    [SerializeField]
    private Image rightFillReference;
    private bool downInput;
    private SkillKey downSkill;
    [SerializeField]
    private Image downFillReference;
    private List<SkillKey> skillKeyList = new List<SkillKey>();
    [SerializeField]
    private float jumpCooldown = 2.5f;
    [SerializeField]
    private float arrowCooldown = 0f;
    private float arrowTime = 0f;
    [SerializeField]
    private float magnetCooldown = 0f;
    private float magnetTime = 0f;
    [SerializeField]
    private float splitCooldown = 0f;
    [SerializeField]
    private float hoverCooldown = 0f;
    private float hoverTime = 0f;
    [SerializeField]
    private float speedCooldown = 0f;
    private float speedTime = 0f;
    [SerializeField]
    private float fireCooldown = 0f;
    private float fireTime = 0f;
    [SerializeField]
    private float explodeCooldown = 30f;
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
        leftSkill = new SkillKey(
            Key.Left,
            Skill.Hover,
            leftFillReference
        );
        upSkill = new SkillKey(
            Key.Up,
            Skill.Jump,
            upFillReference
        );
        rightSkill = new SkillKey(
            Key.Right,
            Skill.Arrow,
            rightFillReference
        );
        downSkill = new SkillKey(
            Key.Down,
            Skill.Explode,
            downFillReference
        );
        skillKeyList.Add(leftSkill);
        skillKeyList.Add(upSkill);
        skillKeyList.Add(rightSkill);
        skillKeyList.Add(downSkill);
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

        foreach (SkillKey skillKey in skillKeyList)
        {
            float now = Time.time;
            if (now < skillKey.endCooldown)
            {
                float greyFill = Mathf.Clamp01((now - skillKey.startCooldown)/(skillKey.endCooldown - skillKey.startCooldown));
                greyFill = 1 - greyFill;
                skillKey.fillReference.fillAmount = greyFill;
            } else
            {
                skillKey.fillReference.fillAmount = 0;
            }
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

        if (leftInput && Time.time >= leftSkill.endCooldown)
        {
            CallSkill(leftSkill);
        }

        if (upInput && Time.time >= upSkill.endCooldown)
        {
            CallSkill(upSkill);
        }

        if (rightInput && Time.time >= rightSkill.endCooldown)
        {
            CallSkill(rightSkill);
        }

        if (downInput && Time.time >= downSkill.endCooldown)
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

    public void Trampoline()
    {
        //bounce
        rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
        rig.AddForce(new Vector3(0, 2, 1) * 60, ForceMode.VelocityChange);
        bottomText.AddText("Bounced on trampoline!");
    }

    void IsGrounded ()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 0.6f))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Plane"))
            {
                //hit ground :(
                grounded = true;
                bottomText.AddText("Landed on ground :(");
            }
        }
    }

    private void CallSkill(SkillKey skillKey)
    {
        switch (skillKey.skill)
        {
            case Skill.Jump:
                bottomText.AddText("Activated Jump!");
                SkillJump(skillKey);
                break;
            case Skill.Arrow:
                bottomText.AddText("Activated Arrow!");
                SkillArrow(skillKey);
                break;
            case Skill.Magnet:
                bottomText.AddText("Activated Magnet!");
                SkillMagnet(skillKey);
                break;
            case Skill.Split:
                bottomText.AddText("Activated Split!");
                SkillSplit(skillKey);
                break;
            case Skill.Hover:
                bottomText.AddText("Activated Hover!");
                SkillHover(skillKey);
                break;
            case Skill.Speed:
                bottomText.AddText("Activated Speed!");
                SkillSpeed(skillKey);
                break;
            case Skill.Fire:
                bottomText.AddText("Activated Fire!");
                SkillFire(skillKey);
                break;
            case Skill.Explode:
                bottomText.AddText("Activated Explode!");
                SkillExplode(skillKey);
                break;
        }
        skillKey.startCooldown = Time.time;
    }

    private void SkillJump(SkillKey skillKey)
    {
        rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
        rig.AddForce(new Vector3(0, 2, 1) * 30, ForceMode.VelocityChange);
        skillKey.endCooldown = Time.time + jumpCooldown;
    }

    private void SkillArrow(SkillKey skillKey)
    {
        // implement arrow
        arrowTime = Time.time + 20f;
        skillKey.endCooldown = Time.time + arrowCooldown;
        // arrowCooldown = Time.time + 30f;
    }

    private void SkillMagnet(SkillKey skillKey)
    {
        // implement magnet
        magnetTime = Time.time + 20f;
        skillKey.endCooldown = Time.time + magnetCooldown;
    }

    private void SkillSplit(SkillKey skillKey)
    {
        // implement split
        // splitCooldown = Time.time + 20f;
        skillKey.endCooldown = Time.time + splitCooldown;
    }

    private void SkillHover(SkillKey skillKey)
    {
        hoverTime = Time.time + 5f;
        // hoverCooldown = Time.time + 10f;
        skillKey.endCooldown = Time.time + hoverCooldown;
    }

    private void SkillSpeed(SkillKey skillKey)
    {
        // implement speed
        speedTime = Time.time + 20f;
        skillKey.endCooldown = Time.time + speedCooldown;
    }

    private void SkillFire(SkillKey skillKey)
    {
        // implement fire
        fireTime = Time.time + 20f;
        skillKey.endCooldown = Time.time + fireCooldown;
    }

    private void SkillExplode(SkillKey skillKey)
    {
        rig.AddForce(new Vector3(0, 1, 0) * 200, ForceMode.VelocityChange);
        // explodeCooldown = Time.time + 20f;
        skillKey.endCooldown = Time.time + explodeCooldown;
    }
}
