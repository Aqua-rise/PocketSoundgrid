using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonCreationMenu : MonoBehaviour
{
    
    /*IEnumerator OpenFileBrowser()
    {
#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.OpenFilePanel("Select Audio File", "", "mp3,ogg,wav");
        if (!string.IsNullOrEmpty(path))
        {
            _audioFilePath = "file://" + path;
            yield return StartCoroutine(LoadAudio());
        }
#elif UNITY_ANDROID || UNITY_IOS
        NativeGallery.Permission permission = NativeGallery.GetAudioFromGallery((path) =>
        {
            if (!string.IsNullOrEmpty(path))
            {
                _audioFilePath = "file://" + path;
                StartCoroutine(LoadAudio());
            }
        }, "Select Audio File", "audio/*");

        if (permission != NativeGallery.Permission.Granted)
        {
            Debug.Log("Permission to access the gallery was not granted.");
        }
        yield return null;
#else
        Debug.Log("File selection is not implemented for this platform.");
#endif
    }
    
    private IEnumerator LoadAudio()
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(_audioFilePath, AudioType.UNKNOWN);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = clip;
            // Set button to hasAudio Sprite
            _selectButtonImageComponent.sprite = hasAudioSprite;

            // Save the path to the audio file
            PlayerPrefs.SetString(AudioPathKey + buttonID, _audioFilePath);

            Debug.Log("Audio file loaded successfully.");
        }
    }
*/
    
}
