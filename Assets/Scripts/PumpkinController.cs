using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PumpkinController : MonoBehaviour, ISaveSystem
{
    private float moveSpeed = 1;
    private float maxXSpeed = 15;
    [SerializeField]
    private float forwardAirResistance = 1f;
    [SerializeField]
    private float maxFallSpeed = 200f;
    [SerializeField]
    private TMP_Text heightText;
    [SerializeField]
    private TMP_Text distanceText;
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private BottomTextController bottomText;
    [SerializeField]
    private TMP_Text scoreTitleText;
    [SerializeField]
    private TMP_Text scoreText;
    private double startTime;
    private Vector2 moveInput;
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
    private float jumpCooldown = 10f;
    private float jumpPower = 30f;
    private float arrowCooldown = 30f;
    private float arrowLength = 15f;
    private bool arrow2 = false;
    private float arrowTime = 0f;
    private float magnetCooldown = 30f;
    private float magnetLength = 10f;
    private float magnetTime = 0f;
    [SerializeField]
    private ParticleSystem magnetEffect;
    private int magnetAttractionSpeed = 20;
    private int magnetRadius = 20;
    private float gravityFlipCooldown = 60f;
    private float gravityFlipLength = 5f;
    private float gravityFlipTime = 0f;
    private float hoverCooldown = 30f;
    private float hoverLength = 5f;
    private float hoverTime = 0f;
    private float floatCooldown = 30f;
    private float floatLength = 10f;
    private float floatTime = 0f;
    private float floatFallSpeed = 10f;
    private float fireCooldown = 30f;
    private float fireLength = 15f;
    private bool fire2 = false;
    private float fireTime = 0f;
    [SerializeField]
    private ParticleSystem fireEffect;
    private float explodeCooldown = 45f;
    private int explodePower = 100;
    private bool hasShot = false;
    private bool grounded = false;
    private Rigidbody rig;
    private LineRenderer lineRenderer;
    [SerializeField]
    private MeshCollider lineMeshCollider;
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
    private float moneySpawnInterval = 50f;
    private float lastMoneySpawnZ;
    private Vector3 gravity = new Vector3(0, -40, 0);

    [SerializeField]
    private CanvasGroup scoreCanvas;
    [SerializeField]
    private CanvasGroup uiCanvas;
    private int maxDistance = 0;
    private int maxHeight = 0;
    private int maxTime = 0;
    private int benjaminsCollected = 0;
    private int benjaminValue = 100;
    private int maxTrampolineMultiplier = 1;
    private int maxGhostBurnedMultiplier = 0;
    private int money;
    private int dayCount;
    [SerializeField]
    private SaveSystem saveSystem;

    [SerializeField]
    private AudioSource mars;
    [SerializeField]
    private AudioSource jump;
    [SerializeField]
    private AudioSource hit;
    [SerializeField]
    private AudioSource moneyGained;
    [SerializeField]
    private AudioSource explode;
    [SerializeField]
    private AudioSource flame;

    private int winHeight = 10000;
    private int timeLimit = 120;
    private bool winning = false;
    private bool spawnedMoon = false;
    [SerializeField]
    private GameObject moon;
    [SerializeField]
    private CameraFollow camera;
    [SerializeField]
    private CanvasGroup winCanvas;
    private float waitTime = 0f;
    private bool finished = false;
    [SerializeField]
    private TMP_Text winSubtitleText;
    [SerializeField]
    private TMP_Text winSuperSubtitleText;
    
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
        timeText.text = "Time\n120";
        lastTrampolineSpawnZ = rig.position.z;
        mars.volume = 0.25f;
        for (int i = 0; i < 30; i++)
        {
            Vector3 spawnPosition = new Vector3(rig.position.x + Random.Range(-30f, 30f), rig.position.y + Random.Range(-10f, 40f), 20f + i*5);
            if (spawnPosition.y <= 0)
            {
                spawnPosition.y = 5;
            }
            
            Instantiate(trampoline, spawnPosition, Quaternion.identity);
        }
        fireEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        magnetEffect.gameObject.SetActive(false);
        scoreCanvas.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (winning)
        {
            mars.volume = 0.9f;
            return;
        }

        if (Time.time <= fireTime)
        {
            fireEffect.Play();
            
        } else
        {
            fireEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        if (!(Time.time <= magnetTime))
        {
            magnetEffect.gameObject.SetActive(false);
        }

        if (grounded || !hasShot)
        {
            return;
        }

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

            for (int i = 0; i < points.Length - 1; i++)
            {
                if (Physics.Linecast(points[i], points[i+1], out RaycastHit hit))
                {
                    if (arrow2 && hit.collider.CompareTag("Money"))
                    {
                        Money();
                        hit.collider.gameObject.SetActive(false);
                    }
                    lineRenderer.positionCount = i + 2;
                    lineRenderer.SetPosition(i + 1, hit.point);
                    break;
                }
            }
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

        if (winning)
        {
            if (rig.linearVelocity.y > 50)
            {
                rig.AddForce(Vector3.down, ForceMode.VelocityChange);
            } else if (!spawnedMoon)
            {
                SpawnMoon();
            } else if(Time.time > waitTime && !finished)
            {
                finished = true;
                //fade in win ui
                winCanvas.gameObject.SetActive(true);
                StartCoroutine(FadeInCanvas(winCanvas, 0f, 1f));
            }
            return;
        }

        int currentHeight = (int)transform.position.y;
        heightText.text = "Height\n" + currentHeight;
        if (currentHeight > maxHeight)
        {
            maxHeight = currentHeight;
        }
        int currentDistance = (int)transform.position.z;
        distanceText.text = "Distance\n" + currentDistance;
        if (currentDistance > maxDistance)
        {
            maxDistance = currentDistance;
        }
        int timeRemaining = timeLimit - (int)(Time.timeAsDouble - startTime);
        timeText.text = "Time\n" + timeRemaining;
        maxTime = timeLimit - timeRemaining;

        if (timeRemaining <= 0)
        {
            EndGame(false);
        }

        float marsVolume = Mathf.Clamp01(currentHeight/winHeight);
        if (marsVolume < 0.25f)
        {
            marsVolume = 0.25f;
        }

        if (marsVolume >= 0.9f)
        {
            marsVolume = 0.9f;
        }
        mars.volume = marsVolume;


        if (Time.time <= gravityFlipTime)
        {
            rig.AddForce(-gravity, ForceMode.Acceleration);
        } else
        {
            rig.AddForce(gravity, ForceMode.Acceleration);
        }

        Vector3 velocity = rig.linearVelocity;
        
        velocity.x += moveInput.x * moveSpeed;
        velocity.x = Mathf.Clamp(velocity.x, -maxXSpeed, maxXSpeed);

        if (Time.time <= floatTime)
        {
            if (velocity.y < -floatFallSpeed)
            {
                velocity.y = -floatFallSpeed;
            }
        } else
        {
            if (velocity.y < -maxFallSpeed)
            {
                velocity.y = -maxFallSpeed;
            }
        }
        

        if (Time.time <= hoverTime && velocity.y <= 0)
        {
            velocity.y = 0;
        }

        transform.Rotate(velocity);

        rig.linearVelocity = velocity;

        if (velocity.z > 0)
        {
            rig.AddForce(Vector3.back * forwardAirResistance, ForceMode.Force);
        }
        

        if (Time.time <= magnetTime)
        {
            Collider[] itemsNear = Physics.OverlapSphere(transform.position, magnetRadius);

            foreach(Collider item in itemsNear)
            {
                if (item.CompareTag("Ghost") || item.CompareTag("Money") || item.CompareTag("Trampoline"))
                {
                    item.transform.position = Vector3.MoveTowards(
                        item.transform.position, 
                        transform.position,
                        magnetAttractionSpeed * Time.deltaTime
                    );
                }
            }
        }

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
            Vector3 farSpawnPosition = new Vector3(rig.position.x + Random.Range(-30f, 30f), rig.position.y + Random.Range(-100f, -40f), rig.position.z + 30f);
            if (localSpawnPosition.y <= 0)
            {
                localSpawnPosition.y = 5;
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
        IsWon();
    }

    public void OnMoveInput (InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void SaveAndQuitButton()
    {
        dayCount++;
        CreateScoreText();
        saveSystem.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void OnContinueInput()
    {
        dayCount++;
        saveSystem.SaveGame();
        SceneManager.LoadScene("Shop");
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
    }

    public void Trampoline()
    {
        //bounce
        rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
        rig.AddForce(new Vector3(0, 3, 1) * trampolineForce, ForceMode.VelocityChange);
        jump.Play();
        var currentMult = bottomText.AddTrampolineText();
        if (currentMult > maxTrampolineMultiplier)
        {
            maxTrampolineMultiplier = currentMult;
        }
    }

    public void Ghost(GameObject ghost)
    {
        if (Time.time <= fireTime)
        {
            ghost.SetActive(false);
            var currentMult = bottomText.AddGhostBurnText(fire2);
            rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
            rig.AddForce(new Vector3(0, 2, 1) * 30f, ForceMode.VelocityChange);
            flame.Play();
            if (currentMult > maxGhostBurnedMultiplier)
            {
                maxGhostBurnedMultiplier = currentMult;
            }
        } else
        {
           //ow slow down
            rig.linearVelocity = new Vector3(rig.linearVelocity.x, 0, rig.linearVelocity.z);
            rig.AddForce(new Vector3(0, 1, 1) * -60, ForceMode.VelocityChange);
            hit.Play();
            bottomText.AddText("Hit Ghost... (._.)");
        }
        
    }

    public void Money()
    {
        //Gain $100/$1000
        benjaminsCollected++;
        moneyGained.Play();
        bottomText.AddText("+$" + benjaminValue);
    }

    void IsGrounded ()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1f))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Plane"))
            {
                EndGame(true);
            }
        }
    }
    void IsWon ()
    {
        if ((int)transform.position.y >= winHeight)
        {
            //fade out UI
            StartCoroutine(FadeOutUI(1f, 0f));
            gravity.y = 0;
            if (rig.linearVelocity.y < 50)
            {
                rig.linearVelocity = new Vector3(0, 50, 0);
            }
            else
            {
                rig.linearVelocity = new Vector3(0, rig.linearVelocity.y, 0);
            }
            winSubtitleText.text = "You beat the game in " + dayCount + " nights!";
            if (dayCount > 20)
            {
                winSuperSubtitleText.text = "You made it in the end.";
            } else if (dayCount > 15)
            {
                winSuperSubtitleText.text = "Good job!";
            } else if (dayCount > 10)
            {
                winSuperSubtitleText.text = "Wow! Impressive!";
            } else if (dayCount > 1)
            {
                winSuperSubtitleText.text = "Unbelievable!";
            } else
            {
                winSuperSubtitleText.text = "... did you cheat";
            }
            winning = true;

        }
    }

    void SpawnMoon()
    {
        //spawn moon above and in front of the player
        Vector3 spawnPosition = new Vector3(rig.position.x, rig.position.y + 500f, rig.position.z + 150f);
        Instantiate(moon, spawnPosition, Quaternion.identity);
        camera.StartWinning();
        spawnedMoon = true;

        waitTime = Time.time + 10f;
    }

    void EndGame (bool hitGround)
    {
        grounded = true;

        if (hitGround)
        {
            //hit ground :(
            bottomText.AddText("Landed on ground :(");
        } else
        {
            bottomText.AddText("Ran out of time!");
        }

        StartCoroutine(FadeOutMusic(mars, 1f, 0));
        
        //set score text based on variables
        scoreTitleText.text = "Night " + dayCount + " Results";
        scoreText.text = CreateScoreText();
        saveSystem.SaveGame();
        //fade in score
        scoreCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeInCanvas(scoreCanvas, 0f, 1f));
    }

    private string CreateScoreText()
    {
        string distanceScore = "Distance: " + maxDistance + "ft -> $" + maxDistance/10 + "\n";
        string heightScore = "Height: " + maxHeight + "ft -> $" + maxHeight/5 + "\n";
        string timeScore = "Time: " + maxTime + "s -> $" + maxTime + "\n";
        string benjaminsScore = "Benjamins Collected: " + benjaminsCollected + " -> $" + benjaminsCollected*benjaminValue + "\n";
        string trampolineMultiplier = "Trampoline Multiplier: X" + maxTrampolineMultiplier + "\n";
        string ghostBurnedMultiplier = "Ghost Burned Multiplier: X" + (maxGhostBurnedMultiplier+1) + "\n";
        int moneyEarned = ((maxDistance/10) + (maxHeight/5) + maxTime + (benjaminsCollected*benjaminValue)) * maxTrampolineMultiplier * (maxGhostBurnedMultiplier+1);
        money += moneyEarned;
        return distanceScore + heightScore + timeScore + benjaminsScore + trampolineMultiplier + ghostBurnedMultiplier + "Money Earned\n" + moneyEarned + "\nTotal Money\n" + money;
    }

    private IEnumerator FadeInCanvas(CanvasGroup canvas, float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;
        canvas.alpha = startAlpha;

        if (targetAlpha > 0) canvas.blocksRaycasts = true;
        else canvas.blocksRaycasts = false;

        while (elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 2);
            yield return null;
        }

        canvas.alpha = targetAlpha;
    }

    private IEnumerator FadeOutUI(float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;
        uiCanvas.alpha = startAlpha;

        if (targetAlpha > 0) uiCanvas.blocksRaycasts = true;
        else uiCanvas.blocksRaycasts = false;

        while (elapsedTime < 5)
        {
            elapsedTime += Time.deltaTime;
            uiCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 5);
            yield return null;
        }

        uiCanvas.alpha = targetAlpha;
    }

    public IEnumerator FadeOutMusic(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float startVolume = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        if (targetVolume <= 0)
        {
            audioSource.Stop();
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
        rig.AddForce(new Vector3(0, 2, 1) * jumpPower, ForceMode.VelocityChange);
        jump.Play();
        skillKey.endCooldown = Time.time + jumpCooldown;
    }

    private void SkillArrow(SkillKey skillKey)
    {
        arrowTime = Time.time + arrowLength;
        skillKey.endCooldown = Time.time + arrowCooldown;
    }

    private void SkillMagnet(SkillKey skillKey)
    {
        magnetTime = Time.time + magnetLength;
        skillKey.endCooldown = Time.time + magnetCooldown;
        magnetEffect.gameObject.SetActive(true);
        magnetEffect.Play();
    }

    private void SkillGravityFlip(SkillKey skillKey)
    {
        gravityFlipTime = Time.time + gravityFlipLength;
        skillKey.endCooldown = Time.time + gravityFlipCooldown;
    }

    private void SkillHover(SkillKey skillKey)
    {
        hoverTime = Time.time + hoverLength;
        skillKey.endCooldown = Time.time + hoverCooldown;
    }

    private void SkillFloat(SkillKey skillKey)
    {
        floatTime = Time.time + floatLength;
        skillKey.endCooldown = Time.time + floatCooldown;
    }

    private void SkillFire(SkillKey skillKey)
    {
        fireTime = Time.time + 20f;
        skillKey.endCooldown = Time.time + fireCooldown;
    }

    private void SkillExplode(SkillKey skillKey)
    {
        rig.AddForce(new Vector3(0, 1, 0.1f) * explodePower, ForceMode.VelocityChange);
        explode.Play();
        skillKey.endCooldown = Time.time + explodeCooldown;
    }

    public void LoadData(GameData data)
    {
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
                    skillKeyList.Add(leftSkill);
                    break;
                case Key.Up:
                    upSkill = new SkillKey(
                        Key.Up,
                        skill.skill,
                        skill.imageFileName,
                        upFillReference
                    );
                    upSkillReference.sprite = Resources.Load<Sprite>("Sprites/" + skill.imageFileName);
                    skillKeyList.Add(upSkill);
                    break;
                case Key.Right:
                    rightSkill = new SkillKey(
                        Key.Right,
                        skill.skill,
                        skill.imageFileName,
                        rightFillReference
                    );
                    rightSkillReference.sprite = Resources.Load<Sprite>("Sprites/" + skill.imageFileName);
                    skillKeyList.Add(rightSkill);
                    break;
                case Key.Down:
                    downSkill = new SkillKey(
                        Key.Down,
                        skill.skill,
                        skill.imageFileName,
                        downFillReference
                    );
                    downSkillReference.sprite = Resources.Load<Sprite>("Sprites/" + skill.imageFileName);
                    skillKeyList.Add(downSkill);
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
                        forwardAirResistance = 0.75f;
                        break;
                    case Upgrade.G1:
                        gravity *= 0.5f;
                        break;
                    case Upgrade.Jump1:
                        jumpCooldown -= 1f;
                        break;
                    case Upgrade.Jump2:
                        jumpPower *= 1.5f;
                        break;
                    case Upgrade.Jump3:
                        jumpCooldown -= 2f;
                        break;
                    case Upgrade.Explode1:
                        explodePower *= 2;
                        break;
                    case Upgrade.Explode2:
                        explodeCooldown -= 15;
                        break;
                    case Upgrade.Explode3:
                        explodePower *= 2;
                        break;
                    case Upgrade.Hover1:
                        hoverLength += 2.5f;
                        break;
                    case Upgrade.Hover2:
                        hoverCooldown -= 10f;
                        break;
                    case Upgrade.Hover3:
                        hoverLength += 2.5f;
                        break;
                    case Upgrade.Magnet1:
                        magnetCooldown -= 5f;
                        break;
                    case Upgrade.Magnet2:
                        magnetAttractionSpeed *= 2;
                        break;
                    case Upgrade.Magnet3:
                        magnetLength += 5f;
                        break;
                    case Upgrade.Float1:
                        floatCooldown -= 5f;
                        break;
                    case Upgrade.Float2:
                        floatLength += 5f;
                        break;
                    case Upgrade.Float3:
                        floatCooldown -= 5f;
                        break;
                    case Upgrade.Fire1:
                        fireCooldown -= 5f;
                        break;
                    case Upgrade.Fire2:
                        fire2 = true;
                        break;
                    case Upgrade.Fire3:
                        fireLength += 5f;
                        break;
                    case Upgrade.Arrow1:
                        arrowCooldown -= 5f;
                        break;
                    case Upgrade.Arrow2:
                        arrow2 = true;
                        benjaminValue = 1000;
                        break;
                    case Upgrade.Arrow3:
                        arrowLength += 5f;
                        break;
                    case Upgrade.GravityFlip1:
                        gravityFlipCooldown -= 10f;
                        break;
                    case Upgrade.GravityFlip2:
                        gravityFlipLength += 2.5f;
                        break;
                    case Upgrade.GravityFlip3:
                        gravityFlipCooldown -= 10f;
                        break;
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data.dayCount = this.dayCount;
        data.money = this.money;
    }
}
