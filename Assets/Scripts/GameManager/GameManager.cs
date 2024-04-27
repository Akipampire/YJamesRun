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
    private int score;
    private bool draw;
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
    private int dead;

    private void Start() {
        Instance = this;
        PlayerLayer = _PlayerLayer;
    }
    void FixedUpdate() {
        dead = 0;
        if (finished) return;
        foreach (var player in Players) {
            if (player.life <= 0) {
                dead += 1;
            }
        }
        if (dead >= 2) PlayerReachWinCondition();
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
        StartCoroutine(PlayerLoose(Players.OrderBy(p => p.score).First()));
    }

    private IEnumerator PlayerLoose(Player looser) {
        EndGame.winner = Players.Where(player => player != looser).First();
        yield return new WaitForSeconds(5);
        finished = true;
        if (looser.score == EndGame.winner.score) { //if the scores are the same
            draw = true;
            EndGame.Draw();
        }
        else if (looser.life > 0) { //if defeat because but still alive
            if (looser.score > EndGame.winner.score) //if the looser's score is superior to the winner's
                EndGame.winner = looser; //replace the winner by the looser ("priority to score")
        }
        PlayerPrefs.SetInt("actual", EndGame.winner.score); //enters the score in the playerprefs for the score board at the end
        if (!draw) //if it's not a draw
            EndGame.enabled = true; //launch the EndGame casual script
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
        if (player.forwardSpeed > infiniteForward.maxPlayerSpeed) 
        player.forwardSpeed = infiniteForward.maxPlayerSpeed;
    }
    //public void Accelerate() {
    //    foreach (var player in Players) 
    //        player.forwardSpeed += Time.fixedDeltaTime / 100 * speedProgression;
    //}
    //private void SlowDown() {
        //yield return new WaitForSeconds(GameManager.SPEED_BOOST_DURATION);
        //if (player.forwardSpeed > infiniteForward.maxPlayerSpeed) player.forwardSpeed = infiniteForward.maxPlayerSpeed;
    //}

}