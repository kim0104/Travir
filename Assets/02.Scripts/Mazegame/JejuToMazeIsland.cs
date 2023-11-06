using UnityEngine;
using UnityEngine.UI;


public class JejuToMazeIsland : MonoBehaviour
{

    public Button startButton;
    public Transform startingPoint;
    public GameObject chatCanvas;
    public GameObject startCanvas;
    private StopWatch StopWatch;
    private float activationStartTime;
    private float duration = 2f;

    private void Start()
    {
        StopWatch = FindObjectOfType<StopWatch>();
        startButton.onClick.AddListener(MovePlayerToStartingPoint);
        startCanvas.SetActive(false);

    }

    private void Update()
    {
        if (startCanvas.activeSelf && Time.time - activationStartTime >= duration)
        {
            // 활성화된 캔버스가 3초 이상 지난 경우 비활성화
            startCanvas.SetActive(false);
        }
    }



    public void MovePlayerToStartingPoint()
    {
        GameObject currentPlayer = MapManager.GetCurrentPlayerInstance();
        
        if (currentPlayer != null)
        {
            currentPlayer.transform.position = startingPoint.position;
            currentPlayer.transform.rotation = Quaternion.Euler(Vector3.zero);
            chatCanvas.SetActive(false);
            startCanvas.SetActive(true);
            StopWatch.StartStopwatch();
            activationStartTime = Time.time;
        }
    }

    
}
