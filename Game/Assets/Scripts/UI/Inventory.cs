using UnityEngine;

public class Inventory : MonoBehaviour
{
    //private static bool isInventoryOpen = false; // 이거 좋다, 나중에 쓸 듯

    [SerializeField]
    private GameObject inventoryTap;
    [SerializeField]
    private GameObject inventorySlotBase; // 자식 개체 검색기능 사용

    private Slot[] slots;
    void Start()
    {
        slots = inventorySlotBase.GetComponentsInChildren<Slot>(); // Slot 컴포넌트를 가진 오브젝트가 배열에 저장됨
    }
    public void GainItem(Item _item, int _count = 1) // 아이템 획득 시 슬롯에 추가
    {
        // 아이템이 들어오면 인벤토리를 쭉 스캔 후 중첩 or 혹은 새 슬롯에 추가(슬롯에 활성화에 더 가까움)
        for (int i = 0; i < slots.Length; i++) // 중복 시
        {
            if(slots[i].item != null)
            {
                if(slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
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
    }
}
