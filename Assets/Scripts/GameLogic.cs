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
    public int howManyTrees;
    private int populatedWith;
    public GameObject aliveTree;

    private int[] howManyTreesCanGrowThere;
    private int[] howManyTreesGrowthThere;

    private int[] aliveTrees;
    private int gameState = 2;
    public int points = 0;
    public int liveTrees = 0;

    private void Awake() {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        howManyTrees = UnityEngine.Random.Range(30, 37);
        populatedWith = howManyTrees - 7;
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
        aliveTrees = new int[7];
        PopulateZones();
    }

    private void PopulateZones() {
        for (int i = 0; i < harvestingZones.Length; i += 2) {
            Vector2 first = harvestingZones[i];
            Vector2 second = harvestingZones[i + 1];
            int howManyInThisZone = UnityEngine.Random.Range(2, Math.Min(5, populatedWith + 1));
            howManyTreesGrowthThere[i / 2] = howManyInThisZone;
            aliveTrees[i / 2] = howManyInThisZone;
            liveTrees += howManyInThisZone;
            howManyTreesCanGrowThere[i / 2] = howManyInThisZone + UnityEngine.Random.Range(2, 5);
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

    public void deforest(float x, float y) {
        int zone = checkInsideZones(x, y);
        if (zone == -1) return;
        aliveTrees[zone]--;
        liveTrees--;
        points += UnityEngine.Random.Range(1, 11);

        Debug.Log($"Game state: toHarvest {howManyTrees} points: {points} liveTrees{liveTrees}");
        if (gameState == 1)
            Debug.Log("Ganaste");
        else if (gameState == 0)
            Debug.Log("Perdiste");
        gameState = checkGameState();    
    }

    private int checkGameState() {
        int sumAlives = 0;
        for (int i = 0; i < aliveTrees.Length; i++) sumAlives += aliveTrees[i];

        if (sumAlives >= howManyTrees) return 1;
        
        int possibles = 0;
        for (int i = 0; i < aliveTrees.Length; i++) {
            possibles += howManyTreesCanGrowThere[i] - howManyTreesGrowthThere[i];
        }

        if (sumAlives + possibles < howManyTrees) { return 0; }

        return 2;
    }


    public int canHarvest(float x, float y) {
        Debug.Log($"harvest point {x}  {y}");
        int zone = checkInsideZones(x, y);
        if (zone == -1) return 0;

        Debug.Log($"harvest point zone {zone} already {howManyTreesGrowthThere[zone]} can  {howManyTreesCanGrowThere[zone]}");
        if (howManyTreesGrowthThere[zone] + 1 > howManyTreesCanGrowThere[zone]) return 1;
        howManyTreesGrowthThere[zone]++;
        aliveTrees[zone]++;

        Instantiate(aliveTree, new Vector3(x, y, 0),  Quaternion.identity);   
        gameState = checkGameState(); 
        if (gameState == 1)
            Debug.Log("Ganaste");
        else if (gameState == 0)
            Debug.Log("Perdiste");
        points++;
        liveTrees++;
         Debug.Log($"Game state: toHarvest {howManyTrees} points: {points} liveTrees: {liveTrees}");
        return 2;
    }
 

}
