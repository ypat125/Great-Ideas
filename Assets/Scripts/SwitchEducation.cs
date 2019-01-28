using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchEducation : MonoBehaviour
{
	public Text displayText;
	public Text winText;
	public Button privateButton;
	public Button publicButton;
	public Button otherButton;
	enum Levels {Private, Public, PrivatePrivate, PrivatePublic, PublicPublic, PublicPrivate, PrivatePrivatePrivate, PrivatePrivatePublic, PrivatePublicPublic, PublicPublicPublic, PublicPublicPrivate, PublicPrivatePrivate};
	private int[] lives = {2, 1, 2, 1, 1, 2, 3, 1, 2, 2, 2, 2};
	private double[] stamina = {1, 1, 1, 0.5, 3, 1, 1, 0.25, 1, 5, 2, 0.75};

    // Start is called before the first frame update
    void Start()
    {
		if (PlayerStats.level >= (int)Levels.PrivatePrivatePrivate) {
			displayText.text = "You win!";
			winText.gameObject.SetActive(true);
			privateButton.gameObject.SetActive(false);
			publicButton.gameObject.SetActive(false);
			otherButton.gameObject.SetActive(false);
			winText.text = "[exiting game...]";
			Invoke("QuitGame", 3.0f);
		}
		else {
			winText.gameObject.SetActive(false);
			if (PlayerStats.level == (int)Levels.PrivatePublic || PlayerStats.level == (int)Levels.PublicPrivate) {
				privateButton.gameObject.SetActive(false);
				publicButton.gameObject.SetActive(false);
			}
			else {
				otherButton.gameObject.SetActive(false);
			}
			privateButton.onClick.AddListener(privateButtonPressed);
			publicButton.onClick.AddListener(publicButtonPressed);
			otherButton.onClick.AddListener(otherButtonPressed);
		}
    }

	void Update() {
		if (PlayerStats.level >= (int)Levels.PrivatePrivatePrivate) {
			if (Input.anyKey) {
				QuitGame();
			}
		}
	}

	void QuitGame() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	void publicButtonPressed() {
		if (PlayerStats.level == -1) {
			PlayerStats.level = (int)Levels.Public;
		}
		else if (PlayerStats.level == (int)Levels.Public) {
			PlayerStats.level = (int)Levels.PublicPublic;
		}
		else if (PlayerStats.level == (int)Levels.Private) {
			PlayerStats.level = (int)Levels.PrivatePublic;
		}
		else if (PlayerStats.level == (int)Levels.PublicPublic) {
			PlayerStats.level = (int)Levels.PublicPublicPublic;
		}
		else if (PlayerStats.level == (int)Levels.PrivatePrivate) {
			PlayerStats.level = (int)Levels.PrivatePrivatePublic;
		}
		statsUpdate();
	}

	void privateButtonPressed() {
		if (PlayerStats.level == -1) {
			PlayerStats.level = (int)Levels.Private;
		}
		else if (PlayerStats.level == (int)Levels.Public) {
			PlayerStats.level = (int)Levels.PublicPrivate;
		}
		else if (PlayerStats.level == (int)Levels.Private) {
			PlayerStats.level = (int)Levels.PrivatePrivate;
		}
		else if (PlayerStats.level == (int)Levels.PublicPublic) {
			PlayerStats.level = (int)Levels.PublicPublicPrivate;
		}
		else if (PlayerStats.level == (int)Levels.PrivatePrivate) {
			PlayerStats.level = (int)Levels.PrivatePrivatePrivate;
		}
		statsUpdate();
	}

	void otherButtonPressed() {
		if (PlayerStats.level == (int)Levels.PublicPrivate) {
			PlayerStats.level = (int)Levels.PublicPrivatePrivate;
		}
		else if (PlayerStats.level == (int)Levels.PrivatePublic) {
			PlayerStats.level = (int)Levels.PublicPrivatePrivate;
		}
		statsUpdate();
	}

	void statsUpdate() {
		PlayerStats.lives = lives[PlayerStats.level];
		PlayerStats.stamina = stamina[PlayerStats.level];
		SceneManager.LoadScene(1);
	}

}
