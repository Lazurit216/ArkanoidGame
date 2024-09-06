using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    public bool isShowed { get; private set; }
    private void Start()
    {
        isShowed = false;
        menuPanel.SetActive(isShowed);
        Time.timeScale = 1f;
    }
    public void MenuSwitch(bool isOn)
    {
        isShowed = isOn;
        if (isShowed) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        menuPanel.SetActive(isShowed);
    }
    public void OnContinue()
    {
        MenuSwitch(false);
    }
    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
