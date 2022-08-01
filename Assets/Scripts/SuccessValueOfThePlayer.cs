using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SuccessValueOfThePlayer
{
    private float _successValue;

    public static SuccessValueOfThePlayer Instance;

    private SuccessValueOfThePlayer()
    {
        _successValue = 0;
    }

    public float calculateTheSuccessValueOfThePlayer(Dictionary<int, int> successValueOfThePlayer)
    {

        for (int i = 0; i < successValueOfThePlayer.Count; i++)
        {
            if ((i + 1) < successValueOfThePlayer.Count)
            {
                _successValue =
                        (successValueOfThePlayer.ElementAt(i + 1).Value -
                            successValueOfThePlayer.ElementAt(i).Value) /
                        (successValueOfThePlayer.ElementAt(i + 1).Key -
                            successValueOfThePlayer.ElementAt(i).Key); // y2 -y1 / x2 - x1

            }
        }

        return _successValue;
    }

    public float getSuccesValue()
    {
        return this._successValue;
    }

    public static SuccessValueOfThePlayer getInstance()
    {
        if (Instance == null)
            Instance = new SuccessValueOfThePlayer();

        return Instance;
    }
}
