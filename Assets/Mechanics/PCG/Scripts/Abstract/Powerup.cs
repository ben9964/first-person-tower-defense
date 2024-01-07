using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{

    public String name;
    public float despawnTime;
    
    // Start is called before the first frame update
    void Start()
    {
        //Despawn the powerup after a certain amount of time
        this.Invoke(() =>
        {
            Destroy(gameObject);
        }, despawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.gameObject.CompareTag("Player"))
        {
            ApplyEffect(other.transform.parent.GetComponent<AbstractPlayer>());
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect(AbstractPlayer player);
}
