using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

    public float coldTime = 2;
    private UISprite sprite ;
    public bool isColding = false;

	void Start () {
        sprite = transform.Find("Sprite").GetComponent<UISprite>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.U) && !isColding)
        {
            //1.展示技能效果

            //2.技能冷却
            sprite.fillAmount = 1;          
            isColding = true;
        }

        if (isColding)
        {
            sprite.fillAmount -= (1.0f / coldTime) * Time.deltaTime;
            if (sprite.fillAmount <= 0.001f)
            {
                isColding = false;
            }
        }
	}
}
