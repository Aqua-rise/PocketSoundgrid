using UnityEngine;

public class ButtonModeManaging : MonoBehaviour
{

    public bool isInMusicMode = true;
    public bool isInMoveMode;
    public bool isInEditMode;
    public bool isInLoopMode;
    public bool isInLinkMode;

    public void TriggerMusicMode()
    {
        isInMusicMode = true;
        isInMoveMode = false;
        isInEditMode = false;
        isInLoopMode = false;
        isInLinkMode = false;
    }

    public void TriggerMoveMode()
    {
        isInMusicMode = false;
        isInMoveMode = true;
        isInEditMode = false;
        isInLoopMode = false;
        isInLinkMode = false;
    }
    
    public void TriggerEditMode()
    {
        isInMusicMode = false;
        isInMoveMode = false;
        isInEditMode = true;
        isInLoopMode = false;
        isInLinkMode = false;
    }
    
    public void TriggerLinkMode()
    {
        isInMusicMode = false;
        isInMoveMode = false;
        isInEditMode = false;
        isInLoopMode = false;
        isInLinkMode = true;
    }
    
    public void TriggerLoopMode()
    {
        isInMusicMode = false;
        isInMoveMode = false;
        isInEditMode = false;
        isInLoopMode = true;
        isInLinkMode = false;
    }

    public bool GetIsInMusicMode()
    {
        return isInMusicMode;
    }
    
    public bool GetIsInMoveMode()
    {
        return isInMoveMode;
    }
    
    public bool GetIsInEditMode()
    {
        return isInEditMode;
    }
    
    public bool GetIsInLinkMode()
    {
        return isInLinkMode;
    }
    
    public bool GetIsInLoopMode()
    {
        return isInLoopMode;
    }
}
