using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using System;

public class WallAdditionReward : IReward
{
    public GameObject _pieceOfWall;
    int countOfPieceOfWall = 0;
    enum TypeOfWall
    {
        BACK_WALL = 1,
        RIGHT_WALL = 2,
        LEFT_WALL = 3
    };

    

    public override bool rewardControl(object objectOfInterest)
    {
        
        if(objectOfInterest.GetType() == typeof(Score))
        {
            countOfPieceOfWall++;
            Score scr = (Score)objectOfInterest;
            Debug.Log("SCORE:"+scr.getScore());
            if (scr.getScore() != 0 && (scr.getScore() % 8 == 0 || scr.getScore() >= 64))
            {
            _pieceOfWall.name = "pieceOfWall_" + countOfPieceOfWall;
                Instantiate(_pieceOfWall, GenerateRandPos(), Quaternion.identity);
            }
           
        }
        return true;
    }

    private Vector3 GenerateRandPos()
    {
        float randomX = Random.Range(-34, 35);
        float bias = Random.Range(-34, 35);
        int scale = 4;

        float tmp = randomX - 36;//randomX - startposx
        float moded = tmp % scale;

        if (moded <= scale / 2) randomX -= moded;

        if (moded > scale / 2) randomX += scale - moded;
        /*
        if (randomX + bias < -34 || randomX + bias > 35)
            randomX -= bias;
        else
            randomX += bias;
        */
        return new Vector3(randomX , 40.0f, -38.0f);

    }


}
