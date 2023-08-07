using UnityEngine;
using UnityEngine.UI;

public class AdjustButtonSize : MonoBehaviour
{
    public GridLayoutGroup gridLayout;

    private void OnEnable()
    {
        AdjustSizeBasedOnTextSize();
    }

    private void AdjustSizeBasedOnTextSize()
    {
        float maxWidth = 0f; // 모든 버튼의 텍스트 중에서 가장 큰 너비를 저장하기 위한 변수
        bool isFirstButton = true; // 첫 번째 버튼인지 판단하기 위한 플래그

        foreach (Transform buttonTransform in transform)
        {
            if (isFirstButton)
            {
                isFirstButton = false;
                continue;
            }

            Text textComponent = buttonTransform.GetComponentInChildren<Text>();
            if (textComponent)
            {
                float width = textComponent.preferredWidth + 20; // 여백을 위해 +20을 추가
                if (width > maxWidth)
                {
                    maxWidth = width;
                }
            }
        }

        // 가장 큰 텍스트 너비를 기반으로 GridLayout의 cellSize를 조절합니다.
        gridLayout.cellSize = new Vector2(maxWidth, 32); // height는 32로 고정
    }
}
