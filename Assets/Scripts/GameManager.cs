using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using utils;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public static LayerMask PlayerLayer;
    [SerializeField] public InfiniteForward infiniteForward;
    [Header("---------------- Player ----------------")]
    [SerializeField] private LayerMask _PlayerLayer;
    [SerializeField] private Player[] Players;
    [Serializable] private class PlayersUI {
        public Player player;
        public PowerUpUI powerUpCanva;
    }
    [SerializeField] private PlayersUI[] playersUI;
    [Header("---------------- EndGameMonitoring ----------------")]
    [SerializeField] private EndGame EndGame;
    [SerializeField] public bool finished = false;
    [Header("---------------- PowerUP ----------------")]
    public static float SPEED_BOOST_DURATION;
    List<Target> Targets = new List<Target>();

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

    public void AddPowerUpIcon(Player thisPawn, PowerUP inPowerUp, int index) {
        foreach (var playerUI in GameManager.Instance.playersUI) {
            if (playerUI.player == thisPawn) {
                if (inPowerUp == null) {
                    playerUI.powerUpCanva.images[index].texture = null;
                }
                else {
                    playerUI.powerUpCanva.images[index].texture = inPowerUp.image;
                }
                break;
            }
        }
    }

    public void ResetSpeed(Player player) {
        StartCoroutine(ResetSpeed(player.GetComponent<PlayerMovement>()));
       
    }
    private IEnumerator ResetSpeed(PlayerMovement player) {
        yield return new WaitForSeconds(GameManager.SPEED_BOOST_DURATION);
        if (player.forwardSpeed > infiniteForward.maxPlayerSpeed) player.forwardSpeed = infiniteForward.maxPlayerSpeed;
    }
}