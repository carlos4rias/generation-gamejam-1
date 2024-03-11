using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Diagnostics;

public class GameLogic : MonoBehaviour
{

    public static GameLogic Instance { get; private set; }


    private Vector2[] harvestingZones;
    private int howManyTrees;
    private int populatedWith;
    public GameObject aliveTree;

    private int[] howManyTreesCanGrowThere;
    private int[] howManyTreesGrowthThere;

    private void Awake() {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        howManyTrees = UnityEngine.Random.Range(30, 51);
        populatedWith = howManyTrees / 2;
        harvestingZones = new Vector2[] { 
            new(-6, 2), new(-3, -4),
            new(-1, -1), new(2, -5),
            new(14, -3), new(17, -8),
            new(22, 4), new(27, -2),
            new(7, 21), new(10, 16),
            new(16, 25), new(20, 22),
            new(25, 19), new(33, 14),

        };
        howManyTreesCanGrowThere = new int[7];
        howManyTreesGrowthThere = new int[7];
        PopulateZones();
    }

    private void PopulateZones() {
        for (int i = 0; i < harvestingZones.Length; i += 2) {
            Vector2 first = harvestingZones[i];
            Vector2 second = harvestingZones[i + 1];
            int howManyInThisZone = UnityEngine.Random.Range(1, Math.Min(5, populatedWith + 1));
            howManyTreesGrowthThere[i / 2] = howManyInThisZone;
            howManyTreesCanGrowThere[i / 2] = howManyInThisZone + UnityEngine.Random.Range(1, 5);
            populatedWith -= howManyInThisZone;

            for (int j = 0; j < howManyInThisZone; ++j) {
                float x = UnityEngine.Random.Range(first.x, second.x);
                float y = UnityEngine.Random.Range(second.y, first.y);
                Debug.Log($"{x}, {y}");
                Instantiate(aliveTree, new Vector3(x, y, 0),  Quaternion.identity);
            }
        }
    }

    private int checkInsideZones(float x, float y) {
        for (int i = 0; i < harvestingZones.Length; i += 2) {
            Vector2 first = harvestingZones[i];
            Vector2 second = harvestingZones[i + 1];
            if (x >= first.x && x <= second.x  && y >= second.y && y <= first.y)
                return i / 2;
        }
        return -1;
    }


    public int canHarvest(float x, float y) {
        Debug.Log($"harvest point {x}  {y}");
        int zone = checkInsideZones(x, y);
        if (zone == -1) return 0;

        Debug.Log($"harvest point zone {zone} already {howManyTreesGrowthThere[zone]} can  {howManyTreesCanGrowThere[zone]}");
        if (howManyTreesGrowthThere[zone] + 1 > howManyTreesCanGrowThere[zone]) return 1;
        howManyTreesGrowthThere[zone]++;
        
        Instantiate(aliveTree, new Vector3(x, y, 0),  Quaternion.identity);    
        return 2;
    }
 

}
