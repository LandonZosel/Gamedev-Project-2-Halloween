[System.Serializable]
public class SkillButton
{
    public Key keyAssigned;
    public Skill skill;
    public bool available;
    public bool purchased;
    public int cost;

    public SkillButton(Key key, Skill skill, bool available, bool purchased, int cost)
    {
        keyAssigned = key;
        this.skill = skill;
        this.purchased = purchased;
        this.cost = cost;
    }
}
