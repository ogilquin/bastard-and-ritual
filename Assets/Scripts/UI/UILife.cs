using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILife : MonoBehaviour {
    public Image empty;
    public Image full;

	public void SetupLifeUI(Color color){
        this.full.color = color;
    }

    public void ShowLife(float life, float total)
    {
        this.full.fillAmount = 1f / total * life;
    }
}
