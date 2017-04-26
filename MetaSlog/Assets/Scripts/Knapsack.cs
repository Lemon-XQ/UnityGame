using UnityEngine;
using System.Collections;

public class Knapsack : MonoBehaviour
{
    public GameObject[] cells;
    public string[] equipmentsName;
    public GameObject item;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        int index = Random.Range(0,equipmentsName.Length);
        string name = equipmentsName[index];
        bool isFind = false;
        
        for (int i = 0; i < cells.Length; ++i)
        {
            if (cells[i].transform.childCount > 0)//判断当前格子有无物品
            {
                MydragdropItem item=cells[i].GetComponentInChildren<MydragdropItem>();
                //判断当前游戏物品的名字是否与捡到的游戏物体名字一样
                if (item.sprite.spriteName == name)
                {
                    isFind = true;
                    item.AddCount(1);
                    break;
                }
            }
        }

        if (isFind == false)
        {
            for (int i = 0; i < cells.Length; ++i)
            {
                if (cells[i].transform.childCount == 0)//当前位置没有物品
                {
                    GameObject go = NGUITools.AddChild(cells[i],item);//实例化
                    go.GetComponent<UISprite>().spriteName = name;
                    go.transform.localPosition = Vector3.zero;
                    break;
                }
            }
        }

    } 
}
