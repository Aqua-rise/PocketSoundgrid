using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/savedata.json";


    // Save entire list
    private static void SaveAllData(AllButtonSaveData allData)
    {
        string json = JsonUtility.ToJson(allData, true);
        File.WriteAllText(savePath, json);
        Debug.Log(savePath);
    }
    
    // Edit existing data (by buttonID)
    public static void EditButtonData(int buttonID, System.Action<ButtonSaveData> modifyCallback)
    {
        var allData = LoadAllData();
        int index = allData.savedButtons.FindIndex(d => d.buttonID == buttonID);
        if (index >= 0)
        {
            modifyCallback.Invoke(allData.savedButtons[index]);
            SaveAllData(allData);
        }
        else
        {
            Debug.LogWarning("Button ID not found for edit.");
        }
    }
    
    // Add a new ButtonSaveData
    public static void AddButtonData(ButtonSaveData newData)
    {
        var allData = LoadAllData();
        allData.savedButtons.Add(newData);
        SaveAllData(allData);
    }

    // Load all existing data
    public static AllButtonSaveData LoadAllData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<AllButtonSaveData>(json);
        }
        return new AllButtonSaveData { savedButtons = new List<ButtonSaveData>() };
    }
    
    // Remove data (by buttonID)
    public static void RemoveButtonData(int buttonID)
    {
        var allData = LoadAllData();
        allData.savedButtons.RemoveAll(d => d.buttonID == buttonID);
        SaveAllData(allData);
    }
    
    public static int GenerateNextButtonID()
    {
        var allData = LoadAllData();
        int maxID = -1;
        foreach (var button in allData.savedButtons)
        {
            if (button.buttonID > maxID)
                maxID = button.buttonID;
        }
        return maxID + 1;
    }

}
