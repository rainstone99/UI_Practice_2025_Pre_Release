using UnityEngine;

public class UiScript : MonoBehaviour //필드값 && 함수호출만
{
    private ButtonScript buttonScript = new();
    [SerializeField] private Inventory inventoryScript;
    [SerializeField] private GameObject inventoryTap;
    public void OnClickInventoryOpen()
    {
        buttonScript.OpenInventory(inventoryTap);
    }
    public void OnClickInventoryClose()
    {
        buttonScript.CloseInventory(inventoryTap);
    }
    public void OnClickAddItem(Item item)
    {
        inventoryScript.GainItem(item, 1);
    }
}
public class ButtonScript // 버튼으로 동작하는 단순 스크립트 모음
{
    public void OpenInventory(GameObject inventory)
    {
        inventory.SetActive(true);
    }
    public void CloseInventory(GameObject inventory)
    {
        inventory.SetActive(false);
    }

}
public class InventoryScript // 인벤토리 관련인데 분리해야 편할 듯
{
    
    //(레거시)폐기 예정 슬롯 스크립트랑 호환 안됨
    // public void AddItem(GameObject prefab, Transform position) // Monobehaviour를 상속받지 않으므로 기존의 Instantiate 사용이 안 됨
    // {
    //     if(position.childCount == 0)
    //     {
    //         GameObject newItem = UnityEngine.Object.Instantiate(prefab, position); // Vector2.zero 넣을 필요 없음
    //         RectTransform itemPosition = newItem.GetComponent<RectTransform>(); // 캔버스에 들어가는거라서 위치 재설정
    //         itemPosition.anchoredPosition = Vector2.zero; //좌표값 재조정
    //         itemPosition.localScale = Vector3.one; // 스케일 변형 방지
    //     }
    //     else
    //     {
    //         Debug.Log("이미 존재합니다.");
    //     }
    // }
}