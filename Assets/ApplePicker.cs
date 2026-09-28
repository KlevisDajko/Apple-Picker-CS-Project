using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    [Header("Start Screen")]
    public GameObject startPanel;

    [Header("Round / Game Over UI")]
    public Text roundText;
    public Button restartButton;

    public List<GameObject> basketList;

    private int round = 1;
    private bool gameOver = false;

    void Start()
    {
        // Start menu reference:
        // https://gamedevacademy.org/unity-start-menu-tutorial/

        // Show the start screen
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        // Game rounds reference:
        // https://discussions.unity.com/t/how-to-make-a-round-system-of-a-fighting-game-like-street-fighter-or-mortal-kombat-c/896998/2

        // Start on Round 1
        round = 1;
        gameOver = false;

        if (roundText != null)
        {
            roundText.text = "Round 1";
        }

        // Game over / restart button reference:
        // https://stackoverflow.com/questions/44288452/how-do-i-add-a-restart-function-to-a-game-in-unity
        // Vector 3 learened from link above.

        // Hide restart button until Game Over
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
        }

        // Pause until Start is clicked
        Time.timeScale = 0;

        basketList = new List<GameObject>();

        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);

            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);

            tBasketGO.transform.position = pos;

            basketList.Add(tBasketGO);
        }
    }

    public void StartGame()
    {
        // Start menu reference:
        // https://gamedevacademy.org/unity-start-menu-tutorial/

        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        Time.timeScale = 1;
    }

    public void AppleMissed()
    {
        if (gameOver)
        {
            return;
        }

        // Destroy all falling Apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        // Remove one Basket
        if (basketList.Count > 0)
        {
            int basketIndex = basketList.Count - 1;

            GameObject basketGO = basketList[basketIndex];

            basketList.RemoveAt(basketIndex);
            Destroy(basketGO);
        }

        // Game rounds reference:
        // https://discussions.unity.com/t/how-to-make-a-round-system-of-a-fighting-game-like-street-fighter-or-mortal-kombat-c/896998/2

        // If no baskets remain, Game Over
        if (basketList.Count == 0)
        {
            GameOver();
        }
        else
        {
            // Advance to next round
            round++;

            if (roundText != null)
            {
                roundText.text = "Round " + round;
            }
        }
    }

    public void GameOver()
    {
        // Game over / restart button reference:
        // https://discussions.unity.com/t/creating-a-restart-button/175658

        if (gameOver)
        {
            return;
        }

        gameOver = true;

        if (roundText != null)
        {
            roundText.text = "Game Over/Restart";
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }

        // Destroy remaining Apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        // Pause game
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        // Game over / restart button reference:
        // https://discussions.unity.com/t/creating-a-restart-button/175658
        //https://discussions.unity.com/t/how-do-you-use-scenemanager/173777

        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
