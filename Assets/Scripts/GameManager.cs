using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static LayerMask PlayerLayer;
    [SerializeField] private LayerMask _PlayerLayer;
    [SerializeField] private Player[] Players;
    [SerializeField] private EndGame EndGame;
    List<Target> Targets = new List<Target>();
    public static GameManager Instance;
    public bool finished = false;

    public void NewTarget(Target newTarget) {
        Targets.Add(newTarget);
    }
    public void DeleteTarget(Target newTarget) {
        Targets.Remove(newTarget);
    }
    public Target AskForTarget(float sideIndex,Player player) {
        return Targets[0];
    }
    private void Start() {
        Instance = this;
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