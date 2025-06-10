using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AudioFilePlayer : MonoBehaviour
{
    private ButtonPressDuration _buttonPressDuration;
    private CreateNewButton _createNewButton;
    private ButtonModeManaging _buttonModeManaging;
    private ButtonLinkingManager _buttonLinkingManager;

    public Button selectButton;
    private GameObject _selectButtonGameObject;
    private Image _selectButtonImageComponent;
    public AudioSource audioSource; 
    private string _audioFilePath;
    private static int _buttonIDCounter = 0;
    private int buttonID;
    private int buttonSaveDataIndex;
    private const string AudioPathKey = "AudioPath_";
    private bool triggerResetAudio = true;
    private bool buttonManuallyPressed = false;
    private bool _isFirstInLink;

    public Sprite hasAudioSprite;
    public Sprite playingAudioSprite;
    public Sprite pausedAudioSprite;
    public Sprite defaultSprite;

    public TMP_Dropdown buttonOptions;

    void Awake()
    {
        buttonID = ++_buttonIDCounter; // Assign a unique ID

        // Get the ButtonPressDuration component from the first child of this script's gameobject
        _buttonPressDuration = transform.gameObject.GetComponentsInChildren<ButtonPressDuration>()[0];

        // Get the CreateNewButton component from the CreateNewButton gameobject
        _createNewButton = GameObject.Find("CreateNewButtonManager").GetComponent<CreateNewButton>();
        
        // Get the ButtonModeManaging component from the ButtonModeManager gameobject
        _buttonModeManaging = GameObject.Find("ButtonModeManager").GetComponent<ButtonModeManaging>();
        
        // Get the ButtonLinkManager component from the ButtonLinkManager gameobject
        _buttonLinkingManager = GameObject.Find("ButtonLinkManager").GetComponent<ButtonLinkingManager>();
    }

    void Start()
    {
        _selectButtonGameObject = selectButton.gameObject;
        _selectButtonImageComponent = _selectButtonGameObject.GetComponent<Image>();

        if (buttonOptions != null)
        {
            buttonOptions.onValueChanged.AddListener(HandleDropdownSelection);
            buttonOptions.gameObject.SetActive(false);
        }

        // Load the saved audio file path if it exists
        if (PlayerPrefs.HasKey(AudioPathKey + buttonID))
        {
            _audioFilePath = PlayerPrefs.GetString(AudioPathKey + buttonID);
            StartCoroutine(LoadAudio());
        }

        // Set the buttonSaveDataIndex to the numerical position of this button as a child of this object's parent
        buttonSaveDataIndex = transform.GetSiblingIndex();
        Debug.Log("ButtonSaveDataIndex: " + buttonSaveDataIndex);
    }

    void Update()
    {
        // Check if the audio has stopped playing
        if (!audioSource.isPlaying && audioSource.clip != null && !buttonManuallyPressed)
        {
            HandleAudioStopped();
        }
        
        if (audioSource.isPlaying && !triggerResetAudio)
        {
            triggerResetAudio = true;
        }
    }

    public void OnSelectButtonClicked()
    {
        if (_buttonModeManaging.isInLinkMode)
        {
            _isFirstInLink = _buttonLinkingManager.SendToButtonLinkingManager(this);
            return;
        }
        if (string.IsNullOrEmpty(_audioFilePath))
        {
            StartCoroutine(OpenFileBrowser());
        }
        else
        {
            if (audioSource.isPlaying)
            {
                if (_buttonModeManaging.isInLoopMode)
                {
                    audioSource.loop = true;
                    return;
                }
                // Set button to pausedAudio Sprite
                _selectButtonImageComponent.sprite = pausedAudioSprite;
                SetButtonManuallyPressed(true);
                audioSource.Pause();
            }
            else
            {
                // Set button to playingAudioSprite
                _selectButtonImageComponent.sprite = playingAudioSprite;
                SetButtonManuallyPressed(false);
                audioSource.loop = _buttonModeManaging.isInLoopMode;
                audioSource.Play();
                if (_isFirstInLink)
                {
                    _buttonLinkingManager.StartLinkedAudio();
                    return;
                }
            }
        }
    }
    private void HandleDropdownSelection(int index)
    {
        switch (index)
        {
            case 1: // Restart Audio
                audioSource.Stop();
                _selectButtonImageComponent.sprite = hasAudioSprite;
                break;
            case 2: // Select Another Audio File
                StartCoroutine(OpenFileBrowser());
                _selectButtonImageComponent.sprite = hasAudioSprite;
                break;
            case 3: // Delete Audio File
                _audioFilePath = string.Empty;
                PlayerPrefs.DeleteKey(AudioPathKey + buttonID);
                _selectButtonImageComponent.sprite = defaultSprite;
                break;
            case 4: // Remove Button, components, and save data
                _audioFilePath = string.Empty; // Set the audio file path to an empty string
                _buttonIDCounter -= 1; // Decrement the button ID counter
                PlayerPrefs.DeleteKey(AudioPathKey + buttonID); // Delete the audio file path from the player prefs
                _buttonPressDuration.DeleteButtonSaveData(); // Call method to delete button position data
                _createNewButton.DeleteButtonData(buttonSaveDataIndex); // Call method to delete button load data using the position this object is at in as a child of its parent
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    public void HandleAudioStopped()
    {
        //Debug.Log("Attempted to reset audio for button : " + buttonID);
        if (!triggerResetAudio) return;
        
        Debug.Log("Detected Audio ended for button : " + buttonID);
        // Reset button appearance
        _selectButtonImageComponent.sprite = hasAudioSprite;
        // Reset the triggerResetAudio flag
        triggerResetAudio = false;
    }

    IEnumerator OpenFileBrowser()
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

    // Public method to be triggered by another script
    public void TriggeredByExternalEvent()
    {
        Debug.Log("All audio paths removed.");
        _audioFilePath = string.Empty;
        PlayerPrefs.DeleteKey(AudioPathKey + buttonID);
        _selectButtonImageComponent.sprite = defaultSprite;
    }

    public void SetButtonManuallyPressed(bool value)
    {
        buttonManuallyPressed = value;
        Debug.Log("Button pressed set to " + value);
    }

    public void SetIsFirstInList(bool value)
    {
        _isFirstInLink = value;
    }
}
