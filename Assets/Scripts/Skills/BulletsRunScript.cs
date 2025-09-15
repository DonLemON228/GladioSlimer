using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsRunScript : MonoBehaviour
{
    private List<BulletSpawnerScript> bulletSpawmersList;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void SpawnAllBullets()
    {
        BulletSpawnerScript[] objects = FindObjectsOfType<BulletSpawnerScript>();
        
        foreach (BulletSpawnerScript obj in objects)
        {
            obj.SpawnBullet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
