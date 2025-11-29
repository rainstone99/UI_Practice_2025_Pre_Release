using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class Inventory : MonoBehaviour, IDropHandler
{
    //private static bool isInventoryOpen = false; // 추후 이걸로 연동해서 탭 on off, 게임 플레이 정지 등 한 번에 관리 

    [SerializeField] private GameObject inventoryTap;
    [SerializeField] private GameObject inventorySlotBase; // 자식 개체 검색기능 사용
    [SerializeField] private GameObject treshCan; // 휴지통
    [SerializeField] private GameObject slot; // 슬롯 프리펩
    public Slot[] slots;
    public IObjectPool<GameObject> Pool {get; private set;}
    public int initSlot = 12; // 시작 슬롯 개수
    public int maxSlot = 36; // 슬롯 최대 개수
    void Start()
    {
        initPool();
        SlotSync();
    }

    // 오브젝트 풀링 함수(생성, 활성화, 비활성화, 삭제, 중복판별여부, 최초 생성, 최대 개수)
    private void initPool()
    {
        Pool = new ObjectPool<GameObject>(CreateSlot, OnGetPool, OnReleasePool, OnDestroyPool, true, initSlot, maxSlot);

        // 최초 시작 시 maxSlot 만큼 생성
        for (int i = 0; i < maxSlot; i++)
        {
            GameObject slotPool = CreateSlot();

            // initslot 만큼만 남겨두고 비활성화
            if(i >= initSlot)
            {
                Pool.Release(slotPool);
            }
        }
    }

    // Slot 컴포넌트를 가진 오브젝트가 배열에 저장됨, 및 슬롯 개수 동기화
    public void SlotSync()
    {
        slots = inventorySlotBase.GetComponentsInChildren<Slot>();
    }
    
    // 아이템이 들어오면 인벤토리를 쭉 스캔 후 중첩 or 혹은 새 슬롯에 추가(슬롯에 활성화에 더 가까움)
    public void IncreaseItem(Item _item, int _count = 1)
    {
        // 중복 시
        for (int i = 0; i < slots.Length; i++)
        {
            if(IsAddItemAble(i, _item))
            {
                slots[i].SetSlotCount(_count);
                return;
            }
        }
        // 중복 아닌 경우
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == null) // ClearSlot()에서 item = null 로 해서 itemName에는 문자열 초기값("")이 들어감
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
        // 위에 조건을 다 통과 시(아이템 생성 불가 시) 출력
        Debug.Log("슬롯이 부족합니다."); 
    }

    // 아이템 추가 가능 여부 판별 (코드가 너무 길어서 뺌)
    public bool IsAddItemAble(int i, Item _item)
    {
        return slots[i].item != null && (slots[i].item.itemName == _item.itemName && !(slots[i].isCountMax));
    }

    //슬롯 제작
    public GameObject CreateSlot()
    {
        // 캔버스의 오브젝트를 좌표로 지정하면 해결 됨
        GameObject newSlot = UnityEngine.Object.Instantiate(slot, inventorySlotBase.transform);
        newSlot.GetComponent<Slot>().Pool = this.Pool; // 오브젝트의 Pool을 현재 스크립트와 연동
        return newSlot;
    }

    // 슬롯 추가
    public void AddSlot()
    {
        if(slots.Length < maxSlot)
        {
            GameObject newSlot = Pool.Get(); // Pool()의 함수 사용으로 생성 (Instantiate 사용X)
            // newSlot.transform.SetParent(inventorySlotBase.transform, false); // 부모 지정이 이미 되어 있어서 필요 X
            SlotSync(); // 슬롯 개수 동기화
            Debug.Log($"현재 슬롯 개수 : {slots.Length}");
        }
        else
        {
            Debug.Log($"최대치입니다. ({maxSlot})");
        }
    }

    // 오브젝트 풀링 함수
    private void OnGetPool(GameObject slot)
    {
        slot.SetActive(true);
    }
    private void OnReleasePool(GameObject slot)
    {
        slot.SetActive(false);
    }
    private void OnDestroyPool(GameObject slot)
    {
        Destroy(slot);
    }

    //아이템 삭제
    public void DeleteItem(Slot _slot)
    {
        _slot.DecreaseItem(); // 기존 ClearSlot()에서 한개씩 삭제하기 위해 교체
    }

    // 휴지통 전용 드롭 이벤트
    public void OnDrop(PointerEventData eventData)
    {
        if (treshCan == eventData.pointerEnter)
        {
            DeleteItem(DragSlot.instance.dragSlot); // dragslot 은 slot이랑 똑같음.
        }
        
    }
}
