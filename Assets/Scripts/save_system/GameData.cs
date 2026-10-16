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
        skills.Add(new SkillButton(Key.None, Skill.Jump, true, false, 20, "Jump", "Instant: Makes the pumpkin jump forward", "Cooldown: 10s\nTime: N/A", "jump_skill", Upgrade.C1));
        skills.Add(new SkillButton(Key.None, Skill.Explode, false, false, 1500, "Explode", "Instant: Shoots the pumpkin upwards with extreme force", "Cooldown: 45s\nTime: N/A", "explode_skill", Upgrade.Explode1));
        skills.Add(new SkillButton(Key.None, Skill.Hover, true, false, 20, "Hover", "Timed: Makes the pumpkin unable to lose height", "Cooldown: 30s\nTime: 5s", "hover_skill", Upgrade.Hover1));
        skills.Add(new SkillButton(Key.None, Skill.Magnet, false, false, 1500, "Magnet", "Timed: Attracts nearby objects", "Cooldown: 30s\nTime: 10s", "magnet_skill", Upgrade.Magnet1));
        skills.Add(new SkillButton(Key.None, Skill.Float, false, false, 300, "Float", "Timed: Makes the pumpkin slowly descend", "Cooldown: 30s\nTime: 10s", "float_skill", Upgrade.Float1));
        skills.Add(new SkillButton(Key.None, Skill.Fire, true, false, 20, "Fire", "Timed: Sets the pumpkin on fire giving the ability to burn ghost", "Cooldown: 30s\nTime: 15s", "fire_skill", Upgrade.Fire1));
        skills.Add(new SkillButton(Key.None, Skill.Arrow, false, false, 300, "Arrow", "Timed: Gives an arrow that displays the pumpkins current trajectory", "Cooldown: 30s\nTime: 15s", "arrow_skill", Upgrade.Arrow1));
        skills.Add(new SkillButton(Key.None, Skill.GravityFlip, false, false, 1500, "Gravity Flip", "Timed: Inverts the affect of gravity", "Cooldown: 60s\nTime: 5s", "gravity_flip_skill", Upgrade.GravityFlip1));
        upgrades.Add(new UpgradeButton(Upgrade.C1, false, false, 50, "Cannon Upgrade 1", "Increases cannon power", "Power * 2", null, new List<Upgrade>(){Upgrade.Jump1}));
        upgrades.Add(new UpgradeButton(Upgrade.C2, false, false, 200, "Cannon Upgrade 2", "Makes the cannon minigame easier", "Shift Interval - 0.5", null, new List<Upgrade>(){Upgrade.T1}));
        upgrades.Add(new UpgradeButton(Upgrade.C3, false, false, 400, "Cannon Upgrade 3", "Increases cannon power", "Power * 2", null, new List<Upgrade>(){Upgrade.Jump2}));
        upgrades.Add(new UpgradeButton(Upgrade.C4, false, false, 600, "Cannon Upgrade 4", "Makes the cannon minigame easier", "Shift Interval - 0.5", null, new List<Upgrade>(){Upgrade.S1}));
        upgrades.Add(new UpgradeButton(Upgrade.C5, false, false, 800, "Cannon Upgrade 5", "Increases cannon power", "Power * 2", null, new List<Upgrade>(){Upgrade.Jump3}));
        upgrades.Add(new UpgradeButton(Upgrade.T1, false, false, 300, "Trampoline Upgrade 1", "Increases trampoline power", "Power * 2", null, new List<Upgrade>(){Upgrade.C3}));
        upgrades.Add(new UpgradeButton(Upgrade.T2, false, false, 500, "Trampoline Upgrade 2", "Increases trampoline spawn rate", "Rate * 2", null, new List<Upgrade>(){Upgrade.Arrow3}));
        upgrades.Add(new UpgradeButton(Upgrade.T3, false, false, 100, "Trampoline Upgrade 3", "Increases trampoline power", "Power * 2", null, new List<Upgrade>(){Upgrade.Hover3}));
        upgrades.Add(new UpgradeButton(Upgrade.S1, false, false, 700, "Speed Upgrade 1", "Increases max horizontal speed", "Max Speed * 2", null, new List<Upgrade>(){Upgrade.C5}));
        upgrades.Add(new UpgradeButton(Upgrade.S2, false, false, 100, "Speed Upgrade 2", "Increases horizontal movement speed", "Speed * 2", null, new List<Upgrade>(){Upgrade.Fire3}));
        upgrades.Add(new UpgradeButton(Upgrade.S3, false, false, 500, "Speed Upgrade 3", "Decreases forward air resistance", "Resistance * 0.75", null, new List<Upgrade>(){Upgrade.Float3}));
        upgrades.Add(new UpgradeButton(Upgrade.G1, false, false, 5000, "Gravity Manipulator", "Decreases gravity", "Gravity / 2", null, null));
        upgrades.Add(new UpgradeButton(Upgrade.Jump1, false, false, 100, "Jump Upgrade 1", "Decreases Jump cooldown", "- 1s", null, new List<Upgrade>(){Upgrade.C2}));
        upgrades.Add(new UpgradeButton(Upgrade.Jump2, false, false, 500, "Jump Upgrade 2", "Increases Jump power", "Power * 1.5", null, new List<Upgrade>(){Upgrade.C4}));
        upgrades.Add(new UpgradeButton(Upgrade.Jump3, false, false, 900, "Jump Upgrade 3", "Decreases Jump cooldown", "- 2s", Skill.Explode, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.Explode1, false, false, 500, "Explode! Upgrade 1", "Increases Explode! power", "Power * 2", null, new List<Upgrade>(){Upgrade.Explode2}));
        upgrades.Add(new UpgradeButton(Upgrade.Explode2, false, false, 1000, "Explode! Upgrade 2", "Decreases Explode! cooldown", "- 15s", null, new List<Upgrade>(){Upgrade.Explode3}));
        upgrades.Add(new UpgradeButton(Upgrade.Explode3, false, false, 1500, "Explode! Upgrade 3", "Increases Explode! power", "Power * 2", null, new List<Upgrade>(){Upgrade.G1}));
        upgrades.Add(new UpgradeButton(Upgrade.Hover1, false, false, 50, "Hover Upgrade 1", "Increases Hover time", "+ 2.5s", null, new List<Upgrade>(){Upgrade.Hover2, Upgrade.T3}));
        upgrades.Add(new UpgradeButton(Upgrade.Hover2, false, false, 100, "Hover Upgrade 2", "Decreases Hover cooldown", "- 10s", null, new List<Upgrade>(){Upgrade.Hover3}));
        upgrades.Add(new UpgradeButton(Upgrade.Hover3, false, false, 200, "Hover Upgrade 3", "Increases Hover time", "+ 2.5s", Skill.Float, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet1, false, false, 500, "Magnet Upgrade 1", "Decreases Magnet cooldown", "- 5s", null, new List<Upgrade>(){Upgrade.Magnet2}));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet2, false, false, 1000, "Magnet Upgrade 2", "Increases Magnet pull strenght", "Strength * 2", null, new List<Upgrade>(){Upgrade.Magnet3}));
        upgrades.Add(new UpgradeButton(Upgrade.Magnet3, false, false, 1500, "Magnet Upgrade 3", "Increases Magnet time", "+ 5s", null, new List<Upgrade>(){Upgrade.G1}));
        upgrades.Add(new UpgradeButton(Upgrade.Float1, false, false, 300, "Float Upgrade 1", "Decreases Float cooldown", "- 5s", null, new List<Upgrade>(){Upgrade.Float2, Upgrade.S3}));
        upgrades.Add(new UpgradeButton(Upgrade.Float2, false, false, 500, "Float Upgrade 2", "Increases Float time", "+ 5s", null, new List<Upgrade>(){Upgrade.Float3}));
        upgrades.Add(new UpgradeButton(Upgrade.Float3, false, false, 750, "Float Upgrade 3", "Decreases Float cooldown", "- 5s", Skill.GravityFlip, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.Fire1, false, false, 50, "Fire Upgrade 1", "Decreases Fire cooldown", "- 5s", null, new List<Upgrade>(){Upgrade.Fire2, Upgrade.S2}));
        upgrades.Add(new UpgradeButton(Upgrade.Fire2, false, false, 100, "Fire Upgrade 2", "Increases the effectiveness of the ghost burn multiplier", "Each ghost burned adds X2", null, new List<Upgrade>(){Upgrade.Fire3}));
        upgrades.Add(new UpgradeButton(Upgrade.Fire3, false, false, 200, "Fire Upgrade 3", "Increases Fire time", "+ 5s", Skill.Arrow, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow1, false, false, 300, "Arrow Upgrade 1", "Decreases Arrow cooldown", "- 5s", null, new List<Upgrade>(){Upgrade.Arrow2, Upgrade.T2}));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow2, false, false, 500, "Arrow Upgrade 2", "Arrow can now collect Benjamins", "Benjamin Amount * 10", null, new List<Upgrade>(){Upgrade.Arrow3}));
        upgrades.Add(new UpgradeButton(Upgrade.Arrow3, false, false, 750, "Arrow Upgrade 3", "Increases Arrow time", "+ 5s", Skill.Magnet, new List<Upgrade>(){}));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip1, false, false, 500, "Gravity Flip Upgrade 1", "Decreases Gravity Flip cooldown", "- 10s", null, new List<Upgrade>(){Upgrade.GravityFlip2}));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip2, false, false, 1000, "Gravity Flip Upgrade 2", "Increases Gravity Flip time", "+ 2.5s", null, new List<Upgrade>(){Upgrade.GravityFlip3}));
        upgrades.Add(new UpgradeButton(Upgrade.GravityFlip3, false, false, 1500, "Gravity Flip Upgrade 3", "Decreases Gravity Flip cooldown", "- 10s", null, new List<Upgrade>(){Upgrade.G1}));
    }
}
