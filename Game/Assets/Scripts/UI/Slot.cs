using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class Slot : MonoBehaviour, 
IPointerClickHandler, 
IBeginDragHandler, 
IDragHandler, 
IEndDragHandler, 
IDropHandler
{
    public Item item; // 아이템 정보 불러오기
    public int itemCount; // 아이템 개수
    public int itemCountLimit;
    [HideInInspector] public bool isCountMax = false;
    public Image itemImage; // 아이템 이미지
    private Vector2 firstPos;
    public IObjectPool<GameObject> Pool { get; set; } // Pool 기능 사용을 위한 선언

    //받아올 오브젝트
    [SerializeField]
    private TextMeshProUGUI textCount; // 갯수 세는 텍스트
    [SerializeField]
    private GameObject isCounting; // 갯수가 존재하는지 여부
    void Start()
    {
        firstPos = transform.position;
    }

    // 아이템 추가 (원리는 오브젝트 활성화에 가까움)
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        isCounting.SetActive(true);
        textCount.text = itemCount.ToString();
        SetOpacity(1f); // 투명도값 1일 때 최대
        IsCountMax();
    }

    // 투명도 올리기 (해서 오브젝트 이미지 활성화처럼 보이기)
    private void SetOpacity(float _opacity)
    {
        Color color = itemImage.color;
        color.a = _opacity;
        itemImage.color = color;
    }

    // 중복템 개수 늘리기
    public void SetSlotCount(int _count)
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

    // 슬롯비우기
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetOpacity(0); // 투명도 값 0
        textCount.text = "0";
        isCounting.SetActive(false);
    }
    // 버리기 or 사용 등의 이유로 개수가 줄어들 때
    public void DecreaseItem()
    {
        string deleteItemName = item.itemName; // null로 전환시 아래 debug.log가 작동하지 않음
        // int deleteItemCount = itemCount; // 추후 복수 아이템 삭제 시 사용 예정
        if(itemCount > 1)
        {
            itemCount -= 1;
            textCount.text = itemCount.ToString();
            IsCountMax();
        }
        else
        {
            ClearSlot();
        }
        Debug.Log($"{deleteItemName} 아이템이 삭제되었습니다.");
    }

    // 현재 아이템의 개수가 최대치인지 (복수의 아이템을 얻는 경우, 추후 수정해야 함)
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
    // 클릭 시 아이템 이름 출력 (나중에 팝업Ui로 확장 가능)
    public void OnClickItemInfo()
    {
        if(item != null)
        Debug.Log($"이름 :{item.itemName} 개수 :{itemCount}"); // 이것만 있으면 이름이 null 일 시 오류
        else
        Debug.Log("아이템이 없습니다."); // 없으면 빈칸 클릭 시 오류
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left) // 마우스왼쪽 클릭 시 실행
        {
            OnClickItemInfo();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.DragSetOpacity(0f);
        DragSlot.instance.dragSlot = null;
    }

    // 드랍되었을 때 드랍받은 곳이 호출
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }
    public void ChangeSlot()
    {
        Item temp = item; // temp에 드랍받은 위치의 데이터가 대입됨 B
        int tempCount = itemCount;
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount); // B에 A가 대입
        if (temp != null) // B에 아이템이 있다면
        {
            DragSlot.instance.dragSlot.AddItem(temp, tempCount); // 이전의 슬롯 A에 B 대입
        }
        else // 없었다면
        {
            DragSlot.instance.dragSlot.ClearSlot(); // 슬롯 지우기
        }
    }
}
