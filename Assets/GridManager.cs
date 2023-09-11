using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab; // Reference to the cube prefab

    private CellularAutomata cellularAutomaton; // Reference to your CellularAutomaton3D script

    void Start()
    {
        cellularAutomaton = GetComponent<CellularAutomata>();
        CreateGrid();
    }

    public float updateInterval = 1.0f; // Update every 1 second
    private float timeSinceLastUpdate = 0.0f;

    void Update()
    {
        // Accumulate time
        timeSinceLastUpdate += Time.deltaTime;

        // Check if it's time to update the grid
        if (timeSinceLastUpdate >= updateInterval)
        {
            // Update the grid logic
            cellularAutomaton.UpdateGrid();
         
             // Destroy any existing objects in the scene
            DestroyGridObjects();
            
            
            CreateGrid();

           

            // Reset the time counter
            timeSinceLastUpdate = 0.0f;
        }
    }

    void DestroyGridObjects()
    {
        // Find all objects with a specific tag (or by some other means) that you want to delete.
        GameObject[] gridObjects = GameObject.FindGameObjectsWithTag("Cube");

        // Iterate through the array and destroy each object.
        foreach (GameObject obj in gridObjects)
        {
            Destroy(obj);
        }
    }

    void CreateGrid()
    {
        for (int x = 0; x < cellularAutomaton.width; x++)
        {
            for (int y = 0; y < cellularAutomaton.height; y++)
            {
                for (int z = 0; z < cellularAutomaton.depth; z++)
                {
                    int cellState = cellularAutomaton.grid[x, y, z];
                    Vector3 cellPosition = new Vector3(x, y, z);

                    // Instantiate a cube at the cell's position
                    GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);

                    // Set the cube's color based on the cell's state
                    if (cellState == 1)
                    {
                        cell.GetComponent<Renderer>().material.color = Color.green; // Example color for alive cells
                    }
                    else
                    {
                        cell.GetComponent<Renderer>().material.color = Color.red; // Example color for dead cells
                    }
                }
            }
        }
    }
}
