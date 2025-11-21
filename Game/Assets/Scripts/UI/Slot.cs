using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Item item; // 아이템 정보 불러오기
    public int itemCount; // 아이템 개수
    public int itemCountLimit;
    public bool isCountMax = false;
    public Image itemImage; // 아이템 이미지

    //받아올 오브젝트
    [SerializeField]
    private TextMeshProUGUI textCount; // 갯수 세는 텍스트
    [SerializeField]
    private GameObject isCounting; // 갯수가 존재하는지 여부

    public void AddItem(Item _item, int _count = 1) // 아이템 추가 (원리는 오브젝트 활성화에 가까움)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        isCounting.SetActive(true);
        textCount.text = itemCount.ToString();
        SetOpacity(1f); // 투명도값 1일 때 최대
        IsCountMax();
    }

    private void SetOpacity(float _opacity) // 투명도 올리기 (해서 오브젝트 이미지 활성화처럼 보이기)
    {
        Color color = itemImage.color;
        color.a = _opacity;
        itemImage.color = color;
    }
    public void SetSlotCount(int _count) // 중복템 개수 늘리기
    {
        itemCount += _count;
        textCount.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
        else
        {
            IsCountMax();
        }
    }
    private void ClearSlot() // 슬롯비우기
    {
        item = null;
        itemCount = 0;
        itemImage = null;
        SetOpacity(0); // 투명도 값 0
        textCount.text = "0";
        isCounting.SetActive(false);
    }
    private void IsCountMax()
    {
        if(itemCount >= itemCountLimit)
        {
            isCountMax = true;
        }
        else
        {
            isCountMax = false;
        }
    }
}
