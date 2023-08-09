using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class SettingsPanel : MonoBehaviour
{
    [Header("설정창")]
    public TextMeshProUGUI optionPanelName = null;
    public GameObject settingsPanel; // Settings Panel GameObject
    public Slider volumeSlider; // Volume Slider
    public Button quitButton; // Quit Button
    public TMP_Dropdown resolutionDropdown; // Resolution Dropdown
    public AudioSource bgmAudio;
    public TMP_InputField inputField;
    public ToggleGroup toggleGroup;
    private List<Toggle> toggles = new List<Toggle>();
    private string password = "hello";

    // Start is called before the first frame update
    void Start()
    {
        // Add listener for volume slider
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });

        // Add listener for quit button
        quitButton.onClick.AddListener(delegate { OnQuitClick(); });

        // Add listener for resolution dropdown
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });

        // Hide the settings panel at start
        settingsPanel.SetActive(true);

        inputField.onEndEdit.AddListener(delegate { CheckingPassword(); });

        toggles.AddRange(toggleGroup.GetComponentsInChildren<Toggle>());

        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener((bool isOn) => OnToggleValueChanged(toggle, isOn));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the settings button (assumed to be 'S' key here) is pressed, toggle the settings panel
        if (Input.GetKeyDown(KeyCode.S))
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }

    void OnToggleValueChanged(Toggle changedToggle, bool isOn)
    {
        if(isOn)
        {
            switch (changedToggle.transform.GetChild(1).GetComponent<Text>().text)
            {
                case "Red":
                    optionPanelName.color = Color.red;
                    break;
                case "Blue":
                    optionPanelName.color = Color.blue;
                    break;
                case "Green":
                    optionPanelName.color = Color.green;
                    break;
            }
            
        }
    }

    void CheckingPassword()
    {
        if(password==inputField.text)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Called when the volume slider value is changed
    void OnVolumeChange()
    {
        // Set the volume of the AudioListener (i.e., the global volume) to the slider value
        //AudioListener.volume = volumeSlider.value;
        bgmAudio.volume = volumeSlider.value; // AudioSource는 한 개가 아니라 위에서 선언한 변수를 바로 사용한다. 
    }

    // Called when the quit button is clicked
    void OnQuitClick()
    {
        // Quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Called when the resolution dropdown value is changed
    void OnResolutionChange()
    {
        // Get the current resolution option
        string resolution = resolutionDropdown.options[resolutionDropdown.value].text;

        // Split the resolution option into width and height
        string[] dimensions = resolution.Split('x');

        // Set the screen resolution
        Screen.SetResolution(int.Parse(dimensions[0]), int.Parse(dimensions[1]),Screen.fullScreen);

        Debug.Log(resolutionDropdown.options[resolutionDropdown.value].text);
    }
}
