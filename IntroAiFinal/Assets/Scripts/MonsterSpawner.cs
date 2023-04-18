using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject Monster;
    private Transform spawnPosition;
    void Start()
    {
        spawnPosition = this.gameObject.transform;
        spawnPosition.position = new Vector3(1f, 1f);
        Instantiate(Monster, spawnPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
