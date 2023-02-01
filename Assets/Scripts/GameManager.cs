using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using utils;

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
    public Target[] AskForTarget(Side side, Transform player) {
        return Targets.Where(target => target.side == side).Where(target => player.position.z < target.transform.parent.position.z && target.transform.parent.position.z <= player.position.z + 19 ).ToArray();
    }
    private void Start() {
        Instance = this;
        PlayerLayer = _PlayerLayer;
    }
    void FixedUpdate() {
        if (finished) return;
        foreach (var player in Players)
            if (player.life <= 0) StartCoroutine(PlayerLoose(player));
    }

    private IEnumerator PlayerLoose(Player looser) {
        yield return new WaitForSeconds(5);
        finished = true;
        EndGame.winner = Players.Where(player => player != looser).First();
        EndGame.enabled = true;
    }
}