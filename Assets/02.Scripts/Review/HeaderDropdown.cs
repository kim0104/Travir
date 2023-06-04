using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HeaderDropdown : MonoBehaviour


{
    public Dropdown localDropdown;
    public Dropdown conceptDropdown;
    public Dropdown seasonDropdown;
    public Dropdown companionDropdown;
    public Dropdown courseDropdown;

    public Button writeButton;
    public Button addButton;
    public Button cancelButton;
    public Button closeButton;

    private void Start()
    {
        writeButton.onClick.AddListener(OnWriteButtonClick);
        addButton.onClick.AddListener(OnAddButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnWriteButtonClick()
    {
        ResetDropdowns();
        ResetCourses();
    }

    private void OnCloseButtonClick()
    {
        ResetDropdowns();
        ResetCourses();
    }


    private void OnCancelButtonClick()
    {
        ResetDropdowns();
        ResetCourses();
    }

    private void OnAddButtonClick()
    {
        ResetDropdowns();
    }



    private void ResetDropdowns()
    {
        // 태그 초기화
        localDropdown.value = 0;
        conceptDropdown.value = 0;
        seasonDropdown.value = 0;
        companionDropdown.value = 0;
        localDropdown.RefreshShownValue();
        conceptDropdown.RefreshShownValue();
        seasonDropdown.RefreshShownValue();
        companionDropdown.RefreshShownValue();

    }

    private void ResetCourses()
    {
        // courseDropdown 초기화
        courseDropdown.ClearOptions();
        courseDropdown.AddOptions(new List<string> { "태그를 선택하세요" });
        courseDropdown.value = 0;
    }
}
