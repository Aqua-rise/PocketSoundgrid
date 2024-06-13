using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonModeManaging : MonoBehaviour
{
    
    public UnityEvent musicModeButtonEvent;
    public UnityEvent moveModeButtonEvent;
    public UnityEvent editModeButtonEvent;
    public GameObject audioButtonFolder;

    
    void Start()
    {
        if (musicModeButtonEvent == null)
            musicModeButtonEvent = new UnityEvent();
        
        if (moveModeButtonEvent == null)
            moveModeButtonEvent = new UnityEvent();
        
        if (editModeButtonEvent == null)
            editModeButtonEvent = new UnityEvent();
        
    }

    public void AddListenersToAllCurrentButtons()
    {
        // Get all ButtonPressDuration components in the children of the specified parent object
        if (audioButtonFolder != null)
        {
            ButtonPressDuration[] buttonPressDuration = audioButtonFolder.GetComponentsInChildren<ButtonPressDuration>();
            foreach (ButtonPressDuration button in buttonPressDuration)
            {
                musicModeButtonEvent.AddListener(button.MusicButtonPressed);
                moveModeButtonEvent.AddListener(button.MoveButtonPressed);
                editModeButtonEvent.AddListener(button.EditButtonPressed);
            }
        }
    }

    public void AddListenerToButton(GameObject soundButton)
    {
        
        ButtonPressDuration[] buttonPressDuration = soundButton.GetComponentsInChildren<ButtonPressDuration>();
        foreach (ButtonPressDuration button in buttonPressDuration)
        {
            musicModeButtonEvent.AddListener(button.MusicButtonPressed);
            moveModeButtonEvent.AddListener(button.MoveButtonPressed);
            editModeButtonEvent.AddListener(button.EditButtonPressed);
        }
        
    }

    
    public void TriggerMusicModeEvent()
    {
        musicModeButtonEvent.Invoke();
    }

    public void TriggerMoveModeEvent()
    {
        moveModeButtonEvent.Invoke();
    }
    
    public void TriggerEditModeEvent()
    {
        editModeButtonEvent.Invoke();
    }
}
