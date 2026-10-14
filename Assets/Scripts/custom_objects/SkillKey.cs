using UnityEngine.UI;

public class SkillKey
{
    public Key key;
    public Skill skill;
    public float startCooldown;
    public float endCooldown;
    public string imageFileName;
    public Image fillReference;

    public SkillKey(Key key, Skill skill, string imageFileName, Image fillReference)
    {
        this.key = key;
        this.skill = skill;
        startCooldown = 0;
        endCooldown = 0;
        this.imageFileName = imageFileName;
        this.fillReference = fillReference;
    }
}
