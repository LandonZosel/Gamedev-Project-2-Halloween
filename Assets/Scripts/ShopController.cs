using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal.Commands;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour, ISaveSystem
{
    [SerializeField] 
    private List<Button> upgradeButtons;
    [SerializeField] 
    private List<Button> skillButtons;
    [SerializeField] 
    private Button purchaseButton;
    [SerializeField] 
    private TMP_Text purchaseButtonText;
    [SerializeField] 
    private Button setSkillButton;
    [SerializeField] 
    private TMP_Text titleText;
    [SerializeField] 
    private TMP_Text subTitleText;
    [SerializeField] 
    private TMP_Text effectText;
    [SerializeField] 
    private Image skillImage;
    [SerializeField] 
    private Button leftSkillButton;
    [SerializeField] 
    private Button upSkillButton;
    [SerializeField] 
    private Button rightSkillButton;
    [SerializeField] 
    private Button downSkillButton;
    [SerializeField] 
    private TMP_Text moneyText;
    [SerializeField]
    private SaveSystem saveSystem;
    private List<SkillButton> skills = new List<SkillButton>();
    private SkillButton selectedSkill;
    private List<UpgradeButton> upgrades = new List<UpgradeButton>();
    private UpgradeButton selectedUpgrade;
    private int dayCount;
    private int money;
    
    void Start()
    {
        skillButtons[(int)Skill.Jump].onClick.AddListener(() => SkillButtonPressed(Skill.Jump));
        skillButtons[(int)Skill.Explode].onClick.AddListener(() => SkillButtonPressed(Skill.Explode));
        skillButtons[(int)Skill.Hover].onClick.AddListener(() => SkillButtonPressed(Skill.Hover));
        skillButtons[(int)Skill.Magnet].onClick.AddListener(() => SkillButtonPressed(Skill.Magnet));
        skillButtons[(int)Skill.Float].onClick.AddListener(() => SkillButtonPressed(Skill.Float));
        skillButtons[(int)Skill.Fire].onClick.AddListener(() => SkillButtonPressed(Skill.Fire));
        skillButtons[(int)Skill.Arrow].onClick.AddListener(() => SkillButtonPressed(Skill.Arrow));
        skillButtons[(int)Skill.GravityFlip].onClick.AddListener(() => SkillButtonPressed(Skill.GravityFlip));

        upgradeButtons[(int)Upgrade.C1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C1));
        upgradeButtons[(int)Upgrade.C2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C2));
        upgradeButtons[(int)Upgrade.C3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C3));
        upgradeButtons[(int)Upgrade.C4].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C4));
        upgradeButtons[(int)Upgrade.C5].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.C5));
        upgradeButtons[(int)Upgrade.T1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.T1));
        upgradeButtons[(int)Upgrade.T2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.T2));
        upgradeButtons[(int)Upgrade.T3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.T3));
        upgradeButtons[(int)Upgrade.S1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.S1));
        upgradeButtons[(int)Upgrade.S2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.S2));
        upgradeButtons[(int)Upgrade.S3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.S3));
        upgradeButtons[(int)Upgrade.G1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.G1));
        upgradeButtons[(int)Upgrade.Jump1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Jump1));
        upgradeButtons[(int)Upgrade.Jump2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Jump2));
        upgradeButtons[(int)Upgrade.Jump3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Jump3));
        upgradeButtons[(int)Upgrade.Explode1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Explode1));
        upgradeButtons[(int)Upgrade.Explode2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Explode2));
        upgradeButtons[(int)Upgrade.Explode3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Explode3));
        upgradeButtons[(int)Upgrade.Hover1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Hover1));
        upgradeButtons[(int)Upgrade.Hover2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Hover2));
        upgradeButtons[(int)Upgrade.Hover3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Hover3));
        upgradeButtons[(int)Upgrade.Magnet1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Magnet1));
        upgradeButtons[(int)Upgrade.Magnet2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Magnet2));
        upgradeButtons[(int)Upgrade.Magnet3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Magnet3));
        upgradeButtons[(int)Upgrade.Float1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Float1));
        upgradeButtons[(int)Upgrade.Float2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Float2));
        upgradeButtons[(int)Upgrade.Float3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Float3));
        upgradeButtons[(int)Upgrade.Fire1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Fire1));
        upgradeButtons[(int)Upgrade.Fire2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Fire2));
        upgradeButtons[(int)Upgrade.Fire3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Fire3));
        upgradeButtons[(int)Upgrade.Arrow1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Arrow1));
        upgradeButtons[(int)Upgrade.Arrow2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Arrow2));
        upgradeButtons[(int)Upgrade.Arrow3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.Arrow3));
        upgradeButtons[(int)Upgrade.GravityFlip1].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.GravityFlip1));
        upgradeButtons[(int)Upgrade.GravityFlip2].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.GravityFlip2));
        upgradeButtons[(int)Upgrade.GravityFlip3].onClick.AddListener(() => UpgradeButtonPressed(Upgrade.GravityFlip3));

        leftSkillButton.image.alphaHitTestMinimumThreshold = 0.5f;
        upSkillButton.image.alphaHitTestMinimumThreshold = 0.5f;
        rightSkillButton.image.alphaHitTestMinimumThreshold = 0.5f;
        downSkillButton.image.alphaHitTestMinimumThreshold = 0.5f;

        leftSkillButton.onClick.AddListener(() => OnSkillButton(Key.Left));
        upSkillButton.onClick.AddListener(() => OnSkillButton(Key.Up));
        rightSkillButton.onClick.AddListener(() => OnSkillButton(Key.Right));
        downSkillButton.onClick.AddListener(() => OnSkillButton(Key.Down));

        purchaseButton.gameObject.SetActive(false);
        setSkillButton.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        subTitleText.gameObject.SetActive(false);
        effectText.gameObject.SetActive(false);
        skillImage.gameObject.SetActive(false);
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkillButtonPressed(Skill skill)
    {
        UpdateSkillButtonState(false);

        selectedSkill = skills[(int)skill];
        selectedUpgrade = null;
        skillImage.gameObject.SetActive(true);
        skillImage.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
        if (selectedSkill.purchased)
        {
            //reveal set button and hide purchase button
            purchaseButton.gameObject.SetActive(false);
            setSkillButton.gameObject.SetActive(true);
            //switch text
            SwitchSkillText();
        } else if (selectedSkill.available)
        {
            //reveal purchase button
            purchaseButton.gameObject.SetActive(true);
            setSkillButton.gameObject.SetActive(false);
            purchaseButton.interactable = selectedSkill.cost <= money;
            purchaseButtonText.text = "Purchase";
            //switch text
            SwitchSkillText();
        }
        
    }

    public void UpgradeButtonPressed(Upgrade upgrade)
    {
        UpdateSkillButtonState(false);

        selectedUpgrade = upgrades[(int)upgrade];
        selectedSkill = null;
        //reveal purchase button and hide set button
        purchaseButton.gameObject.SetActive(true);
        setSkillButton.gameObject.SetActive(false);
        skillImage.gameObject.SetActive(false);
        if (selectedUpgrade.purchased)
        {
            //switch purchase button text and make not interactable
            purchaseButton.interactable = false;
            purchaseButtonText.text = "Purchased";
            //switch text
            SwitchUpgradeText();
        } else if (selectedUpgrade.available)
        {
            //switch purchase button text and make interactable
            purchaseButton.interactable = selectedUpgrade.cost <= money;
            purchaseButtonText.text = "Purchase";
            //switch text
            SwitchUpgradeText();
        }
    }

    public void SwitchSkillText()
    {
        SwitchTitleText(selectedSkill.title);
        SwitchSubTitleText(selectedSkill.subTitle);
        SwitchEffectText(selectedSkill.effect);
    }

    public void SwitchUpgradeText()
    {
        SwitchTitleText(selectedUpgrade.title);
        SwitchSubTitleText(selectedUpgrade.subTitle);
        SwitchEffectText(selectedUpgrade.effect);
    }

    public void SwitchTitleText(string text)
    {
        if (!titleText.IsActive())
        {
            titleText.gameObject.SetActive(true);
        }
        titleText.text = text;
    }

    public void SwitchSubTitleText(string text)
    {
        if (!subTitleText.IsActive())
        {
            subTitleText.gameObject.SetActive(true);
        }
        subTitleText.text = text;
    }

    public void SwitchEffectText(string text)
    {
        if (!effectText.IsActive())
        {
            effectText.gameObject.SetActive(true);
        }
        effectText.text = text;
    }

    public void PlayButton()
    {
        //TODO change to load new day with settings
        dayCount++;
        SceneManager.LoadScene("Game");
    }

    public void PurchaseButton()
    {
        if (selectedSkill != null)
        {
            PurchaseSkill();
        } else if (selectedUpgrade != null)
        {
            PurchaseUpgrade();
        }
    }

    public void PurchaseSkill()
    {
        //TODO purchase currently selected skill if enough money
        if (selectedSkill.cost <= money)
        {
            money -= selectedSkill.cost;
            SetMoneyText();
            selectedSkill.purchased = true;
            //change button to set button
            purchaseButton.gameObject.SetActive(false);
            setSkillButton.gameObject.SetActive(true);

            //set new purchased colors
            ColorBlock cb = skillButtons[(int)selectedSkill.skill].colors;
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.greenYellow;
            cb.pressedColor = Color.green;
            skillButtons[(int)selectedSkill.skill].colors = cb;

            //activate future upgrade
            upgrades[(int)selectedSkill.onPurchaseUpgradeUnlock].available = true;
            upgradeButtons[(int)selectedSkill.onPurchaseUpgradeUnlock].interactable = true;
        }

        
    }

    public void PurchaseUpgrade()
    {
        //TODO purchase currently selected upgrade if enough money
        if (selectedUpgrade.cost <= money)
        {
            money -= selectedUpgrade.cost;
            SetMoneyText();
            selectedUpgrade.purchased = true;

            //change purchase button to purchased and inactive
            purchaseButton.interactable = false;
            purchaseButtonText.text = "Purchased";

            //set new purchased colors
            ColorBlock cb = upgradeButtons[(int)selectedUpgrade.upgrade].colors;
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.greenYellow;
            cb.pressedColor = Color.green;
            upgradeButtons[(int)selectedUpgrade.upgrade].colors = cb;

            //activate future upgrades
            for (int i = 0; i < selectedUpgrade.onPurchaseUpgradeUnlocks.Count; i++)
            {
                if (selectedUpgrade.onPurchaseUpgradeUnlocks[i] == Upgrade.G1)
                {
                    if (upgrades[(int)Upgrade.GravityFlip3].purchased
                    && upgrades[(int)Upgrade.Explode3].purchased
                    && upgrades[(int)Upgrade.Magnet3].purchased)
                    {
                        upgrades[(int)selectedUpgrade.onPurchaseUpgradeUnlocks[i]].available = true;
                        upgradeButtons[(int)selectedUpgrade.onPurchaseUpgradeUnlocks[i]].interactable = true;
                    }
                } else
                {
                    upgrades[(int)selectedUpgrade.onPurchaseUpgradeUnlocks[i]].available = true;
                    upgradeButtons[(int)selectedUpgrade.onPurchaseUpgradeUnlocks[i]].interactable = true;
                }
                
            }

            //activate future skill
            skills[(int)selectedUpgrade.onPurchaseSkillUnlock].available = true;
            skillButtons[(int)selectedUpgrade.onPurchaseSkillUnlock].interactable = true;
        }
    }

    public void SetMoneyText()
    {
        moneyText.text = "Money:\n$" + money;
    }

    public void SetSkillButton()
    {
        if (selectedSkill != null)
        {
            UpdateSkillButtonState(true);
        }
    }

    public void OnSkillButton(Key newKey)
    {
        if (selectedSkill != null)
        {
            if (selectedSkill.keyAssigned == Key.None)
            {
                Debug.Log("Assigning " + selectedSkill.skill + " to " + newKey);
                selectedSkill.keyAssigned = newKey;
                switch (newKey)
                {
                    case Key.Left:
                        leftSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                        break;
                    case Key.Up:
                        upSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                        break;
                    case Key.Right:
                        rightSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                        break;
                    case Key.Down:
                        downSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                        break;
                }
                
            } else if (selectedSkill.keyAssigned != newKey)
            {
                //need to swap sprites
                Key oldKey = selectedSkill.keyAssigned;
                SkillButton oldSkill = null;

                foreach(SkillButton skill in skills)
                {
                    if (skill.keyAssigned == newKey)
                    {
                        oldSkill = skill;
                        break;
                    }
                }

                if (oldSkill == null)
                {
                    Debug.Log("Removing " + selectedSkill.skill + " from " + oldKey + " and assigning to " + newKey);
                    //no old skill, need to just remove and add
                    switch (oldKey)
                    {
                        case Key.Left:
                            leftSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/empty_skill");
                            break;
                        case Key.Up:
                            upSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/empty_skill");
                            break;
                        case Key.Right:
                            rightSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/empty_skill");
                            break;
                        case Key.Down:
                            downSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/empty_skill");
                            break;
                    }

                    selectedSkill.keyAssigned = newKey;
                    switch (newKey)
                    {
                        case Key.Left:
                            leftSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                            break;
                        case Key.Up:
                            upSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                            break;
                        case Key.Right:
                            rightSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                            break;
                        case Key.Down:
                            downSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                            break;
                    }
                } else
                {
                    Debug.Log("Removing " + selectedSkill.skill + " from " + oldKey + " and assigning to " + newKey);
                    Debug.Log(oldSkill.skill + " must go to " + oldKey);
                    //swap skills
                    oldSkill.keyAssigned = oldKey;
                    switch (oldKey)
                    {
                        case Key.Left:
                            leftSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + oldSkill.imageFileName);
                            break;
                        case Key.Up:
                            upSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + oldSkill.imageFileName);
                            break;
                        case Key.Right:
                            rightSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + oldSkill.imageFileName);
                            break;
                        case Key.Down:
                            downSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + oldSkill.imageFileName);
                            break;
                    }

                    selectedSkill.keyAssigned = newKey;
                    switch (newKey)
                    {
                        case Key.Left:
                            leftSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                            break;
                        case Key.Up:
                            upSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                            break;
                        case Key.Right:
                            rightSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                            break;
                        case Key.Down:
                            downSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + selectedSkill.imageFileName);
                            break;
                    }
                }

            }
        }
        UpdateSkillButtonState(false);
    }

    public void UpdateSkillButtonState(bool state)
    {
        leftSkillButton.interactable = state;
        upSkillButton.interactable = state;
        rightSkillButton.interactable = state;
        downSkillButton.interactable = state;
    }

    public void SaveAndQuitButton()
    {
        saveSystem.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void LoadData(GameData data)
    {
        Debug.Log("Loading data from shop controller");
        dayCount = data.dayCount;
        money = data.money;
        SetMoneyText();
        skills = data.skills;
        upgrades = data.upgrades;
        for(int i = 0; i < skills.Count; i++)
        {
            SkillButton current = skills[i];

            if (current.purchased)
            {
                skillButtons[i].interactable = true;
                ColorBlock cb = skillButtons[i].colors;
                cb.normalColor = Color.green;
                cb.highlightedColor = Color.greenYellow;
                cb.pressedColor = Color.green;
                skillButtons[i].colors = cb;
            } else if (current.available)
            {
                skillButtons[i].interactable = true;
            } else
            {
                skillButtons[i].interactable = false;
            }

            //set skill images
            if (current.keyAssigned != Key.None)
            {
                switch (current.keyAssigned)
                {
                    case Key.Left:
                        leftSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + current.imageFileName);
                        break;
                    case Key.Up:
                        upSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + current.imageFileName);
                        break;
                    case Key.Right:
                        rightSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + current.imageFileName);
                        break;
                    case Key.Down:
                        downSkillButton.image.sprite = Resources.Load<Sprite>("Sprites/" + current.imageFileName);
                        break;
                }
            }
        }

        for(int i = 0; i < upgrades.Count; i++)
        {
            UpgradeButton current = upgrades[i];

            if (current.purchased)
            {
                upgradeButtons[i].interactable = true;
                ColorBlock cb = upgradeButtons[i].colors;
                cb.normalColor = Color.green;
                cb.highlightedColor = Color.greenYellow;
                cb.pressedColor = Color.green;
                upgradeButtons[i].colors = cb;
            } else if (current.available)
            {
                upgradeButtons[i].interactable = true;
            } else
            {
                upgradeButtons[i].interactable = false;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving data from shop controller");
        data.dayCount = this.dayCount;
        data.money = this.money;
        data.skills = this.skills;
        data.upgrades = this.upgrades;
    }
}
