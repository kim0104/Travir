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
    public GameObject mapPanel; // Map Panel GameObject
    public Slider volumeSlider; // Volume Slider
    public Button quitButton; // Quit Button
    public Button mapButton; // map Button
    public Button gimpoButton;
    public Button lotteworldButton;
    public Button lottetowerButton;
    public Button namsanButton;
    public Button gwanghwaButton;
    public Button worldcupButton;
    public Button settingscloseButton; // Settings Panel close
    public Button mapcloseButton; // Map Panel close
    public Button returnButton; // return to Settings Panel

    // Start is called before the first frame update
    void Start()
    {
        // Add listener for volume slider
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });

        // Add listener for quit button
        quitButton.onClick.AddListener(delegate { OnQuitClick(); });

        // Add listener for map button
        mapButton.onClick.AddListener(delegate { OnMapClick(); });

        gimpoButton.onClick.AddListener(delegate { OnGimpoClick(); });

        lotteworldButton.onClick.AddListener(delegate { OnLotteworldClick(); });

        lottetowerButton.onClick.AddListener(delegate { OnLottetowerClick(); });

        namsanButton.onClick.AddListener(delegate { OnNamsanClick(); });

        gwanghwaButton.onClick.AddListener(delegate { OnGwanghwaClick(); });

        worldcupButton.onClick.AddListener(delegate { OnWorldCupClick(); });

        settingscloseButton.onClick.AddListener(delegate { OnSettigsCloseClick(); });

        mapcloseButton.onClick.AddListener(delegate { OnMapCloseClick(); });

        returnButton.onClick.AddListener(delegate { OnReturnClick(); });

        // Hide the settings panel at start
        settingsPanel.SetActive(false);
        mapPanel.SetActive(false);

        

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
        settingsPanel.SetActive(false);
        mapPanel.SetActive(true);

    }


    public SeoulMapManager seoulMapManager; // SeoulMapManager 참조
    void OnGimpoClick()
    {
        Data.spawnType = Data.SpawnType.Gimpo;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        seoulMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }

    void OnLotteworldClick()
    {
        Data.spawnType = Data.SpawnType.Lotteworld;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        seoulMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false); ;
    }

    void OnLottetowerClick()
    {
        Data.spawnType = Data.SpawnType.Lottetower;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        seoulMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }

    void OnNamsanClick()
    {
        Data.spawnType = Data.SpawnType.Namsan;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        seoulMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }

    void OnGwanghwaClick()
    {
        Data.spawnType = Data.SpawnType.Gwanghwa;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        seoulMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }

    void OnWorldCupClick()
    {
        Data.spawnType = Data.SpawnType.Worldcup;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        seoulMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }


    void OnSettigsCloseClick()
    {
        settingsPanel.SetActive(false);
    }

    void OnMapCloseClick()
    {
        mapPanel.SetActive(false);
    }

    void OnReturnClick()
    {
        mapPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    
}

