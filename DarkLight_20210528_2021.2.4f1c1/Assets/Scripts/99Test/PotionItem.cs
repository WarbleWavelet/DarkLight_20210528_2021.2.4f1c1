using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Potion",menuName ="Item/Potion")]
public class PotionItem : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int itemAmout = 1;
    public string itemOwner;
}
