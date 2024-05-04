using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomSpawner : MonoBehaviour
{
    public Transform[] noteBookSpawnPoints;
    private List<Transform> usedSpawnPoints = new List<Transform>();

    void Start()
    {
        foreach (var notebook in GameObject.FindGameObjectsWithTag("Notebook"))
        {
            MoveNotebookToRandomSpawnPoint(notebook.transform);
        }
    }

    void MoveNotebookToRandomSpawnPoint(Transform notebook)
    {
        int indexNum = Random.Range(0, noteBookSpawnPoints.Length);
        Transform spawnPoint = noteBookSpawnPoints[indexNum];

        // Check if the spawn point has already been used
        while (usedSpawnPoints.Contains(spawnPoint))
        {
            indexNum = Random.Range(0, noteBookSpawnPoints.Length);
            spawnPoint = noteBookSpawnPoints[indexNum];
        }

        // Move the notebook to the selected spawn point
        notebook.position = spawnPoint.position;
        usedSpawnPoints.Add(spawnPoint);
    }
}
