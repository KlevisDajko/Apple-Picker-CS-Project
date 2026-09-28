using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    [Header("Start Screen")]
    public GameObject startPanel;

    public List<GameObject> basketList;

    void Start()
    {
        // Show the start screen
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        // Pause the game until Start is clicked
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
        // Hide the start screen
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        // Start the game
        Time.timeScale = 1;
    }

    public void AppleMissed()
    {
        // Destroy all of the falling Apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        // Destroy one of the Baskets

        // Get the index of the last Basket in basketList
        int basketIndex = basketList.Count - 1;

        // Get a reference to that Basket GameObject
        GameObject basketGO = basketList[basketIndex];

        // Remove the Basket from the list and destroy the GameObject
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        // If there are no Baskets left, restart the game
        if (basketList.Count == 0)
        {
            SceneManager.LoadScene("_Scene_0");
        }
    }
}
