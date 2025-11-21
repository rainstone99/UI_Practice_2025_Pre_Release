using UnityEngine;

public class UiScript : MonoBehaviour //필드값 && 함수호출만
{
    private ButtonScript buttonScript = new();
    private InventoryButtonScript inventoryButtonScript = new();
    [SerializeField] private Inventory inventoryScript;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotCount; // 자식 개체 검색 기능 이용
    private Slot[] slots;
    void Start()
    {
        slots = slotCount.GetComponentsInChildren<Slot>();
    }
    public void OnClickInventoryOpen(GameObject _inventoryTap)
    {
        buttonScript.OpenInventory(_inventoryTap);
    }
    public void OnClickInventoryClose(GameObject _inventoryTap)
    {
        buttonScript.CloseInventory(_inventoryTap);
    }
    public void OnClickAddItem(Item item)
    {
        inventoryScript.GainItem(item, 1);
    }
    public void OnClickAddSlot()
    {
        inventoryButtonScript.AddSlot(slots.Length, slotPrefab, slotCount.transform);
        slots = inventoryScript.GetComponentsInChildren<Slot>(); // slots 갱신 해야 됨
        inventoryScript.SlotAdd(); // slots 받아와야 됨
    }
}
public class ButtonScript // 버튼으로 동작하는 단순 스크립트 모음
{
    public void OpenInventory(GameObject _tap)
    {
        _tap.SetActive(true);
    }
    public void CloseInventory(GameObject _tap)
    {
        _tap.SetActive(false);
    }

}
public class InventoryButtonScript // 인벤토리 관련인데 분리해야 편할 듯
{
    public void AddSlot(int slots, GameObject prefab, Transform position) // Monobehaviour를 상속받지 않으므로 기존의 Instantiate 사용이 안 됨
    {
        if(slots < 36) // 추후 확장 가능
        {
            GameObject newItem = UnityEngine.Object.Instantiate(prefab, position); // Vector2.zero 넣을 필요 없음
            RectTransform itemPosition = newItem.GetComponent<RectTransform>(); // 캔버스에 들어가는거라서 위치 재설정
            itemPosition.anchoredPosition = Vector2.zero; //좌표값 재조정
            itemPosition.localScale = Vector3.one; // 스케일 변형 방지
            Debug.Log("현재 슬롯 개수 :" + slots);
        }
        else
        {
            Debug.Log("최대치입니다. (36)");
        }
    }
}