using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    public string ResourceDir = "UI";

    //现有面板
    public Stack<UIBase> UIStack = new Stack<UIBase>();

    //保存当前的面板，防止重复创建
    private Dictionary<string, UIBase> currentUIDict = new Dictionary<string, UIBase>();

    private Dictionary<string, GameObject> UIObjectDict = new Dictionary<string, GameObject>();

    void Awake()
    {
        _instance = this;
        //LoadAllUIObject();
        AddUIBase("UIOption");
        AddUIBase("UIStart");
        AddUIBase("TMX_1_1");
        AddUIBase("UILose");
        AddUIBase("UIWin");
        AddUIBase("UILevel");
        AddUIBase("UIControl");
    }

    //入栈，显示界面
    public void PushUIPanel(string UIName)
    {
        if (UIStack.Count > 0)
        {
            UIBase old_topUI = UIStack.Peek();
            old_topUI.DoOnPausing();
        }
        UIBase new_topUI = GetUIBase(UIName);
        new_topUI.DoOnEntering();
        UIStack.Push(new_topUI);
    }

    private UIBase GetUIBase(string UIName) 
    {
        foreach (var name in currentUIDict.Keys)
        {
            if (name == UIName)
            {
                UIBase u = currentUIDict[UIName];
                return u;
            }
        }
        //如果没有，就先得到面板的prefab
        GameObject UIPrefab = UIObjectDict[UIName];
        GameObject UIObject = GameObject.Instantiate<GameObject>(UIPrefab);
        UIObject.name = UIName; //不让名字以clone结尾
        //创建面板,默认以canvas为parent，不用另外创建了
        //UIObject.transform.SetParent(UIParent, false);
        UIBase uibase = UIObject.GetComponent<UIBase>();
        currentUIDict.Add(UIName,uibase);
        return uibase;

    }

    //出栈，隐藏界面
    public void PopUIPanel()
    {
        if (UIStack.Count == 0)
            return;
        UIBase old_topUI = UIStack.Pop();
        old_topUI.DoOnExit();
        if (UIStack.Count > 0)
        {
            UIBase new_topUI = UIStack.Peek();
            new_topUI.DoOnResuming();
        }
    }

    //private void LoadAllUIObject()
    //{
    //    string path = Application.dataPath + "/Game/Resources/" + ResourceDir;
    //    DirectoryInfo folder = new DirectoryInfo(path);
    //    foreach (FileInfo file in folder.GetFiles("*.prefab"))
    //    {
    //        int index = file.Name.LastIndexOf('.');
    //        string UIName = file.Name.Substring(0,index);
    //        string UIPath = ResourceDir + "/" + UIName;
    //        GameObject UIObject = Resources.Load<GameObject>(UIPath);
    //        UIObjectDict.Add(UIName, UIObject);
    //    }
    //}

    public void AddUIBase(string UIName)
    {
        string path = ResourceDir + "/" + UIName;
        GameObject UIObject =  Resources.Load<GameObject>(path);
        if (UIObject)
            UIObjectDict.Add(UIName, UIObject);
    }

}
