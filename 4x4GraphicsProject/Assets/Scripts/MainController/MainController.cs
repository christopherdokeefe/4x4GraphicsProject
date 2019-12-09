using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public Text Timer;
    float time = 60f;
    float rbGravityFactor = 8f;  // Multiply gravity constant for all rigidbodies by this amount

    // objects used for win conditions
    private Transform player;
    private MainController mainController;
    public Text winText;
    public Button playAgainButton;
    public GameObject theWorld;

    void Start()
    {
        Physics.gravity *= rbGravityFactor;  // Adjust gravity of all rigidbodies since trebuchet is really big

        winText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        player = GameObject.Find("Player").transform;
        Debug.Assert(player);
        mainController = GameObject.Find("Controller").GetComponent<MainController>();
        Debug.Assert(mainController);
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            Timer.text = "Time: " + time.ToString("F1");
            if  (player.position.y <= -150)
            {
                EnableUIWinScreen("Trebuchet");
            }
        }
        else if (time <= 0)
        {
            EnableUIWinScreen("Dodger");
        }
    }

    private void EnableUIWinScreen(string winner)
    {
        winText.gameObject.SetActive(true);
        winText.text = winner + " Wins!";
        playAgainButton.gameObject.SetActive(true);
    }

    public void Reset()
    {
        time = 60f;
        winText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
    }
}
