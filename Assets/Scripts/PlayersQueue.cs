using System.Collections.Generic;

public class PlayersQueue
{
    public static LinkedList<int> queue = new LinkedList<int>();
    public static bool CheckQueue(int playerNumber)
    {
        return playerNumber == queue.First.Value;
    }
}
