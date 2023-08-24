using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class MenuPanel : MonoBehaviour
{
    public GameObject settingsPanel; // Settings Panel GameObject
    public Slider volumeSlider; // Volume Slider
    public Button quitButton; // Quit Button
    public Button mapButton; // map Button
    public TMP_Dropdown sceneDropdown; // ScenechangeDropdwon

    // Start is called before the first frame update
    void Start()
    {
        // Add listener for volume slider
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });

        // Add listener for quit button
        quitButton.onClick.AddListener(delegate { OnQuitClick(); });

        // Add listener for map button
        mapButton.onClick.AddListener(delegate { OnMapClick(); });

        // Add listener for resolution dropdown
        sceneDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Hide the settings panel at start
        settingsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If the settings button (assumed to be 'S' key here) is pressed, toggle the settings panel
        if (Input.GetKeyDown(KeyCode.M))
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }

    // Called when the volume slider value is changed
    void OnVolumeChange()
    {
        // Set the volume of the AudioListener (i.e., the global volume) to the slider value
        AudioListener.volume = volumeSlider.value;
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

    void OnMapClick()
    {
        //Map Panel change

    }

    // Called when the resolution dropdown value is changed
    private void OnDropdownValueChanged(int index)
    {
        // Get the selected scene name from the Dropdown
        string selectedSceneName = sceneDropdown.options[index].text;

        // Load the selected scene

        
        SceneManager.LoadScene(selectedSceneName);
    }
}

