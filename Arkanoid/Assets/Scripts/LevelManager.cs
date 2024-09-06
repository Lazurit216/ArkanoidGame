using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> m_Levels = new List<Level>();
    private int curentLevelid;
    private Level curentLevel;
    private Player player;

    public static LevelManager Instance;

    public event NextLvl onNextLevel;
    public delegate void NextLvl();

    private SoundManager soundManager;
    [SerializeField] private AudioClip nextLvlSound;

    [SerializeField] private PopupMenu winMenu;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        curentLevelid = 0;
        LoadLevel(curentLevelid);

        player = Player.Instance;
        soundManager = SoundManager.Instance;

        winMenu.MenuSwitch(false);
    }
    private void LoadLevel(int idx)
    {
        Level nLevel=Instantiate(m_Levels[idx], transform.position, Quaternion.identity);
        curentLevel=nLevel;
    }
    public void ChangeLevel()
    {
        Destroy(curentLevel);

        winMenu.MenuSwitch(true);

        curentLevelid += 1;
        if(curentLevelid >= m_Levels.Count) curentLevelid=m_Levels.Count-1;
        LoadLevel(curentLevelid);
        onNextLevel?.Invoke();
        player.OnDeath(true);
        soundManager.PlaySound(nextLvlSound);
    }
}
