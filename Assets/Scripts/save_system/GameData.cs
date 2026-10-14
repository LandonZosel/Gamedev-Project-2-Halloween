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
        upgrades.Add(new UpgradeButton(Upgrade.C1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.C2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.C1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.C1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.C1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.T1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.T1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.T1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.S1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.S2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.S3, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.G1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Jump1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Jump2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Jump3, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Explode1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Explode2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Explode3, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Hover1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Hover2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Hover3, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet3, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Speed1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Speed2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Speed3, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Fire1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Fire2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Fire3, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow3, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip1, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip2, false, false, 20));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip3, false, false, 20));
    }
}
