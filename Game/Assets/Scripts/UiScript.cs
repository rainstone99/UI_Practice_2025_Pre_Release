using UnityEngine;

public class UiScript : MonoBehaviour 
{
    private ButtonScript buttonScript = new();
    public InventoryScript inventoryScript = new();
    [SerializeField] private GameObject inventoryTap;
    [SerializeField] private GameObject itemPrefab;
    public void OnClickInventoryOpen()
    {
        buttonScript.OpenInventory(inventoryTap);
    }
    public void OnClickInventoryClose()
    {
        buttonScript.CloseInventory(inventoryTap);
    }
    public void OnClickAddItem()
    {
        inventoryScript.AddItem(itemPrefab);
    }
}
public class ButtonScript
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
public class InventoryScript
{
    public Item Item {get; set;}
    public int ItemCount {get; set;}
    
    public void AddItem(GameObject prefab) // Monobehaviour를 상속받지 않으므로 기존의 Instantiate 사용이 안 됨
    {
        GameObject newItem = Instantiate(prefab);
    }
}