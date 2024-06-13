using UnityEngine;
using UnityEngine.Events;


public class ResetAllPathData : MonoBehaviour
{
    
    public UnityEvent resetAllFilePaths;
    public GameObject audioButtonFolder;

    
    void Start()
    {
        if (resetAllFilePaths == null)
            resetAllFilePaths = new UnityEvent();
        // Get all AudioFilePlayer components in the children of the specified parent object
        if (audioButtonFolder != null)
        {
            AudioFilePlayer[] audioFilePlayers = audioButtonFolder.GetComponentsInChildren<AudioFilePlayer>();
            foreach (AudioFilePlayer player in audioFilePlayers)
            {
                resetAllFilePaths.AddListener(player.TriggeredByExternalEvent);
            }
        }
    }

    // This method can be called to trigger the event
    public void TriggerResetAllFilePaths()
    {
        resetAllFilePaths.Invoke();
    }
}
