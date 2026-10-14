using System.Collections.Generic;

[System.Serializable]
public class UpgradeButton
{
    public Upgrade upgrade;
    public bool available;
    public bool purchased;
    public int cost;
    public string title;
    public string subTitle;
    public string effect;
    public Skill onPurchaseSkillUnlock;
    public List<Upgrade> onPurchaseUpgradeUnlocks;

    public UpgradeButton(
        Upgrade upgrade, 
        bool available, 
        bool purchased, 
        int cost,
        string title, 
        string subTitle, 
        string effect,
        Skill? onPurchaseSkillUnlock,
        List<Upgrade> onPurchaseUpgradeUnlocks
    ) {
        this.upgrade = upgrade;
        this.available = available;
        this.purchased = purchased;
        this.cost = cost;
        this.title = title;
        this.subTitle = subTitle;
        this.effect = effect;
        if (onPurchaseSkillUnlock != null)
        {
            this.onPurchaseSkillUnlock = (Skill)onPurchaseSkillUnlock;
        }
        this.onPurchaseUpgradeUnlocks = onPurchaseUpgradeUnlocks;
    }
}
