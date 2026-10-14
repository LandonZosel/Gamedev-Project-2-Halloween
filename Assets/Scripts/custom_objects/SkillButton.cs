using System.Collections.Generic;

[System.Serializable]
public class SkillButton
{
    public Key keyAssigned;
    public Skill skill;
    public bool available;
    public bool purchased;
    public int cost;
    public string title;
    public string subTitle;
    public string effect;
    public string imageFileName;
    public Upgrade onPurchaseUpgradeUnlock;

    public SkillButton(
        Key key, 
        Skill skill, 
        bool available, 
        bool purchased, 
        int cost, 
        string title, 
        string subTitle, 
        string effect,
        string imageFileName,
        Upgrade onPurchaseUpgradeUnlock
    ) {
        keyAssigned = key;
        this.skill = skill;
        this.available = available;
        this.purchased = purchased;
        this.cost = cost;
        this.title = title;
        this.subTitle = subTitle;
        this.effect = effect;
        this.imageFileName = imageFileName;
        this.onPurchaseUpgradeUnlock = onPurchaseUpgradeUnlock;
    }
}
