using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayButtonScript : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    public GameObject background;
    private Canvas canvas;
    private Vector2 originalPosition;
    private Vector2 pointerOffset;

    public CreateNewButton createNewButton;
    public ButtonLoader buttonLoader;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("DraggableUI must be a child of a Canvas.");
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pointerOffset
        );

        pointerOffset = rectTransform.anchoredPosition - pointerOffset;
    }

    
    
    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        Vector2 pointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pointerPosition
        );

        rectTransform.anchoredPosition = pointerPosition + pointerOffset;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        // Create a new button at the current position on release
        //createNewButton.InstantiateButtonPrefabAtLocation(rectTransform.position);
        buttonLoader.CreateNewButton(rectTransform.position);
        
        rectTransform.anchoredPosition = originalPosition; 
        
    }
}
