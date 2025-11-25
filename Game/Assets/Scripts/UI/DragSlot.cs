using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public static DragSlot instance;
    public Slot dragSlot;
    public Image dragImage;
    void Start()
    {
        instance = this;
    }
    //**레이캐스트 타겟 해제 해야 함 이미지만 제거해서는 안 됨
    // 이미지 받아오기
    public void DragSetImage(Image _itemImage)
    {
        dragImage.sprite = _itemImage.sprite;
        DragSetOpacity(1f);
    }
    // 이미지 투명화
    public void DragSetOpacity(float _opacity)
    {
        Color color = dragImage.color;
        color.a = _opacity;
        dragImage.color = color;
    }
}
