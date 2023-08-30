using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasActive : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public Button closeButton;

    private void Start()
    {
        // 닫기 버튼에 클릭 이벤트 리스너 추가
        closeButton.onClick.AddListener(CloseTutorialCanvas);

        // 처음에는 튜토리얼 캔버스를 꺼둡니다.
        tutorialCanvas.SetActive(false);
    }

    private void Update()
    {
        // F1 키를 누르면 튜토리얼 캔버스를 켭니다.
        if (Input.GetKeyDown(KeyCode.F1))
        {
            tutorialCanvas.SetActive(!tutorialCanvas.activeSelf);
        }
    }

    private void CloseTutorialCanvas()
    {
        // 닫기 버튼을 누르면 튜토리얼 캔버스를 끕니다.
        tutorialCanvas.SetActive(false);
    }
}
