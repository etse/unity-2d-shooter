using System.Collections;
using System.Collections.Generic;

public class PlayerState
{
    private static PlayerState instance = null;
    public int score = 0;
    public int currentLevel = 0;

    private PlayerState()
    {

    }

    public static PlayerState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerState();
            }
            return instance;
        }
    }

    public void addPoints(int points)
    {
        score += points;
    }
}
