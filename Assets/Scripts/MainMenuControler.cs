using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuControler : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TextMeshProUGUI volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Gameplay Setting")]
    [SerializeField] private TextMeshProUGUI controlerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    private int _qualityLevel;
    private bool _isFullscreen;
    private float _brightnessLevel;

    [Header("Private Toggle")]
    [SerializeField] private Toggle InvertToggle = null;

    [Header("Graphics Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TextMeshProUGUI brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;




    [Header("Conformation")]
    [SerializeField] private GameObject conformationPrompt = null;

    [Header("Levels to Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSaveGameDialog = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Resolution Dropdown")]
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> option = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string options = resolutions[i].width + " x " + resolutions[i].height;
            option.Add(options);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
      //  resolutionDropdown.AddOptions(option);
      //  resolutionDropdown.value = currentResolutionIndex;
       // resolutionDropdown.RefreshShownValue();
    }
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSaveGameDialog.SetActive(true);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }
    public void volumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(conformationBox());
    }
    public void SetControllerSensitivity(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controlerSenTextValue.text = sensitivity.ToString("0");
    }
    public void GamePlayApply()
    {
        if(InvertToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            // invert the y
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            // not invert
        }
        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine(conformationBox());
    }
     public void setBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");

    }
    public void setFullScreen(bool isFullScreen)
    {
        _isFullscreen = isFullScreen;
    }
    public void setQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }
    public void GraphycisApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        // change the brightness with our post processing or whatever it is

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullscreen ? 1 : 0));
        Screen.fullScreen = _isFullscreen;

        StartCoroutine(conformationBox());

    }
    public void resetBotton(string MenuType)
    {
        if(MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            volumeApply();

        }
        if(MenuType == "Gameplay")
        {
            controlerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            InvertToggle.isOn = false;
            GamePlayApply();
        }
    }

    public IEnumerator conformationBox()
    {
        conformationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        conformationPrompt.SetActive(false);
    }
}
