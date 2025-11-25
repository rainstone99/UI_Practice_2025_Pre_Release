using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IDropHandler
{
    //private static bool isInventoryOpen = false; // 이거 좋다, 나중에 쓸 듯

    [SerializeField] private GameObject inventoryTap;
    [SerializeField] private GameObject inventorySlotBase; // 자식 개체 검색기능 사용
    [SerializeField] private GameObject treshCan;
    public Slot[] slots;
    void Start()
    {
        SlotAdd();
    }
    public void SlotAdd() // Slot 컴포넌트를 가진 오브젝트가 배열에 저장됨
    {
        slots = inventorySlotBase.GetComponentsInChildren<Slot>();
    }
    public void IncreaseItem(Item _item, int _count = 1) // 아이템 획득 시 슬롯에 추가
    {
        // 아이템이 들어오면 인벤토리를 쭉 스캔 후 중첩 or 혹은 새 슬롯에 추가(슬롯에 활성화에 더 가까움)
        for (int i = 0; i < slots.Length; i++) // 중복 시
        {
            if(IsAddItemAble(i, _item))
            {
                slots[i].SetSlotCount(_count);
                return;
            }
        }

        for (int i = 0; i < slots.Length; i++) // 중복 아닌 경우
        {
            if(slots[i].item == null) // ClearSlot()에서 item = null 로 해서 itemName에는 문자열 초기값("")이 들어감
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
        Debug.Log("슬롯이 부족합니다."); // 위에 조건을 다 통과 시 출력
    }
    public bool IsAddItemAble(int i, Item _item) // 코드가 너무 길어서 뺌
    {
        return slots[i].item != null && (slots[i].item.itemName == _item.itemName && !(slots[i].isCountMax));
    }
    public void DeleteItem(Slot _slot)
    {
        _slot.DecreaseItem(); // 기존 ClearSlot()에서 한개씩 삭제하기 위해 교체
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (treshCan == eventData.pointerEnter)
        {
            DeleteItem(DragSlot.instance.dragSlot); // dragslot 은 slot이랑 똑같음.
        }
        
    }
}
