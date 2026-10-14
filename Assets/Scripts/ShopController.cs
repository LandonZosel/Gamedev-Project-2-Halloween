using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour, ISaveSystem
{
    [SerializeField] 
    private List<Button> upgradeButtons;
    [SerializeField] 
    private List<Button> skillButtons;
    [SerializeField]
    private SaveSystem saveSystem;
    private List<SkillButton> skills = new List<SkillButton>();
    private List<UpgradeButton> upgrades = new List<UpgradeButton>();
    private int dayCount;
    private int money;
    
    void Start()
    {
        skillButtons[(int)Skill.Jump].onClick.AddListener(() => SkillButtonPressed(Skill.Jump));
        skillButtons[(int)Skill.Explode].onClick.AddListener(() => SkillButtonPressed(Skill.Explode));
        skillButtons[(int)Skill.Hover].onClick.AddListener(() => SkillButtonPressed(Skill.Hover));
        skillButtons[(int)Skill.Magnet].onClick.AddListener(() => SkillButtonPressed(Skill.Magnet));
        skillButtons[(int)Skill.Speed].onClick.AddListener(() => SkillButtonPressed(Skill.Speed));
        skillButtons[(int)Skill.Fire].onClick.AddListener(() => SkillButtonPressed(Skill.Fire));
        skillButtons[(int)Skill.Arrow].onClick.AddListener(() => SkillButtonPressed(Skill.Arrow));
        skillButtons[(int)Skill.GravityFlip].onClick.AddListener(() => SkillButtonPressed(Skill.GravityFlip));
        upgradeButtons[(int)Upgrade.C1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C1));
        upgradeButtons[(int)Upgrade.C2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C2));
        upgradeButtons[(int)Upgrade.C3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C3));
        upgradeButtons[(int)Upgrade.C4].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C4));
        upgradeButtons[(int)Upgrade.C5].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C5));
        upgradeButtons[(int)Upgrade.T1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.T1));
        upgradeButtons[(int)Upgrade.T2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.T2));
        upgradeButtons[(int)Upgrade.T3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.T3));
        upgradeButtons[(int)Upgrade.S1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.S1));
        upgradeButtons[(int)Upgrade.S2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.S2));
        upgradeButtons[(int)Upgrade.S3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.S3));
        upgradeButtons[(int)Upgrade.G1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.G1));
        upgradeButtons[(int)Upgrade.Jump1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Jump1));
        upgradeButtons[(int)Upgrade.Jump2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Jump2));
        upgradeButtons[(int)Upgrade.Jump3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Jump3));
        upgradeButtons[(int)Upgrade.Explode1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Explode1));
        upgradeButtons[(int)Upgrade.Explode2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Explode2));
        upgradeButtons[(int)Upgrade.Explode3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Explode3));
        upgradeButtons[(int)Upgrade.Hover1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Hover1));
        upgradeButtons[(int)Upgrade.Hover2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Hover2));
        upgradeButtons[(int)Upgrade.Hover3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Hover3));
        upgradeButtons[(int)Upgrade.Magnet1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Magnet1));
        upgradeButtons[(int)Upgrade.Magnet2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Magnet2));
        upgradeButtons[(int)Upgrade.Magnet3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Magnet3));
        upgradeButtons[(int)Upgrade.Speed1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Speed1));
        upgradeButtons[(int)Upgrade.Speed2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Speed2));
        upgradeButtons[(int)Upgrade.Speed3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Speed3));
        upgradeButtons[(int)Upgrade.Fire1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Fire1));
        upgradeButtons[(int)Upgrade.Fire2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Fire2));
        upgradeButtons[(int)Upgrade.Fire3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Fire3));
        upgradeButtons[(int)Upgrade.Arrow1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Arrow1));
        upgradeButtons[(int)Upgrade.Arrow2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Arrow2));
        upgradeButtons[(int)Upgrade.Arrow3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Arrow3));
        upgradeButtons[(int)Upgrade.GravityFlip1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.GravityFlip1));
        upgradeButtons[(int)Upgrade.GravityFlip2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.GravityFlip2));
        upgradeButtons[(int)Upgrade.GravityFlip3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.GravityFlip3));
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkillButtonPressed(Skill skill)
    {
        Debug.Log(skill.ToString() + " Button Pressed!");
        dayCount++;
        if (skills[(int)skill].purchased)
        {
            //switch text
        } else if (skills[(int)skill].available)
        {
            //switch text but make purchase button available
        }
        
    }

    public void UpgradeButtonPressed(Upgrade upgrade)
    {
        Debug.Log(upgrade.ToString() + " Button Pressed!");
        if (upgrades[(int)upgrade].purchased)
        {
            //switch text
        } else if (upgrades[(int)upgrade].available)
        {
            //switch text but make purchase button available
        }
    }

    public void PlayButton()
    {
        //TODO change to load new day with settings
        dayCount++;
        SceneManager.LoadScene("Game");
    }

    public void SaveAndQuitButton()
    {
        saveSystem.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Loading data from shop controller");
        dayCount = data.dayCount;
        money = data.money;
        skills = data.skills;
        upgrades = data.upgrades;
        for(int i = 0; i < skills.Count; i++)
        {
            SkillButton current = skills[i];

            if (current.purchased)
            {
                Debug.Log(current.skill.ToString() + " is purchased!");
                Debug.Log(skillButtons[i].ToString() + " is now interactable!");
                skillButtons[i].interactable = true;
                ColorBlock cb = skillButtons[i].colors;
                cb.normalColor = Color.green;
                skillButtons[i].colors = cb;
            } else if (current.available)
            {
                Debug.Log(current.skill.ToString() + " is available!");
                Debug.Log(skillButtons[i].ToString() + " is now interactable!");
                skillButtons[i].interactable = true;
            } else
            {
                Debug.Log(current.skill.ToString() + " is not available.");
                Debug.Log(skillButtons[i].ToString() + " is NOT interactable.");
                skillButtons[i].interactable = false;
            }
        }
        for(int i = 0; i < upgrades.Count; i++)
        {
            UpgradeButton current = upgrades[i];

            if (current.purchased)
            {
                Debug.Log(current.upgrade.ToString() + " is purchased!");
                Debug.Log(skillButtons[i].ToString() + " is now interactable!");
                upgradeButtons[i].interactable = true;
                ColorBlock cb = upgradeButtons[i].colors;
                cb.normalColor = Color.green;
                upgradeButtons[i].colors = cb;
            } else if (current.available)
            {
                Debug.Log(current.upgrade + " is available!");
                Debug.Log(upgradeButtons[i].ToString() + " is now interactable!");
                upgradeButtons[i].interactable = true;
            } else
            {
                Debug.Log(current.upgrade.ToString() + " is not available.");
                Debug.Log(upgradeButtons[i].ToString() + " is NOT interactable.");
                upgradeButtons[i].interactable = false;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving data from shop controller");
        data.dayCount = this.dayCount;
        data.money = this.money;
        data.skills = this.skills;
        data.upgrades = this.upgrades;
    }
}
