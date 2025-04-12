using UnityEngine;

public static class GameController
{
    public static int collectableCount = 5;

    public static bool gameOver
    {
        get { return collectableCount <= 0; }
    }

    public static void Init()
    {
        collectableCount = 5;
    }

    public static void Collect()
    {
        collectableCount--;
    }
}
