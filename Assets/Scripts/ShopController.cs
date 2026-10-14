using System.Collections.Generic;
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
    private int dayCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skills.Add(new SkillButton(
            Key.None,
            Skill.Jump,
            false,
            false,
            20
        ));
        skills.Add(new SkillButton(
            Key.None,
            Skill.Explode,
            false,
            false,
            20
        ));
        skills.Add(new SkillButton(
            Key.None,
            Skill.Hover,
            false,
            false,
            20
        ));
        skills.Add(new SkillButton(
            Key.None,
            Skill.Magnet,
            false,
            false,
            20
        ));
        skills.Add(new SkillButton(
            Key.None,
            Skill.Speed,
            false,
            false,
            20
        ));
        skills.Add(new SkillButton(
            Key.None,
            Skill.Fire,
            false,
            false,
            20
        ));
        skills.Add(new SkillButton(
            Key.None,
            Skill.Arrow,
            false,
            false,
            20
        ));
        skills.Add(new SkillButton(
            Key.None,
            Skill.GravityFlip,
            false,
            false,
            20
        ));

        skillButtons[0].onClick.AddListener(() => SkillButtonPressed(Skill.Jump));
        skillButtons[1].onClick.AddListener(() => SkillButtonPressed(Skill.Explode));
        skillButtons[2].onClick.AddListener(() => SkillButtonPressed(Skill.Hover));
        skillButtons[3].onClick.AddListener(() => SkillButtonPressed(Skill.Magnet));
        skillButtons[4].onClick.AddListener(() => SkillButtonPressed(Skill.Speed));
        skillButtons[5].onClick.AddListener(() => SkillButtonPressed(Skill.Fire));
        skillButtons[6].onClick.AddListener(() => SkillButtonPressed(Skill.Arrow));
        skillButtons[7].onClick.AddListener(() => SkillButtonPressed(Skill.GravityFlip));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkillButtonPressed(Skill skill)
    {
        Debug.Log(skill.ToString() + " Button Pressed!");
        dayCount++;
        // switch (skill)
        // {
        //     case Skill.Jump:
        //         SkillJump(skillKey);
        //         break;
        //     case Skill.Arrow:
        //         bottomText.AddText("Activated Arrow!");
        //         SkillArrow(skillKey);
        //         break;
        //     case Skill.Magnet:
        //         bottomText.AddText("Activated Magnet!");
        //         SkillMagnet(skillKey);
        //         break;
        //     case Skill.GravityFlip:
        //         bottomText.AddText("Activated Gravity Flip!");
        //         SkillGravityFlip(skillKey);
        //         break;
        //     case Skill.Hover:
        //         bottomText.AddText("Activated Hover!");
        //         SkillHover(skillKey);
        //         break;
        //     case Skill.Speed:
        //         bottomText.AddText("Activated Speed!");
        //         SkillSpeed(skillKey);
        //         break;
        //     case Skill.Fire:
        //         bottomText.AddText("Activated Fire!");
        //         SkillFire(skillKey);
        //         break;
        //     case Skill.Explode:
        //         bottomText.AddText("Activated Explode!");
        //         SkillExplode(skillKey);
        //         break;
        // }
        
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
        this.dayCount = data.dayCount;
    }

    public void SaveData(ref GameData data)
    {
        data.dayCount = this.dayCount;
    }
}
