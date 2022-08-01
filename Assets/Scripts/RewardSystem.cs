using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem 
{
    bool _isThereAReward;
    Score scoreOfGame;
    SuccessValueOfThePlayer successValueOfThePlayer;
    RewardSystemManager rewardSystemManager;

    public RewardSystem()
    {
    }


    public void signUpToBeAnObserver(params IReward[] observers)
    {

        //I added the objects to be used in the reward system.
        RewardSystemManager.getInstance().subscribeToTheRewardSystem(Score.getInstance());
        RewardSystemManager.getInstance().subscribeToTheRewardSystem(SuccessValueOfThePlayer.getInstance());
        int index = 0;

        foreach (IReward _observer in observers)
        {
            
            RewardSystemManager.getInstance().subscribeToTheObserverList(observers[index]);
            index++;
        }
    }

    public bool notifyAllObserver()
    {
        foreach (IReward classObject in RewardSystemManager.getInstance()._objectsToObservers)
        {
            foreach (var objectToBeUsedForReward in RewardSystemManager.getInstance()._objectToBeUsedForReward)
            {
                _isThereAReward = classObject.rewardControl(objectToBeUsedForReward);
            }

        }

        return _isThereAReward;
    }
}
