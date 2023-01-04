using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static LayerMask PlayerLayer;
    [SerializeField] private LayerMask _PlayerLayer;
    [SerializeField] private Player[] Players;
    [SerializeField] private EndGame EndGame;

    public bool finished = false;

    private void Start() {
        PlayerLayer = _PlayerLayer;
    }

    void FixedUpdate() {
        if (finished) return;
        foreach (var player in Players)
            if (player.life <= 0) PlayerLoose(player);
    }

    private void PlayerLoose(Player looser) {
        foreach (var player in Players) player.life = 0;
        finished = true;
        EndGame.winner = Players.Where(player => player != looser).First();
        EndGame.enabled = true;
    }
}