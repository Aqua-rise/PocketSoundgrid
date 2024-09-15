using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPressDuration : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Button button;
    private float longPressDuration = 0f; // Duration in seconds for a long press (to prevent errors)
    private bool isPointerDown = false;
    private bool isDragging = false;
    private float pointerDownTimer = 0;
    private RectTransform buttonParentRectTransform;
    private Vector2 originalPosition;
    
    private static int _buttonMovementIDCounter = 0;
    private int _buttonMovementID;

    private bool isInMusicMode = true;
    private bool isInMoveMode;
    private bool isInEditMode;

    private TMP_Dropdown optionsMenu;
    private TMP_InputField _buttonNameInputField;

    private bool pointerDownCheck;

    void Awake()
    {       
        //Get the optionsMenu dropdown from the fellow children of this Gameobject's parent
        optionsMenu = button.transform.parent.GetComponentsInChildren<TMP_Dropdown>()[0];
             
        _buttonMovementID = ++_buttonMovementIDCounter; // Assign a unique ID
        buttonParentRectTransform = button.transform.parent.GetComponent<RectTransform>();
        
        // Get the _buttonNameInputField component from the first child of this script's gameobject
        _buttonNameInputField = transform.gameObject.GetComponentsInChildren<TMP_InputField>()[0];
        
        if (_buttonNameInputField == null)
        {
            Debug.LogError("Could not find _buttonNameInputField component");
        }
        
        LoadPosition();
        
    }
    


    void Update()
    {
        if (isPointerDown && !isDragging && isInMoveMode)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= longPressDuration)
            {
                
                SetIsDragging(true);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        isPointerDown = true;
        pointerDownTimer = 0;
        if (isInEditMode)
        {
            //Make the button un-interactable
            button.interactable = false;
            if (optionsMenu.gameObject.activeSelf)
                optionsMenu.gameObject.SetActive(false);
            else
                optionsMenu.gameObject.SetActive(true);
            

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging)
        {
            Reset();
            button.interactable = true;
        }
    }

    void SetIsDragging(bool value)
    {
        isDragging = value;
        originalPosition = buttonParentRectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            button.interactable = false;
                int siblingIndex = buttonParentRectTransform.GetSiblingIndex();
                Debug.Log("Sibling index: " + siblingIndex);
                PlayerPrefs.SetInt(_buttonMovementID + "_siblingIndex", siblingIndex);
                PlayerPrefs.Save();
                            
                buttonParentRectTransform.SetAsLastSibling(); // Bring the button to the front
                
                // Set pointerDownCheck to true
                pointerDownCheck = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(buttonParentRectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        
            // Round the position to the nearest 100
            localPoint.x = Mathf.Round(localPoint.x / 25) * 25;
            localPoint.y = Mathf.Round(localPoint.y / 25) * 25;
        
            buttonParentRectTransform.anchoredPosition = localPoint;
        }
    }




    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {

            if (pointerDownCheck)
            {
                int originalSiblingIndex = PlayerPrefs.GetInt(_buttonMovementID + "_siblingIndex");
                                            
                buttonParentRectTransform.SetSiblingIndex(originalSiblingIndex);
                Debug.Log("Set sibling index to: " + originalSiblingIndex);
                                            
                PlayerPrefs.DeleteKey(_buttonMovementID + "_siblingIndex");
                PlayerPrefs.Save();
            }

            pointerDownCheck = false;
            isDragging = false;
            button.interactable = true;
            SavePosition();
            Reset();
        }
    }

    void SavePosition()
    {
        
        PlayerPrefs.SetFloat(_buttonMovementID + "_x", buttonParentRectTransform.anchoredPosition.x);
        PlayerPrefs.SetFloat(_buttonMovementID + "_y", buttonParentRectTransform.anchoredPosition.y);
        PlayerPrefs.Save();
    }

    void LoadPosition()
    {
        
        Debug.Log("Loading position for button count: " + _buttonMovementID);

        if (PlayerPrefs.HasKey(_buttonMovementID + "_x") && PlayerPrefs.HasKey(_buttonMovementID + "_y"))
        {
            float x = PlayerPrefs.GetFloat(_buttonMovementID + "_x");
            float y = PlayerPrefs.GetFloat(_buttonMovementID + "_y");

            Debug.Log("Retrieved position x: " + x + " y: " + y);

            buttonParentRectTransform.anchoredPosition = new Vector2(x, y);
        }
        else
        {
            Debug.LogWarning("Position data not found for button count: " + _buttonMovementID);
        }
    
        // Additionally add the name from the prefab to the _buttonNameInputField
        if (PlayerPrefs.HasKey(_buttonMovementID + "_name"))
        {
            string buttonName = PlayerPrefs.GetString(_buttonMovementID + "_name");
            _buttonNameInputField.text = buttonName;

            Debug.Log("Setting button name to: " + buttonName);
        }
        else
        {
            Debug.LogWarning("Button name data not found.");
        }
        
    }

    public void OnButtonInputFieldValueChanged()
    {

        
        // Save the text input from the _buttonNameInputField to the PlayerPrefs
        var buttonName = _buttonNameInputField.text;
        PlayerPrefs.SetString(_buttonMovementID + "_name", buttonName);
        PlayerPrefs.Save();
    }

    void Reset()
    {
        isPointerDown = false;
        pointerDownTimer = 0;
    }

    public void DeleteButtonSaveData()
    {
        PlayerPrefs.DeleteKey(_buttonMovementID + "_x");
        PlayerPrefs.DeleteKey(_buttonMovementID + "_y");
        PlayerPrefs.DeleteKey(_buttonMovementID + "_name");
        PlayerPrefs.Save();
    }

    public void MusicButtonPressed()
    {
        isInMusicMode = true;
        isInMoveMode = false;
        isInEditMode = false;
    }
    
    public void MoveButtonPressed()
    {
        isInMusicMode = false;
        isInMoveMode = true;
        isInEditMode = false;
    }
    
    public void EditButtonPressed()
    {
        isInMusicMode = false;
        isInMoveMode = false;
        isInEditMode = true;
    }
    
    
}
