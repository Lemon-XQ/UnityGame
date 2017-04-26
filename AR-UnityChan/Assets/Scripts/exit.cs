using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class exit : MonoBehaviour {

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>Application.Quit());
    }

}
