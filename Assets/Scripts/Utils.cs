using System.Collections;
using System.Collections.Generic;

public class Utils 
{
    float _speed;
    float _movementSpeed;
    float _timerForRecreation;

    private static Utils Instance;

    private Utils()
    {
        _speed = 120.0f;
        _movementSpeed = 20.0f;
        _timerForRecreation = 1.0f;
    }

    public void setSpeedOfDice(float speed)
    {
        _speed = speed;
    }

    public float getSpeedOfDice()
    {
        return _speed;
    }

    public void setMovementSpeedOfDice(float movementSpeed)
    {
        _movementSpeed = movementSpeed;
    }

    public float getMovementSpeedOfDice()
    {
        return _movementSpeed;
    }

    public void setTimerValue(float timerForRecreation)
    {
        _timerForRecreation = timerForRecreation;
    }

    public float getTimerValue()
    {
        return this._timerForRecreation;
    }

    public static Utils getInstance()
    {
        if (Instance == null)
        {
            Instance = new Utils();
        }
        return Instance;
    }
}
