using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Transform _spawnPosition;
    void Start()
    {
        _spawnPosition = this.gameObject.transform;
        _spawnPosition.position = new Vector3(0, 0, 0);
        Instantiate(player, _spawnPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
