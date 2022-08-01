using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystemManager 
{
    public List<object> _objectToBeUsedForReward;//TODO: object yerine T t generic yapisi kullanilacak
    public List<IReward> _objectsToObservers;
    

    public static RewardSystemManager Instance;

    private RewardSystemManager()
    {
        _objectToBeUsedForReward = new List<object>();
        _objectsToObservers = new List<IReward>();
    }

    public void subscribeToTheRewardSystem(object objectToBeUsedForReward) 
    {
        _objectToBeUsedForReward.Add(objectToBeUsedForReward);
    }

    public void subscribeToTheObserverList(IReward observer)
    {
        _objectsToObservers.Add(observer);
    }

    public static RewardSystemManager getInstance()
    {
        if (Instance == null)
            Instance = new RewardSystemManager();
        
        return Instance;
    }


}
