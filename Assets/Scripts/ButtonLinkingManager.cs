using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLinkingManager : MonoBehaviour
{
    public List<AudioFilePlayer> _soundButtonAudioScripts;
    public LineRenderer lineRenderer;
    public Camera referenceCamera; 

    private int _currentIndex;
    
    public void StartLinkedAudio()
    {
        _currentIndex = 0;
        PlayLinkedAudio();
    }

    IEnumerator WaitForAudioToEnd(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        Debug.Log("Audio finished!");
        
        _soundButtonAudioScripts[_currentIndex].HandleAudioStopped();
        Debug.Log("Audio at index " + _currentIndex + " has ended");

        _currentIndex++;
        if (_currentIndex < _soundButtonAudioScripts.Count)
        {
            _soundButtonAudioScripts[_currentIndex].audioSource.Stop();
            Debug.Log("Now playing audio at index " + (_currentIndex));
            PlayLinkedAudio();
        }
    }

    private void PlayLinkedAudio()
    {
        //Check the first audio source in the list to see if it is still playing, then move to the next audio source and do the same
        if (_currentIndex != 0)
        {
            _soundButtonAudioScripts[_currentIndex].OnSelectButtonClicked();

        }
        StartCoroutine(WaitForAudioToEnd(_soundButtonAudioScripts[_currentIndex].audioSource));
    }

    public bool SendToButtonLinkingManager(AudioFilePlayer instance)
    {
        if (!_soundButtonAudioScripts.Contains(instance))
        {
            if (_soundButtonAudioScripts.Count == 0)
            {
                //This return value is so that the first button in the list will trigger the start of the link for subsequent buttons
                AddAudioFilePlayerToList(instance);
                UpdateLinePositions();
                return true;
            }
            AddAudioFilePlayerToList(instance);
            UpdateLinePositions();
            return false;

        }
        RemoveAudioFilePlayerFromList(instance);
        SetFirstButtonInListAsFirst();
        UpdateLinePositions();
        return false;
    }

    private void AddAudioFilePlayerToList(AudioFilePlayer instance)
    {
        _soundButtonAudioScripts.Add(instance);
    }

    private void RemoveAudioFilePlayerFromList(AudioFilePlayer instance)
    {
        _soundButtonAudioScripts.RemoveAt(_soundButtonAudioScripts.IndexOf(instance));
    }

    private void SetFirstButtonInListAsFirst()
    {
        //check to see if there are at least 2 elements before setting boolean
        if (_soundButtonAudioScripts.Count > 1)
        {
            _soundButtonAudioScripts[0].SetIsFirstInList(true);

        }
    }

    public void UpdateLinePositions()
    {
        lineRenderer.positionCount = _soundButtonAudioScripts.Count;
        
        for (int i = 0; i < _soundButtonAudioScripts.Count; i++)
        {
            //lineRenderer.SetPosition(i, _soundButtonAudioScripts[i].transform.position);
            
            // Get screen position of UI element
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, _soundButtonAudioScripts[i].transform.position);

            // Convert to world position at a fixed Z depth (e.g., in front of the camera)
            Vector3 worldPos = referenceCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 1.5f));
            
            //manually adjust for better button positioning
            worldPos.y += .3f;

            lineRenderer.SetPosition(i, worldPos);
        }

    }
    
}