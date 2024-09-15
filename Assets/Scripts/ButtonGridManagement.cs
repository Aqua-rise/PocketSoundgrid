using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGridManagement : MonoBehaviour
{
    public List<Vector3> AvaliablePositions = new List<Vector3>();
    public List<bool> AvaliablePositionsOccupied = new List<bool>();

    private int column1Value;
    private int column2Value;
    private int column3Value;
    private int column4Value;
    private int column5Value;
    private int rowStartingValue = 400;
    private int rowDecreaseValue = 300;
    


    public void RunMethods()
    {
        ChangeGridType(5);
        AvailablePositionListColumnInstantiation();
    } 

    public void ChangeGridType(int numColumns)
    {
        switch(numColumns)
        {
            case 2:
                PlayerPrefs.SetInt("NumberOfColumnsKey", 2);
                column1Value = -200;
                column2Value = 200;
                break;
            case 3:
                PlayerPrefs.SetInt("NumberOfColumnsKey", 3);
                column1Value = -250;
                column2Value = 0;
                column3Value = 250;
                break;
            case 4:
                PlayerPrefs.SetInt("NumberOfColumnsKey", 4);
                column1Value = -300;
                column2Value = -50;
                column3Value = 50;
                column4Value = 300;
                break;
            case 5:
                PlayerPrefs.SetInt("NumberOfColumnsKey", 5);
                column1Value = -350;
                column2Value = -175;
                column3Value = 0;
                column4Value = 175;
                column5Value = 350;
                break;
            default:
                break;
        }
    }
    
    public void AvailablePositionListColumnInstantiation()
    {
        AvaliablePositions.Clear();
        AvaliablePositionsOccupied.Clear();
        int numButtons = PlayerPrefs.GetInt("ButtonCount", 0);
        int iterations = 0;
        switch (PlayerPrefs.GetInt("NumberOfColumnsKey", 0))
        {
            case 2:
                while (iterations < numButtons)
                {
                    // For two columns, the x and y positions are (-200, y) and (200, y), where y decreases by itself every row
                    // The available positions list will be filled with specific x and y values by the number of buttons

                    AvaliablePositions.Add(new Vector3(column1Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column2Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);

                    iterations++;
                }

                break;
            case 3:
                while (iterations < numButtons)
                {
                    // For three columns, the x and y positions are (-250, y) and (0, y) and (250, y), where y decreases by itself every row
                    // The available positions list will be filled with specific x and y values by the number of buttons

                    AvaliablePositions.Add(new Vector3(column1Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column2Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column3Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    iterations++;
                }

                break;
            case 4:
                while (iterations < numButtons)
                {
                    // For four columns, the x and y positions are (-300, y) and (-50, y) and (50, y) and (300, y), where y decreases by itself every row
                    // The available positions list will be filled with specific x and y values by the number of buttons
                    
                    AvaliablePositions.Add(new Vector3(column1Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column2Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column3Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column4Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    iterations++;
                }

                break;
            case 5:
                // For five columns, the x and y positions are (-350, y) and (-100, y) and (0, y) and (100, y) and (350, y), where y decreases by itself every row
                // The available positions list will be filled with specific x and y values by the number of buttons
                while (iterations < numButtons)
                {
                    AvaliablePositions.Add(new Vector3(column1Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column2Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column3Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column4Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    AvaliablePositions.Add(new Vector3(column5Value, rowStartingValue + (rowDecreaseValue * -iterations), 0));
                    AvaliablePositionsOccupied.Add(false);
                    iterations++;
                }

                break;
            default:
                break;
        }

    }
}
