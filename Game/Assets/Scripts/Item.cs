using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObject/Item Data", order = int.MaxValue)]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public ItemType itemtype;
    public enum ItemType
    {
        Equipment, //장비
        Used, //소비 << 사용
        Ingredient, //재료 << 조합 재료
        Scraps, //잡동사니 << 판매 용도
        Quest //퀘스트(중요아이템) << 판매불가로 설정
    }
    public bool tradeAble;
    
}
