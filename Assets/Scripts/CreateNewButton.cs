using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CreateNewButton : MonoBehaviour
{
    private ButtonModeManaging _buttonModeManaging;
    private ButtonGridManagement _buttonGridManagement;
    public GameObject buttonPrefab;
    public Vector3 buttonSpawnPosition;
    public GameObject buttonFolder;

    private const string ButtonCountKey = "ButtonCount";
    private const string ButtonPositionKey = "ButtonPosition_";
    private const string ButtonScaleKey = "ButtonScale_";

    public bool autoManageGrid;
    
    
    void Start()
    {

        if (autoManageGrid)
        {
            // Get the ButtonGridManagement component from the ButtonGridManager gameobject
            _buttonGridManagement = GameObject.Find("ButtonGridManager")?.GetComponent<ButtonGridManagement>();
                    
            // Run the RunMethods method
            if (_buttonGridManagement != null) _buttonGridManagement.RunMethods();
        }
        
        // Load the saved button details
        LoadButtons();
        
        // Get the ButtonModeManaging component from the ButtonModeManager gameobject
        _buttonModeManaging = GameObject.Find("ButtonModeManager")?.GetComponent<ButtonModeManaging>();
        
        // Add Listeners to all current buttons
        if (_buttonModeManaging != null) _buttonModeManaging.AddListenersToAllCurrentButtons();
    }
    
    public void InstantiateButtonPrefabAtLocation(Vector3 spawnPosition)
    {
        // Instantiate the buttonPrefab at buttonSpawnPosition &
        // set the instantiated buttonPrefab to be the child of buttonFolder
        GameObject newButton = Instantiate(buttonPrefab, spawnPosition, Quaternion.identity);
        newButton.transform.SetParent(buttonFolder.transform, true);

        // Set the scale to 0.75
        newButton.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        
        // Save the new button details
        SaveButton(newButton);
        newButton.GetComponentInChildren<ButtonPressDuration>().SavePosition();
        
        // Add a listener to the new button
        _buttonModeManaging.AddListenerToButton(newButton);
    }

    private void SaveButton(GameObject button)
    {
        int buttonCount = PlayerPrefs.GetInt(ButtonCountKey, 0);
        PlayerPrefs.SetFloat(ButtonPositionKey + buttonCount + "_x", button.transform.localPosition.x);
        PlayerPrefs.SetFloat(ButtonPositionKey + buttonCount + "_y", button.transform.localPosition.y);
        PlayerPrefs.SetFloat(ButtonPositionKey + buttonCount + "_z", button.transform.localPosition.z);
        PlayerPrefs.SetFloat(ButtonScaleKey + buttonCount, button.transform.localScale.x); // Assuming uniform scale
        PlayerPrefs.SetInt(ButtonCountKey, buttonCount + 1);
        PlayerPrefs.Save();
    }
    
    private void LoadButtons()
    {
        int buttonCount = PlayerPrefs.GetInt(ButtonCountKey, 0);

        for (int i = 0; i < buttonCount; i++)
        {
            /*float x = PlayerPrefs.GetFloat(ButtonPositionKey + i + "_x");
            float y = PlayerPrefs.GetFloat(ButtonPositionKey + i + "_y");
            float z = PlayerPrefs.GetFloat(ButtonPositionKey + i + "_z");*/
            float scale = PlayerPrefs.GetFloat(ButtonScaleKey + i);

            GameObject newButton = Instantiate(buttonPrefab, buttonSpawnPosition, Quaternion.identity);
            newButton.transform.SetParent(buttonFolder.transform, false);
            
            newButton.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    
    public void SaveAllButtonPositions()
    {
        int buttonCount = buttonFolder.transform.childCount;
        
        for (int i = 0; i < buttonCount; i++)
        {
            Transform child = buttonFolder.transform.GetChild(i);
            PlayerPrefs.SetFloat(ButtonPositionKey + i + "_x", child.localPosition.x);
            PlayerPrefs.SetFloat(ButtonPositionKey + i + "_y", child.localPosition.y);
            PlayerPrefs.SetFloat(ButtonPositionKey + i + "_z", child.localPosition.z);
            PlayerPrefs.SetFloat(ButtonScaleKey + i, child.localScale.x); // Assuming uniform scale
        }

        PlayerPrefs.SetInt(ButtonCountKey, buttonCount);
        Debug.Log("Current buttonCountKey value: " + PlayerPrefs.GetInt(ButtonCountKey, 0));
        PlayerPrefs.Save();
    }
    
    public void DeleteButtonData(int buttonIndex)
    {
        PlayerPrefs.DeleteKey(ButtonPositionKey + buttonIndex + "_x");
        PlayerPrefs.DeleteKey(ButtonPositionKey + buttonIndex + "_y");
        PlayerPrefs.DeleteKey(ButtonPositionKey + buttonIndex + "_z");
        PlayerPrefs.DeleteKey(ButtonScaleKey + buttonIndex);

        // Get button count
        int buttonCount = PlayerPrefs.GetInt(ButtonCountKey, 0);
        if (buttonIndex < buttonCount)
        {
            for (int i = buttonIndex; i < buttonCount - 1; i++)
            {
                // Move the data from the next button to the current position
                PlayerPrefs.SetFloat(ButtonPositionKey + i + "_x", PlayerPrefs.GetFloat(ButtonPositionKey + (i + 1) + "_x"));
                PlayerPrefs.SetFloat(ButtonPositionKey + i + "_y", PlayerPrefs.GetFloat(ButtonPositionKey + (i + 1) + "_y"));
                PlayerPrefs.SetFloat(ButtonPositionKey + i + "_z", PlayerPrefs.GetFloat(ButtonPositionKey + (i + 1) + "_z"));
                PlayerPrefs.SetFloat(ButtonScaleKey + i, PlayerPrefs.GetFloat(ButtonScaleKey + (i + 1)));
            }

            // Remove the last button data
            PlayerPrefs.DeleteKey(ButtonPositionKey + (buttonCount - 1) + "_x");
            PlayerPrefs.DeleteKey(ButtonPositionKey + (buttonCount - 1) + "_y");
            PlayerPrefs.DeleteKey(ButtonPositionKey + (buttonCount - 1) + "_z");
            PlayerPrefs.DeleteKey(ButtonScaleKey + (buttonCount - 1));
            
        }

        PlayerPrefs.SetInt(ButtonCountKey, buttonCount - 1);        
        PlayerPrefs.Save();
    }
    
    
}
