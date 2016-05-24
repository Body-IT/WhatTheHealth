using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonclick : MonoBehaviour {
    public InputField mainInputField;
    public GameObject login;
    public GameObject record;
    public GameObject input;

    // Use this for initialization
    void Start () {
        
    }
    public void clickLogin()
    {
        parameter.id = mainInputField.text;
        parameter.loginFlag = 1;
        Debug.Log(parameter.id);
        login.SetActive(false);
        input.SetActive(false);
        record.SetActive(true);

    }
    public void clickRecord()
    {
        parameter.dbView = true;
        parameter.lFlag = true;
    }
    public void clickLogout()
    {
        parameter.loginFlag = 0;
        parameter.startFlag = true;
    }
    public void clickExit()
    {
        Debug.Log("h");
        parameter.startFlag = true;
    }
    // Update is called once per frame
    void Update()
    {

    }


}
