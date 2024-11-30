using UnityEngine;

/// <summary>
/// Each hoop a disk/ball can sit in
/// </summary>
public class Hoop : MonoBehaviour
{
    [SerializeField] private Transform _midPointTransform;
    [SerializeField] private GameObject _playerOneBall;
    [SerializeField] private GameObject _playerTwoBall;

    //Used for finding the final position for the disk the be in
    public Vector3 WorldPosition => _midPointTransform.position;

    //Enables the disk corresponindg to the player
    public void SetDisk(Player player)
    {
        if (player == Player.PlayerOne)
        {
            _playerOneBall.SetActive(true);
            _playerTwoBall.SetActive(false);
        } 
        else if (player == Player.PlayerTwo)
        {
            _playerOneBall.SetActive(false);
            _playerTwoBall.SetActive(true);
        }
    }

    public void ResetHoop()
    {
        _playerOneBall.SetActive(false);
        _playerTwoBall.SetActive(false);
    }

}
