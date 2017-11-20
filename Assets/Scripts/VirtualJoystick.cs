using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImage;
    private Image joystickImage;

    public Vector3 InputDirection { set; get; }

    // Creating joystick logic

    private void Start()
    {
        bgImage = GetComponent<Image>();
        joystickImage = GetComponentInChildren<Image>();
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        // Moving the joystick
        Vector2 pos = Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform,
            ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImage.rectTransform.sizeDelta.y);

            float x = (bgImage.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (bgImage.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            InputDirection = new Vector3(x, 0, y);
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            joystickImage.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (bgImage.rectTransform.sizeDelta.x / 3),
                InputDirection.z * (bgImage.rectTransform.sizeDelta.y / 3));
        }

    }

    public virtual void OnPointerDown(PointerEventData ped)
    {

    }

    public virtual void OnPointerUp(PointerEventData ped)
    {

    }

}
