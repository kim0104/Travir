using UnityEngine;
using UnityEngine.UI;

public class TileDetailScreen : MonoBehaviour
{
    public Text detailText;
    public Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(HideDetail);
       
    }

    public void ShowDetail(string title, string starValue, string reviewValue)
    {

        detailText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        detailText.fontSize = 14;
        detailText.fontStyle = FontStyle.Bold;
        detailText.alignment = TextAnchor.UpperLeft;
        detailText.color = Color.black;

        detailText.text = "[" + title + "]\n\n" +
                          "별점: " + starValue + "\n" +
                          "내용: " + reviewValue;

        
    }

    public void HideDetail()
    {
        gameObject.SetActive(false);
    }


}


