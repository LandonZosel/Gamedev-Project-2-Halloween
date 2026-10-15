using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]

public class PumpkinController : MonoBehaviour, ISaveSystem
{
    private float moveSpeed = 1;
    private float maxXSpeed = 15;
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
    private bool spaceInput;
    private bool leftInput;
    private SkillKey leftSkill;
    [SerializeField]
    private Image leftSkillReference;
    [SerializeField]
    private Image leftFillReference;
    private bool upInput;
    private SkillKey upSkill;
    [SerializeField]
    private Image upSkillReference;
    [SerializeField]
    private Image upFillReference;
    private bool rightInput;
    private SkillKey rightSkill;
    [SerializeField]
    private Image rightSkillReference;
    [SerializeField]
    private Image rightFillReference;
    private bool downInput;
    private SkillKey downSkill;
    [SerializeField]
    private Image downFillReference;
    [SerializeField]
    private Image downSkillReference;
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
    private float gravityFlipCooldown = 0f;
    [SerializeField]
    private float hoverCooldown = 0f;
    private float hoverTime = 0f;
    [SerializeField]
    private float floatCooldown = 0f;
    private float floatTime = 0f;
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
    private float trampolineSpawnInterval = 10f;
    private float lastTrampolineSpawnZ;
    private int trampolineForce = 20;
    [SerializeField]
    private GameObject ghost1;
    [SerializeField]
    private GameObject ghost2;
    [SerializeField]
    private GameObject ghost3;
    private float ghostSpawnInterval = 50f;
    private float lastGhostSpawnZ;
    [SerializeField]
    private GameObject moneyObj;
    private float moneySpawnInterval = 100f;
    private float lastMoneySpawnZ;
    private int money;
    private int dayCount;
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
        lastTrampolineSpawnZ = rig.position.z;
        for (int i = 0; i < 30; i++)
        {
            Vector3 spawnPosition = new Vector3(rig.position.x + Random.Range(-30f, 30f), rig.position.y + Random.Range(-10f, 40f), 10f + i*5);
            if (spawnPosition.y <= 0)
            {
                spawnPosition.y = 1;
            }
            
            // GameObject obj = 
            Instantiate(trampoline, spawnPosition, Quaternion.identity);
            // Destroy(obj, 20.0f);
        }
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

        if (rig.position.z > lastTrampolineSpawnZ + trampolineSpawnInterval)
        {
            Vector3 localSpawnPosition = new Vector3(rig.position.x + Random.Range(-30f, 30f), rig.position.y + Random.Range(-60f, 20f), rig.position.z + 150f);
            Vector3 farSpawnPosition = new Vector3(rig.position.x + Random.Range(-30f, 30f), rig.position.y + Random.Range(-100f, -30f), rig.position.z + 40f);
            if (localSpawnPosition.y <= 0)
            {
                localSpawnPosition.y = 1;
            }

            if (farSpawnPosition.y < 1)
            {
                farSpawnPosition.y = -99;
            }
            
            GameObject obj1 = Instantiate(trampoline, localSpawnPosition, Quaternion.identity);
            GameObject obj2 = Instantiate(trampoline, farSpawnPosition, Quaternion.identity);
            Destroy(obj1, 20.0f);
            Destroy(obj2, 20.0f);

            lastTrampolineSpawnZ = rig.position.z;
        }

        if (rig.position.z > lastGhostSpawnZ + ghostSpawnInterval)
        {
            Vector3 spawnPosition = new Vector3(rig.position.x + Random.Range(-40f, 40f), rig.position.y + Random.Range(-100f, 10f), rig.position.z + 150f);
            if (spawnPosition.y <= 0)
            {
                spawnPosition.y = 1;
            }

            int ghostNum = Random.Range(0, 100);
            GameObject obj;
            
            if (ghostNum > 94)
            {
                obj = Instantiate(ghost3, spawnPosition, Quaternion.Euler(0, 180, 0));
            } else if (ghostNum < 5)
            {
                obj = Instantiate(ghost2, spawnPosition, Quaternion.Euler(0, 180, 0));
            }
            else
            {
                obj = Instantiate(ghost1, spawnPosition, Quaternion.Euler(0, 180, 0));
            }
            
            Destroy(obj, 20.0f);

            lastGhostSpawnZ = rig.position.z;
        }

        if (rig.position.z > lastMoneySpawnZ + moneySpawnInterval)
        {
            Vector3 spawnPosition = new Vector3(rig.position.x + Random.Range(-40f, 40f), rig.position.y + Random.Range(-100f, 10f), rig.position.z + 150f);
            if (spawnPosition.y <= 0)
            {
                spawnPosition.y = 1;
            }
            
            GameObject obj = Instantiate(moneyObj, spawnPosition, Quaternion.Euler(-180, 0, -90));
            Destroy(obj, 20.0f);

            lastMoneySpawnZ = rig.position.z;
        }

