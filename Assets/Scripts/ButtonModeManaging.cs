using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonModeManaging : MonoBehaviour
{
    
    public UnityEvent musicModeButtonEvent;
    public UnityEvent moveModeButtonEvent;
    public UnityEvent editModeButtonEvent;
    public UnityEvent linkModeButtonEvent;
    public UnityEvent loopModeButtonEvent;
    public GameObject audioButtonFolder;

    public bool isInLoopMode;
    public bool isInLinkMode;

    
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
                linkModeButtonEvent.AddListener(button.LinkButtonPressed);
                loopModeButtonEvent.AddListener(button.LoopButtonPressed);
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
            linkModeButtonEvent.AddListener(button.LinkButtonPressed);
            loopModeButtonEvent.AddListener(button.LoopButtonPressed);
        }
        
    }

    
    public void TriggerMusicModeEvent()
    {
        musicModeButtonEvent.Invoke();
        isInLoopMode = false;
        isInLinkMode = false;
    }

    public void TriggerMoveModeEvent()
    {
        moveModeButtonEvent.Invoke();
        isInLoopMode = false;
        isInLinkMode = false;
    }
    
    public void TriggerEditModeEvent()
    {
        editModeButtonEvent.Invoke();
        isInLoopMode = false;
        isInLinkMode = false;

    }
    
    public void TriggerLinkModeEvent()
    {
        linkModeButtonEvent.Invoke();
        isInLoopMode = false;
        isInLinkMode = true;
    }
    
    public void TriggerLoopModeEvent()
    {
        loopModeButtonEvent.Invoke();
        isInLoopMode = true;
        isInLinkMode = false;
    }
}
