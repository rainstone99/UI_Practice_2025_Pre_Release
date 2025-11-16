using UnityEngine;

public class UiScript : MonoBehaviour
{
    private ButtonScript buttonScript = new();
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject itemPrefab;
    public void OnClickInventoryOpen()
    {
        buttonScript.OpenInventory(inventory);
    }
    public void OnClickInventoryClose()
    {
        buttonScript.CloseInventory(inventory);
    }
    public void OnClickAddItem()
    {
        buttonScript.AddItem(itemPrefab);
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
    public void AddItem(GameObject prefab)
    {

    }
}
public class ItemScript
{
    
}