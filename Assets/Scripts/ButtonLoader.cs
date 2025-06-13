using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoader : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonFolder;

    public void CreateNewButton(Vector3 spawnPosition)
    {
        // Instantiate the buttonPrefab at buttonSpawnPosition &
        // set the instantiated buttonPrefab to be the child of buttonFolder
        GameObject newButton = Instantiate(buttonPrefab, spawnPosition, Quaternion.identity);
        newButton.transform.SetParent(buttonFolder.transform, true);

        // Set the scale to 0.75
        newButton.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        
        int uniqueID = SaveSystem.GenerateNextButtonID();
        
        newButton.GetComponent<AudioFilePlayer>().SetButtonID(uniqueID);
        
        SaveDefaultButtonDetails(uniqueID, newButton.transform.localPosition.x, newButton.transform.localPosition.y, newButton.transform.localPosition.z);
        
        // Add a listener to the new button
        //_buttonModeManaging.AddListenerToButton(newButton);
    }

    private void Start()
    {
        LoadExistingButtons();
    }

    public void LoadExistingButtons()
    {
        var allData = SaveSystem.LoadAllData();
        
        foreach (var button in allData.savedButtons)
        {
            //Create a vector3 using the saved vectors
            Vector3 spawnPosition = new Vector3(button.posX, button.posY, button.posZ);
            
            //Create the button and set its parent to be the parent folder
            GameObject newButton = Instantiate(buttonPrefab, spawnPosition, Quaternion.identity);
            newButton.transform.SetParent(buttonFolder.transform, false);
            
            // Set the scale to 0.75
            newButton.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

            //Create variable for easy referencing
            AudioFilePlayer newButtonScript = newButton.GetComponent<AudioFilePlayer>();
            
            //Give the loaded button its ID
            newButtonScript.SetButtonID(button.buttonID);
            
            //Send the audioPath to the button's AudioFilePlayer
            newButtonScript.LoadSavedAudio(button.audioPath);
            
            //Send the buttonName to the button's AudioFilePlayer
            newButtonScript.LoadSavedName(button.buttonName);
  
        }
    }
    void SaveDefaultButtonDetails(int uniqueID, float x, float y, float z)
    {
        ButtonSaveData newButton = new ButtonSaveData
        {
            buttonID = uniqueID,
            buttonName = null,
            audioPath = null,
            posX = x,
            posY = y,
            posZ = z
        };
        SaveSystem.AddButtonData(newButton);
    }

    public void DeleteAllButtonsAndSaveData()
    {
        for (int i = 0; i < buttonFolder.transform.childCount; i++)
        {
            var child = buttonFolder.transform.GetChild(i).GetComponent<AudioFilePlayer>();
            SaveSystem.RemoveButtonData(child.GetButtonID());
            Destroy(child.gameObject);
        }
    }
}
