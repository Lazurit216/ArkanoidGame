using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    private bool isShowed;
    [SerializeField] private AudioSource musicSource;
    private float defMusicVol;
    [SerializeField] private AudioSource soundSource;
    private float defSoundVol;
    private void Start()
    {

        isShowed = false;
        menuPanel.SetActive(isShowed);
        Time.timeScale = 1f;

        defMusicVol=musicSource.volume;
        defSoundVol=soundSource.volume;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MenuSwitch();
        }
    }
    private void MenuSwitch()
    {
        isShowed = !isShowed;
        if (isShowed) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        menuPanel.SetActive(isShowed);
    }
    public void OnContinue()
    {
        MenuSwitch();
    }
    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnMusicToggle(Toggle value)
    {
        if (value.isOn) musicSource.volume = defMusicVol;
        else musicSource.volume=0;
    }
    public void OnSoundToggle(Toggle value)
    {
        if (value.isOn) soundSource.volume = defSoundVol;
        else soundSource.volume = 0;
    }
    public void OnExit()
    {
        Application.Quit();
    }

}
