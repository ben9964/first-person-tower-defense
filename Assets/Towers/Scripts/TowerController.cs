using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    private float projectileSpeed;
    private float range;

    void Start()
    {
        range = 50f;
        damage = 1f;
        projectileSpeed = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
