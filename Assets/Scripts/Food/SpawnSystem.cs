using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public GameObject foodPrefab;
    public int maxCount;
    public int maxSpawnRadius = 200;
    public float spawnDelay = 1;
    List<GameObject> listFood;

    private void OnEnable()
    {
        const string FoodTag = "Food";
        listFood = new List<GameObject>();

        for(int i =0; i < maxCount; i++)
        {
            Spawn();
        } 
    }

    private void Update()
    {
        UpdateList(); 

        if(listFood.Count < maxCount)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Vector2 spR = Random.insideUnitSphere * maxSpawnRadius;
        Vector3 spawnPos = new Vector3(spR.x, 0, spR.y);

        GameObject food = Instantiate(foodPrefab, spawnPos, Quaternion.identity);
        listFood.Add(food);
    }
    void UpdateList()
    {
        foreach(GameObject food in listFood )
        {
            if(food == null)
            {
                listFood.Remove(food);
            }
        }
    }
}
