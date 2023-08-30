using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class JejuMenuPanel : MonoBehaviour
{
    public GameObject settingsPanel; // Settings Panel GameObject
    public GameObject mapPanel; // Map Panel GameObject
    public Slider volumeSlider; // Volume Slider
    public Button quitButton; // Quit Button
    public Button mapButton; // map Button
    public Button jejuairportButton;
    public Button museumButton;
    public Button jeoliButton;
    public Button seongsanButton;
    public Button mandarinButton;
    public Button hanraButton;
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

        jejuairportButton.onClick.AddListener(delegate { OnJejuairportClick(); });

        museumButton.onClick.AddListener(delegate { OnMuseumClick(); });

        jeoliButton.onClick.AddListener(delegate { OnJeoliClick(); });

        seongsanButton.onClick.AddListener(delegate { OnSeongsanClick(); });

        mandarinButton.onClick.AddListener(delegate { OnMandarinClick(); });

        hanraButton.onClick.AddListener(delegate { OnHanraClick(); });

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


    public JejuMapManager jejuMapManager; // jejuMapManager 참조
    void OnJejuairportClick()
    {
        Data.spawnType = Data.SpawnType.Jejuairport;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        jejuMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }

    void OnMuseumClick()
    {
        Data.spawnType = Data.SpawnType.Museum;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        jejuMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false); ;
    }

    void OnJeoliClick()
    {
        Data.spawnType = Data.SpawnType.Jeoli;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        jejuMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }

    void OnSeongsanClick()
    {
        Data.spawnType = Data.SpawnType.Seongsan;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        jejuMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }

    void OnMandarinClick()
    {
        Data.spawnType = Data.SpawnType.Mandarin;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        jejuMapManager.ChangePlayerPosition(player);
        mapPanel.SetActive(false);
    }

    void OnHanraClick()
    {
        Data.spawnType = Data.SpawnType.Hanra;
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체를 찾습니다.
        jejuMapManager.ChangePlayerPosition(player);
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
