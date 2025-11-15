using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    private TextScript textScript = new();
    private ButtonScript buttonScript = new();
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject itemPrefab;
    public void InventoryOpen()
    {
        textScript.OnClickOpenInventory(inventory);
    }
    public void InventoryClose()
    {
        textScript.OnClickCloseInventory(inventory);
    }
    public void AddItem()
    {
        buttonScript.OnClickAddItem(itemPrefab);
    }
}
public class TextScript
{
    public void OnClickOpenInventory(GameObject inventory)
    {
        inventory.SetActive(true);
    }
    public void OnClickCloseInventory(GameObject inventory)
    {
        inventory.SetActive(false);
    }
}
public class ButtonScript
{
    public void OnClickAddItem(GameObject prefab)
    {

    }
}
public class ImageScript
{
    
}