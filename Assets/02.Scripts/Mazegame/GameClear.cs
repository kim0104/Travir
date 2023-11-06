using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{

    public Button moveButton;
    public Button retryButton;
    public GameObject clearCanvas;
    public Transform goal;
    public GameObject chatCanvas;
    private JejuToMazeIsland JejuToMazeIsland;
    private StopWatch StopWatch;


    Vector3 startButtonPosition = new Vector3(137, 30, 346);

    void Start()
    {
        JejuToMazeIsland = FindObjectOfType<JejuToMazeIsland>();
        StopWatch = FindObjectOfType<StopWatch>();

        moveButton.onClick.AddListener(MovePlayerToJeju);
        moveButton.onClick.AddListener(OnButtonClick);
        retryButton.onClick.AddListener(JejuToMazeIsland.MovePlayerToStartingPoint);
        retryButton.onClick.AddListener(OnButtonClick);
        clearCanvas.SetActive(false);
    }

    void Update()
    {
        CheckIfPlayerReachedGoal();
    }


    private void CheckIfPlayerReachedGoal()
    {
        GameObject currentPlayer = MapManager.GetCurrentPlayerInstance();

        if (currentPlayer != null)
        {
            // 현재 플레이어의 위치와 goal의 위치 사이의 거리를 비교
            float distanceToGoal = Vector3.Distance(currentPlayer.transform.position, goal.position);

            if (distanceToGoal < 1f)
            {
                clearCanvas.SetActive(true);
                StopWatch.StopStopwatch();
            }
        }
    }


    private void MovePlayerToJeju()
    {
        GameObject currentPlayer = MapManager.GetCurrentPlayerInstance();

        if (currentPlayer != null)
        {
            currentPlayer.transform.position = startButtonPosition;
            currentPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);
            chatCanvas.SetActive(true);
            StopWatch.ResetStopwatch();
        }

    }

    private void OnButtonClick()
    {
        if (clearCanvas != null)
        {
            clearCanvas.SetActive(false); 
        }
    }

}
