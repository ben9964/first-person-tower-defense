using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : MonoBehaviour
{

    private int Base_Health =  100;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void lose()
    {
        AbstractPlayer player = GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>();
        player.GetHud().SendMessage("You Lose!", new Color32(0, 255, 0, 255));
        player.Freeze();
    }

}
