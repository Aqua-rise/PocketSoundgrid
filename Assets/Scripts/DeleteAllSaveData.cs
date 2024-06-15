using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAllSaveData : MonoBehaviour
{
    public GameObject audioButtonFolder;


    //Delete all player pref data
    public void DeleteAllPreferenceData()
    {
        PlayerPrefs.DeleteAll();
        
        // Delete all the button children in the audioButtonFolder
        if (audioButtonFolder != null)
        {
            foreach (Transform child in audioButtonFolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
    }
}
