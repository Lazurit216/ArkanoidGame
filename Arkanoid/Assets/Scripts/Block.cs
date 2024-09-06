using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] protected int HP;
    protected int factHP;
    [SerializeField] protected int scoreCost;
    private Player player;
    public Level parentLevel;
    [SerializeField] private List<GameObject> breakGrades;
    private int lastIdx;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    private SoundManager soundManager;
    [SerializeField] private ParticleSystem babah;

    protected virtual void Start()
    {
        soundManager = SoundManager.Instance;
        player = Player.Instance;
        factHP = HP;

        foreach (GameObject grade in breakGrades) grade.SetActive(false);
        lastIdx = 0;
    }
    public virtual void OnHit()
    {
        GetDamage();
    }
    protected virtual void GetDamage()
    {
        factHP -= 1;
        if (factHP <= 0)
        {
            OnDeath();
            return;
        }

        soundManager.PlaySound(damageSound);

        int idx=breakGrades.Count-factHP;
        if (idx < 0) idx = 0;

        for (int i = lastIdx; i <=idx; i += 1)
        {
            breakGrades[i].SetActive(true);
        }
        lastIdx = idx;

    }
    protected virtual void OnDeath()
    {
        soundManager.PlaySound(deathSound);
        Instantiate(babah, transform.position, Quaternion.Euler(90,0,0));

        player.GetScore(scoreCost);
        parentLevel.ChangeCount();

        Destroy(gameObject);
    }
}
