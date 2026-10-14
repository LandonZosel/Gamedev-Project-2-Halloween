[System.Serializable]
public class UpgradeButton
{
    public Upgrade upgrade;
    public bool available;
    public bool purchased;
    public int cost;

    public UpgradeButton(Upgrade upgrade, bool available, bool purchased, int cost)
    {
        this.upgrade = upgrade;
        this.available = available;
        this.purchased = purchased;
        this.cost = cost;
    }
}
