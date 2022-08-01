using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class IReward : MonoBehaviour
{
    abstract public bool rewardControl(object objectOfInterest);
}
