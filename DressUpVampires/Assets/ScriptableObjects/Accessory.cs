using UnityEngine;

[CreateAssetMenu(fileName = "Accessory", menuName = "Scriptable Objects/Accessory")]
public class Accessory : ScriptableObject
{
    public string accessoryName;
    public Sprite accessorySprite;
    public Sprite accessoryIcon;
    public AccessoryTypes accessoryType;
    public AccessoryTags accessoryTag;
}
