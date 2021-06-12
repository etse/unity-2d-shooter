using System.Collections;
using System.Collections.Generic;

public class PlayerScore
{
    private static PlayerScore instance = null;
    public int score = 0;

    private PlayerScore()
    {

    }

    public static PlayerScore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerScore();
            }
            return instance;
        }
    }

    public void addPoints(int points)
    {
        score += points;
    }
}
