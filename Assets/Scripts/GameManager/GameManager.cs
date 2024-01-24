using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using utils;
using static SFXPlayer;

public struct PlayerRoutine {
    public PlayerRoutine(Coroutine coroutine,Player player) {
        this.coroutine = coroutine;
        this.player = player;
    }
    public Player player;
    public Coroutine coroutine;
}
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
    List<PlayerRoutine> resetSpeedRoutines = new List<PlayerRoutine>();
    [Header("---------------- SFX ----------------")]
    [SerializeField] SFXPlayer sfx;

    private void Start() {
        Instance = this;
        PlayerLayer = _PlayerLayer;
    }
    void FixedUpdate() {
        if (finished) return;
        foreach (var player in Players)
            if (player.life <= 0) StartCoroutine(PlayerLoose(player));
    }
	//SFX
	public void PlaySFX(SFX_TYPE type) {
		sfx.AskSFX(type);
	}
    //
	public void NewTarget(Target newTarget) {
        Targets.Add(newTarget);
    }
    public void DeleteTarget(Target newTarget) {
        Targets.Remove(newTarget);
    }
    public Target[] AskForTarget(Side side, Transform player) {
        return Targets.Where(target => target.side == side).Where(target => player.position.z < target.transform.parent.position.z && target.transform.parent.position.z <= player.position.z + 19 ).ToArray();
    }

    public void PlayerReachWinCondition() {
        StartCoroutine(PlayerLoose(Players.OrderBy(p => p.transform.position.z).First()));
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
        IEnumerable<PlayerRoutine> alreadyExistingRoutine = resetSpeedRoutines.Where(myStruct => myStruct.player == player);
        if (alreadyExistingRoutine.Count() > 0) {
            StopCoroutine(alreadyExistingRoutine.First().coroutine);
            resetSpeedRoutines.Remove(alreadyExistingRoutine.First());
        }
		resetSpeedRoutines.Add(new PlayerRoutine(StartCoroutine(ResetSpeed(player.GetComponent<PlayerMovement>())),player));
	}
	private IEnumerator ResetSpeed(PlayerMovement player) {
        yield return new WaitForSeconds(GameManager.SPEED_BOOST_DURATION);
        if (player.forwardSpeed > infiniteForward.maxPlayerSpeed) player.forwardSpeed = infiniteForward.maxPlayerSpeed;
    }
    //public void Accelerate() {
    //    foreach (var player in Players) 
    //        player.forwardSpeed += Time.fixedDeltaTime / 100 * speedProgression;
    //}
    //private void SlowDown() {
    //    yield return new WaitForSeconds(GameManager.SPEED_BOOST_DURATION);
    //    if (player.forwardSpeed > infiniteForward.maxPlayerSpeed) player.forwardSpeed = infiniteForward.maxPlayerSpeed;
    //}
}