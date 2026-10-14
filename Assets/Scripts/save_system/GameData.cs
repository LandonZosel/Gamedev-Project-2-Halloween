using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int dayCount;
    public int money;
    public List<SkillButton> skills = new List<SkillButton>();
    public List<UpgradeButton> upgrades = new List<UpgradeButton>();

    public GameData()
    {
        //Default new game values
        dayCount = 0;
        money = 0;
        skills.Add(new SkillButton(Key.None, Skill.Jump, true, false, 20));
        skills.Add(new SkillButton(Key.None, Skill.Explode, false, false, 20));
        skills.Add(new SkillButton(Key.None, Skill.Hover, true, false, 20));
        skills.Add(new SkillButton(Key.None, Skill.Magnet, false, false, 20));
        skills.Add(new SkillButton(Key.None, Skill.Speed, false, false, 20));
        skills.Add(new SkillButton(Key.None, Skill.Fire, true, false, 20));
        skills.Add(new SkillButton(Key.None, Skill.Arrow, false, false, 20));
        skills.Add(new SkillButton(Key.None, Skill.GravityFlip, false, false, 20));
        upgrades.Add(new UpgradeButton("C1", false, false, 20));
        upgrades.Add(new UpgradeButton("C2", false, false, 20));
        upgrades.Add(new UpgradeButton("C3", false, false, 20));
        upgrades.Add(new UpgradeButton("C4", false, false, 20));
        upgrades.Add(new UpgradeButton("C5", false, false, 20));
        upgrades.Add(new UpgradeButton("T1", false, false, 20));
        upgrades.Add(new UpgradeButton("T2", false, false, 20));
        upgrades.Add(new UpgradeButton("T3", false, false, 20));
        upgrades.Add(new UpgradeButton("S1", false, false, 20));
        upgrades.Add(new UpgradeButton("S2", false, false, 20));
        upgrades.Add(new UpgradeButton("S3", false, false, 20));
        upgrades.Add(new UpgradeButton("G1", false, false, 20));
        upgrades.Add(new UpgradeButton("Jump1", false, false, 20));
        upgrades.Add(new UpgradeButton("Jump2", false, false, 20));
        upgrades.Add(new UpgradeButton("Jump3", false, false, 20));
        upgrades.Add(new UpgradeButton("Explode1", false, false, 20));
        upgrades.Add(new UpgradeButton("Explode2", false, false, 20));
        upgrades.Add(new UpgradeButton("Explode3", false, false, 20));
        upgrades.Add(new UpgradeButton("Hover1", false, false, 20));
        upgrades.Add(new UpgradeButton("Hover2", false, false, 20));
        upgrades.Add(new UpgradeButton("Hover3", false, false, 20));
        upgrades.Add(new UpgradeButton("Magnet1", false, false, 20));
        upgrades.Add(new UpgradeButton("Magnet2", false, false, 20));
        upgrades.Add(new UpgradeButton("Magnet3", false, false, 20));
        upgrades.Add(new UpgradeButton("Speed1", false, false, 20));
        upgrades.Add(new UpgradeButton("Speed2", false, false, 20));
        upgrades.Add(new UpgradeButton("Speed3", false, false, 20));
        upgrades.Add(new UpgradeButton("Fire1", false, false, 20));
        upgrades.Add(new UpgradeButton("Fire2", false, false, 20));
        upgrades.Add(new UpgradeButton("Fire3", false, false, 20));
        upgrades.Add(new UpgradeButton("Arrow1", false, false, 20));
        upgrades.Add(new UpgradeButton("Arrow2", false, false, 20));
        upgrades.Add(new UpgradeButton("Arrow3", false, false, 20));
        upgrades.Add(new UpgradeButton("GravityFlip1", false, false, 20));
        upgrades.Add(new UpgradeButton("GravityFlip2", false, false, 20));
        upgrades.Add(new UpgradeButton("GravityFlip3", false, false, 20));
    }
}
