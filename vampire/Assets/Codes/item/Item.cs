using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDescription;
    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDescription = texts[2];
        textName.text = data.itemName;
    }

    void OnEnable()
    {
        textLevel.text = string.Format("Lv.{0}", level+1);
        switch (data.itemType){
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Ranged:
                 if (level < data.damages.Length && level < data.counts.Length)
                    textDescription.text = string.Format(data.itemDescription, data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDescription.text = string.Format(data.itemDescription, data.damages[level] * 100);
                break;
            default:
                textDescription.text = string.Format(data.itemDescription);
                break;
        }
    }



    void LateUpdate()
    {
        textLevel.text = string.Format("Lv.{0}", level+1);
    }

    public void OnClick()
    {
       switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Ranged:
                if(level ==0 ){
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0){
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                } else {
                    float rate = data.damages[level];
                    gear.LevelUp(rate);
                
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }
        level++;

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }

}
