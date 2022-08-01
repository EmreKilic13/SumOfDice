using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballReward : IReward
{
    //public GameObject go;
    void Update()
    {

    }

    void OnCollisionEnter(Collision obj)
    {

    }

    public override bool rewardControl(object objectOfInterest) 
    {
        return true;
    }


  
}
