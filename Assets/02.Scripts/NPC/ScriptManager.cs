using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    // 인스펙터에서 보고 설정할 수 있도록 스크립트 변수를 선언
    [SerializeField] private MonoBehaviour script1;
    [SerializeField] private MonoBehaviour script2;

    void Start()
    {
        // 스크립트를 PlayerController에 추가하기
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            if (script1 != null)
                player.gameObject.AddComponent(script1.GetType());

            if (script2 != null)
                player.gameObject.AddComponent(script2.GetType());
        }
    }
}
