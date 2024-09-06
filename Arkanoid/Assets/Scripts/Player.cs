using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float yKat;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int lifes = 3;
    public int factlifes {  get; private set; }

    [SerializeField] private float angleCoof = 0.5f;

    private int factScore;
    private int scoreBonus;
    [SerializeField] private int maxBonus = 4;

    public event ComboText onComboText;
    public delegate void ComboText(int bonus);

    public event Score onGetScore;
    public delegate void Score(int scorePlus, int score);

    private PlayerController playerController;

    public event Ded onDed;
    public delegate void Ded();

    public event HPchange onHPchange;
    public delegate void HPchange();

    public List<BonusEffect> myBonuses;

    public static Player Instance { get; private set; }

    private SoundManager soundManager;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip bonusSound;

    [SerializeField] private PopupMenu deathMenu;
    private void Awake()
    {
        Instance = this;
        factlifes = lifes;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        yKat = spriteRenderer.size.x * 0.5f;

        playerController = PlayerController.Instance;
        playerController.onMousePressed += CalculateScoreBonus;

        soundManager=SoundManager.Instance;

        factScore = 0;
        scoreBonus = 1;

        deathMenu.MenuSwitch(false);
    }
    public float GetAngle(Vector2 hitPos)
    {
        float xKat=hitPos.x-transform.position.x;
        float angle = Mathf.Atan(xKat / yKat) * Mathf.Rad2Deg*-1 * angleCoof;
        //angle=-Mathf.Sign(angle)*90;
        Debug.Log(angle);
        //if (Mathf.Abs(angle % 90) < 2 || Mathf.Abs((angle % 90)-90) < 2) angle += 5;
        
        return angle;
    }
    private void CalculateScoreBonus(float angle, float force)
    {
        float forceDif = playerController.maxForce - playerController.minForce;
        float step = forceDif / maxBonus;
        scoreBonus = 1;
        float diapason = playerController.minForce + step;
        while (diapason < force)
        {
            scoreBonus += 1;
            diapason += step;
        }
        onComboText?.Invoke(scoreBonus);
    }
    public void GetScore(int score)
    {
        int scorePlus = score * scoreBonus;
        factScore += scorePlus;
        onGetScore?.Invoke(scorePlus, factScore);

    }

    public IEnumerator BonusTimer(float time, BonusEffect bonus)
    {
        bool canUse = true;
        foreach(BonusEffect myBonus in myBonuses)
        {
            if (myBonus == bonus)
            {
                canUse = false;
                break;
            }
        }
        if (canUse)
        {
            soundManager.PlaySound(bonusSound);
            bonus.ApplyEffect();
            myBonuses.Add(bonus);
            yield return new WaitForSeconds(time);
            bonus.DisableEffect();
            myBonuses.Remove(bonus);
        }
    }
    public void ChangeHP(int amount)
    {
        factlifes +=amount;
        onHPchange?.Invoke();
    }
    public void OnDeath(bool dontChangeHP=false)
    {
        if (!dontChangeHP)
        {
            ChangeHP(-1);
            soundManager.PlaySound(deathSound);
        }
        if (factlifes <= 0)
        {
            deathMenu.MenuSwitch(true);
        }
        onDed?.Invoke();
        Debug.Log("DED");
    }
}
