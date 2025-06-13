using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonSaveData
{
    public int buttonID;
    public string buttonName;
    public string audioPath;
    public float posX;
    public float posY;
    public float posZ;
}

[System.Serializable]
public class AllButtonSaveData
{
    public List<ButtonSaveData> savedButtons;
}
