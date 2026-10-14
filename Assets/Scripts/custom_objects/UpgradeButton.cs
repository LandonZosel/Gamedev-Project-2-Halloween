[System.Serializable]
public class UpgradeButton
{
    public string name;
    public bool available;
    public bool purchased;
    public int cost;

    public UpgradeButton(string name, bool available, bool purchased, int cost)
    {
        this.name = name;
        this.available = available;
        this.purchased = purchased;
        this.cost = cost;
    }
}
