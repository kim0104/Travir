using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class JejuToMazeIsland : MonoBehaviour
{

    public Button startButton;
    public Transform startingPoint;
    public Transform goal;
    Vector3 startButtonPosition = new Vector3(137, 30, 346);

    private void Start()
    {
        startButton.onClick.AddListener(MovePlayerToStartingPoint);

    }

    private void Update()
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

            // 일정 거리 이하로 접근했을 때 동작을 수행
            if (distanceToGoal < 1f)
            {
                MovePlayerToJejuMazePark();
            }
        }
    }


    private void MovePlayerToStartingPoint()
    {
        GameObject currentPlayer = MapManager.GetCurrentPlayerInstance();
        
        if (currentPlayer != null)
        {
            currentPlayer.transform.position = startingPoint.position;
            currentPlayer.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void MovePlayerToJejuMazePark()
    {
        GameObject currentPlayer = MapManager.GetCurrentPlayerInstance();

        if (currentPlayer != null)
        {
            currentPlayer.transform.position = startButtonPosition;
            currentPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }
}