        IsGrounded();
    }

    public void OnSpaceInput (InputAction.CallbackContext context)
    {
        // spaceInput = context.ReadValueAsButton();
        if (grounded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnMoveInput (InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnExitInput ()
    {
        Application.Quit();
    }

    public void OnLeftInput (InputAction.CallbackContext context)
    {
        if (leftSkill != null)
        {
            leftInput = context.ReadValueAsButton();
        }
    }

    public void OnUpInput (InputAction.CallbackContext context)
    {
        if (upSkill != null)
        {
            upInput = context.ReadValueAsButton();
        }
    }

    public void OnRightInput (InputAction.CallbackContext context)
    {
        if (rightSkill != null)
        {
            rightInput = context.ReadValueAsButton();
        }
    }

    public void OnDownInput (InputAction.CallbackContext context)
    {
        if (downSkill != null)
        {
            downInput = context.ReadValueAsButton();
        }
    }

    public void Shoot (float shootForce)
    {
        hasShot = true;
        startTime = Time.timeAsDouble;
        rig.AddForce(transform.up * shootForce, ForceMode.VelocityChange);
        rig.useGravity = true;
    }

    public void Trampoline()
    {
        //bounce
        rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
        rig.AddForce(new Vector3(0, 2, 1) * trampolineForce, ForceMode.VelocityChange);
        bottomText.AddTrampolineText();
    }

    public void Ghost()
    {
        //ow slow down
        rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
        rig.AddForce(new Vector3(0, 1, 1) * -60, ForceMode.VelocityChange);
        bottomText.AddText("Hit Ghost :(");
    }

    public void Money()
    {
        //Gain 100$
        money += 100;
        bottomText.AddText("+$100");
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
                bottomText.AddText("Landed on ground :(\nPress Space to restart");
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
            case Skill.GravityFlip:
                bottomText.AddText("Activated Gravity Flip!");
                SkillGravityFlip(skillKey);
                break;
            case Skill.Hover:
                bottomText.AddText("Activated Hover!");
                SkillHover(skillKey);
                break;
            case Skill.Float:
                bottomText.AddText("Activated Float!");
                SkillFloat(skillKey);
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

    private void SkillGravityFlip(SkillKey skillKey)
    {
        // implement gravity flip
        // gravityFlipCooldown = Time.time + 20f;
        skillKey.endCooldown = Time.time + gravityFlipCooldown;
    }

    private void SkillHover(SkillKey skillKey)
    {
        hoverTime = Time.time + 5f;
        // hoverCooldown = Time.time + 10f;
        skillKey.endCooldown = Time.time + hoverCooldown;
    }

    private void SkillFloat(SkillKey skillKey)
    {
        // implement float
        floatTime = Time.time + 20f;
        skillKey.endCooldown = Time.time + floatCooldown;
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

    public void LoadData(GameData data)
    {
        Debug.Log("Loading data from pumpkin controller");
        dayCount = data.dayCount;
        money = data.money;
        foreach (SkillButton skill in data.skills)
        {
            switch (skill.keyAssigned)
            {
                case Key.Left:
                    leftSkill = new SkillKey(
                        Key.Left,
                        skill.skill,
                        skill.imageFileName,
                        leftFillReference
                    );
                    leftSkillReference.sprite = Resources.Load<Sprite>("Sprites/" + skill.imageFileName);
                    break;
                case Key.Up:
                    upSkill = new SkillKey(
                        Key.Up,
                        skill.skill,
                        skill.imageFileName,
                        upFillReference
                    );
                    upSkillReference.sprite = Resources.Load<Sprite>("Sprites/" + skill.imageFileName);
                    break;
                case Key.Right:
                    rightSkill = new SkillKey(
                        Key.Right,
                        skill.skill,
                        skill.imageFileName,
                        rightFillReference
                    );
                    rightSkillReference.sprite = Resources.Load<Sprite>("Sprites/" + skill.imageFileName);
                    break;
                case Key.Down:
                    downSkill = new SkillKey(
                        Key.Down,
                        skill.skill,
                        skill.imageFileName,
                        downFillReference
                    );
                    downSkillReference.sprite = Resources.Load<Sprite>("Sprites/" + skill.imageFileName);
                    break;
            }
        }

        foreach (UpgradeButton upgrade in data.upgrades)
        {
            //check if upgrade purchased
            if (upgrade.purchased)
            {
                //massive switch statement to employ upgrade effect
                switch (upgrade.upgrade)
                {
                    case Upgrade.T1:
                        trampolineForce *= 2;
                        break;
                    case Upgrade.T2:
                        trampolineSpawnInterval -= 5;
                        break;
                    case Upgrade.T3:
                        trampolineForce *= 2;
                        break;
                    case Upgrade.S1:
                        maxXSpeed += 15;
                        break;
                    case Upgrade.S2:
                        moveSpeed += 1;
                        break;
                    case Upgrade.S3:
                        forwardAirResistance = 0.97f;
                        break;
                    case Upgrade.G1:
                        
                        break;
                    case Upgrade.Jump1:
                        
                        break;
                    case Upgrade.Jump2:
                        
                        break;
                    case Upgrade.Jump3:
                        
                        break;
                    case Upgrade.Explode1:
                        
                        break;
                    case Upgrade.Explode2:
                        
                        break;
                    case Upgrade.Explode3:
                        
                        break;
                    case Upgrade.Hover1:
                        
                        break;
                    case Upgrade.Hover2:
                        
                        break;
                    case Upgrade.Hover3:
                        
                        break;
                    case Upgrade.Magnet1:
                        
                        break;
                    case Upgrade.Magnet2:
                        
                        break;
                    case Upgrade.Magnet3:
                        
                        break;
                    case Upgrade.Float1:
                        
                        break;
                    case Upgrade.Float2:
                        
                        break;
                    case Upgrade.Float3:
                        
                        break;
                    case Upgrade.Fire1:
                        
                        break;
                    case Upgrade.Fire2:
                        
                        break;
                    case Upgrade.Fire3:
                        
                        break;
                    case Upgrade.Arrow1:
                        
                        break;
                    case Upgrade.Arrow2:
                        
                        break;
                    case Upgrade.Arrow3:
                        
                        break;
                    case Upgrade.GravityFlip1:
                        
                        break;
                    case Upgrade.GravityFlip2:
                        
                        break;
                    case Upgrade.GravityFlip3:
                        
                        break;
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving data from pumpkin controller");
        data.dayCount = this.dayCount;
        data.money = this.money;
    }
}
