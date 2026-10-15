using System.Collections.Generic;

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
        skills.Add(new SkillButton(Key.None, Skill.Jump, true, false, 20, "Jump", "On use: makes the player jump.", "Cooldown: 10s\nTime: N/A", "jump_skill", Upgrade.C1));
        skills.Add(new SkillButton(Key.None, Skill.Explode, false, false, 20, "Explode", "", "", "explode_skill", Upgrade.Explode1));
        skills.Add(new SkillButton(Key.None, Skill.Hover, true, false, 20, "Hover", "", "", "hover_skill", Upgrade.Hover1));
        skills.Add(new SkillButton(Key.None, Skill.Magnet, false, false, 20, "Magnet", "", "", "magnet_skill", Upgrade.Magnet1));
        skills.Add(new SkillButton(Key.None, Skill.Float, false, false, 20, "Float", "", "", "float_skill", Upgrade.Float1));
        skills.Add(new SkillButton(Key.None, Skill.Fire, true, false, 20, "Fire", "", "", "fire_skill", Upgrade.Fire1));
        skills.Add(new SkillButton(Key.None, Skill.Arrow, false, false, 20, "Arrow", "", "", "arrow_skill", Upgrade.Arrow1));
        skills.Add(new SkillButton(Key.None, Skill.GravityFlip, false, false, 20, "Gravity Flip", "", "", "gravity_flip_skill", Upgrade.GravityFlip1));
        upgrades.Add(new UpgradeButton(Upgrade.C1, false, false, 20, "Cannon Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.Jump1}));
        upgrades.Add(new UpgradeButton(Upgrade.C2, false, false, 20, "Cannon Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.T1}));
        upgrades.Add(new UpgradeButton(Upgrade.C3, false, false, 20, "Cannon Upgrade 3", "", "", null, new List<Upgrade>(){Upgrade.Jump2}));
        upgrades.Add(new UpgradeButton(Upgrade.C4, false, false, 20, "Cannon Upgrade 4", "", "", null, new List<Upgrade>(){Upgrade.S1}));
        upgrades.Add(new UpgradeButton(Upgrade.C5, false, false, 20, "Cannon Upgrade 5", "", "", null, new List<Upgrade>(){Upgrade.Jump3}));
        upgrades.Add(new UpgradeButton(Upgrade.T1, false, false, 20, "Trampoline Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.C3}));
        upgrades.Add(new UpgradeButton(Upgrade.T2, false, false, 20, "Trampoline Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.Arrow3}));
        upgrades.Add(new UpgradeButton(Upgrade.T3, false, false, 20, "Trampoline Upgrade 3", "", "", null, new List<Upgrade>(){Upgrade.Hover3}));
        upgrades.Add(new UpgradeButton(Upgrade.S1, false, false, 20, "Speed Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.C5}));
        upgrades.Add(new UpgradeButton(Upgrade.S2, false, false, 20, "Speed Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.Fire3}));
        upgrades.Add(new UpgradeButton(Upgrade.S3, false, false, 20, "Speed Upgrade 3", "", "", null, new List<Upgrade>(){Upgrade.Float3}));
        upgrades.Add(new UpgradeButton(Upgrade.G1, false, false, 20, "Gravity Upgrade", "", "", null, null));
        upgrades.Add(new UpgradeButton(Upgrade.Jump1, false, false, 20, "Jump Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.C2}));
        upgrades.Add(new UpgradeButton(Upgrade.Jump2, false, false, 20, "Jump Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.C4}));
        upgrades.Add(new UpgradeButton(Upgrade.Jump3, false, false, 20, "Jump Upgrade 3", "", "", Skill.Explode, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.Explode1, false, false, 20, "Explode! Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.Explode2}));
        upgrades.Add(new UpgradeButton(Upgrade.Explode2, false, false, 20, "Explode! Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.Explode3}));
        upgrades.Add(new UpgradeButton(Upgrade.Explode3, false, false, 20, "Explode! Upgrade 3", "", "", null, new List<Upgrade>(){Upgrade.G1}));
        upgrades.Add(new UpgradeButton(Upgrade.Hover1, false, false, 20, "Hover Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.Hover2, Upgrade.T3}));
        upgrades.Add(new UpgradeButton(Upgrade.Hover2, false, false, 20, "Hover Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.Hover3}));
        upgrades.Add(new UpgradeButton(Upgrade.Hover3, false, false, 20, "Hover Upgrade 3", "", "", Skill.Float, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet1, false, false, 20, "Magnet Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.Magnet2}));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet2, false, false, 20, "Magnet Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.Magnet3}));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet3, false, false, 20, "Magnet Upgrade 3", "", "", null, new List<Upgrade>(){Upgrade.G1}));
        upgrades.Add(new UpgradeButton(Upgrade.Float1, false, false, 20, "Float Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.Float2, Upgrade.S3}));
        upgrades.Add(new UpgradeButton(Upgrade.Float2, false, false, 20, "Float Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.Float3}));
        upgrades.Add(new UpgradeButton(Upgrade.Float3, false, false, 20, "Float Upgrade 3", "", "", Skill.GravityFlip, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.Fire1, false, false, 20, "Fire Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.Fire2, Upgrade.S2}));
        upgrades.Add(new UpgradeButton(Upgrade.Fire2, false, false, 20, "Fire Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.Fire3}));
        upgrades.Add(new UpgradeButton(Upgrade.Fire3, false, false, 20, "Fire Upgrade 3", "", "", Skill.Arrow, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow1, false, false, 20, "Arrow Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.Arrow2, Upgrade.T2}));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow2, false, false, 20, "Arrow Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.Arrow3}));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow3, false, false, 20, "Arrow Upgrade 3", "", "", Skill.Magnet, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip1, false, false, 20, "Gravity Flip Upgrade 1", "", "", null, new List<Upgrade>(){Upgrade.GravityFlip2}));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip2, false, false, 20, "Gravity Flip Upgrade 2", "", "", null, new List<Upgrade>(){Upgrade.GravityFlip3}));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip3, false, false, 20, "Gravity Flip Upgrade 3", "", "", null, new List<Upgrade>(){Upgrade.G1}));
    }
}
