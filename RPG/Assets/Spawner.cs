using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    int monsterCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Invoke(createMonsters(), 5);
	}

    private void createMonsters()
    {
        while (monsterCount < 5)
        {
           // Instantiate(Cha_Slime);
            monsterCount++;
        }
    }
}
