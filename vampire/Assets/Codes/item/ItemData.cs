using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Ranged, Glove, Shoe, Heal }
    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    
    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public float[] counts;


    [Header("# Weapon")]
    public GameObject projectile;
}