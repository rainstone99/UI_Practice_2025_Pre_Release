using UnityEngine;

public class UiScript : MonoBehaviour //필드값 && 함수호출만
{
    private ButtonScript buttonScript = new();
    private InventoryButtonScript inventoryButtonScript = new();
    [SerializeField] private Inventory inventoryScript;

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
        inventoryScript.IncreaseItem(item, 1);
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
   
}