using UnityEngine;

public class Skin : MonoBehaviour {
    public SpriteRenderer avantBrasGauche;
    public SpriteRenderer botteDroit;
    public SpriteRenderer botteGauche;
    public SpriteRenderer brasGauche;
    public SpriteRenderer corp;
    public SpriteRenderer epauletteDroit;
    public SpriteRenderer epauletteGaucheA;
    public SpriteRenderer epauletteGaucheB;
    public SpriteRenderer jambeDroit;
    public SpriteRenderer jambeGauche;
    
    public void Skining(int playerSkin)
    {
        if(GameManager.instance.playerSkins.Length > 0 && GameManager.instance.playerSkins.Length >= playerSkin)
        {
            PlayerSkin skin = GameManager.instance.playerSkins[playerSkin-1];
            avantBrasGauche.sprite = skin.avantBrasGauche;
            botteDroit.sprite = skin.botteDroit;
            botteGauche.sprite = skin.botteGauche;
            brasGauche.sprite = skin.brasGauche;
            corp.sprite = skin.corp;
            epauletteDroit.sprite = skin.epauletteDroit;
            epauletteGaucheA.sprite = skin.epauletteGaucheA;
            epauletteGaucheB.sprite = skin.epauletteGaucheB;
            jambeDroit.sprite = skin.jambeDroit;
            jambeGauche.sprite = skin.jambeGauche;
        }
    }
}

        