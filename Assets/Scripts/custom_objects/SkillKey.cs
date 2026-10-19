using UnityEngine.UI;

public class SkillKey
{
    public Key key;
    public Skill skill;
    public float startCooldown;
    public float endTime;
    public float endCooldown;
    public string imageFileName;
    public Image fillReference;
    public Image activeFillReference;

    public SkillKey(Key key, Skill skill, string imageFileName, Image fillReference, Image activeFillReference)
    {
        this.key = key;
        this.skill = skill;
        startCooldown = 0;
        endTime = 0;
        endCooldown = 0;
        this.imageFileName = imageFileName;
        this.fillReference = fillReference;
        this.activeFillReference = activeFillReference;
    }
}
