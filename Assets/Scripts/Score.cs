using System.Collections;
using System.Collections.Generic;

public class Score
{
    private int _score;

    public static Score Instance;

    private Score()
    {
        _score = 0;
    }

    public void decreaseScoreOfGame(int valueOfFallingDownObject)
    {
        int tmpScore = getScore();
        int newValueOfScore = tmpScore - (valueOfFallingDownObject / 2);
        setScore(newValueOfScore, true);
    }

    public void setScore(int score, bool flagOfdecrease = false)
    {
        if (flagOfdecrease == false)
        {
            if (score > _score)
                this._score = score;
        }
        else
        {
            this._score = score;
        }

    }

    public int getScore()
    {
        return this._score;
    }

    public static Score getInstance()
    {
        if(Instance == null)
        {
            Instance = new Score();
        }

        return Instance;

    }
}
