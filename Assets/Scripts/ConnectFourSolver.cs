using UnityEngine;

public abstract class ConnectFourSolver : ScriptableObject
{
    abstract public bool CheckIfWin(int rowIndex, int columnIndex, Player playerToCheck);
}
