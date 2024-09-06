using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour //Script povedeniya strelki napravleniya poleta shara
{
    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    private float startScale = 1;

    //Dlya rascheta izmenenia razmera i cveta shara
    [SerializeField] private float scaleDif;
    [SerializeField] private float colordif = 100;

    [SerializeField] private Color lowColor = Color.green;
    [SerializeField] private Color maxColor = Color.green;

    private float forceDif;//raznica sili, po kotoroy prohodit sravnenie
    private PlayerController playerController;
    private void Start()
    {
        playerController=PlayerController.Instance;
        playerController.onMousePressed += ShowDir;
        HideSprite();

        transform.localScale=new Vector3 (1,startScale,1);
        forceDif = playerController.maxForce - playerController.minForce;

    }
    private void Update()
    {
        if(playerController.isStarted) HideSprite();
    }
    private void ShowDir(float angle, float force)
    {
        ShowSprite();
        transform.eulerAngles=new Vector3(0,0,angle);

        //rasschet novih parametrov: maximalnaya delta razmera i cveta delitsya na doli i umnojaetsa na shag sili
        float newYscale = scaleDif / forceDif * force;
        Color newColor = Color.Lerp(lowColor, maxColor, colordif / forceDif * force);

        //sprite strelki sostoit iz dvuh spritov
        foreach (SpriteRenderer spriteRenderer in spriteRenderers) spriteRenderer.color = newColor;
        transform.localScale = new Vector3(1, newYscale ,1);
    }
    private void ShowSprite()
    {
        foreach(SpriteRenderer sprite in spriteRenderers)
        {
            sprite.enabled = true;
        }
    }
    private void HideSprite()
    {
        foreach (SpriteRenderer sprite in spriteRenderers)
        {
            sprite.enabled = false;
        }
    }
}
