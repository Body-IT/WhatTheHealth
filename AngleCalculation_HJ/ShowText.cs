using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowText : MonoBehaviour {

    public GameObject shoulderLeft;
    public GameObject shoulderRight;
    public GameObject spine;
    public GameObject kneeLeft;
    public GameObject kneeRight;
    public GameObject elbowLeft;
    public GameObject elbowRight;

    public Vector2 shoulderLeftScreenPos;
    public Vector2 shoulderRightScreenPos;
    public Vector2 spineScreenPos;
    public Vector2 kneeLeftScreenPos;
    public Vector2 kneeRightScreenPos;
    public Vector2 elbowLeftScreenPos;
    public Vector2 elbowRightScreenPos;


    public GameObject mainCamera;
    public GameObject cubeman;
    private CubemanController cubemanController;
    private Vector2 screenPos;
    public GUISkin angleShowSkin;

    

  //  public void ShowAngle();

    // Use this for initialization
    void Start () {
        cubemanController = cubeman.GetComponent<CubemanController>();
    }
	
	// Update is called once per frame
	void Update () {
        AngleCalculation();
        //ShowAngle(spine, 1);
        ShowAngle(shoulderLeft,4);
        ShowAngle(shoulderRight,8);
        ShowAngle(kneeLeft,13);
        ShowAngle(kneeRight,17);
        ShowAngle(elbowLeft,5);
        ShowAngle(elbowRight,9);


    }

    public void AngleCalculation()
    {

    }
    public void ShowAngle(GameObject angleBetweenTwoVector, int index)
    {
        Camera camera = mainCamera.GetComponent<Camera>();

        Vector3 cameraPos = angleBetweenTwoVector.transform.position;


        Vector3 transCameraToWorldPos = new Vector3();

        //Change camera coordinate to world coordinate.
        transCameraToWorldPos.x = -cameraPos.x;
        transCameraToWorldPos.y = cameraPos.y;
        transCameraToWorldPos.z = -cameraPos.z;

        //Chage world position to screen position.
        if(index == 4)
            shoulderLeftScreenPos = camera.WorldToScreenPoint(transCameraToWorldPos);
        else if(index == 8)
            shoulderRightScreenPos = camera.WorldToScreenPoint(transCameraToWorldPos);
        //else if(index == 1)
        //    spineScreenPos = camera.WorldToScreenPoint(transCameraToWorldPos);
        else if (index == 13)
            kneeLeftScreenPos = camera.WorldToScreenPoint(transCameraToWorldPos);
        else if (index == 17)
            kneeRightScreenPos = camera.WorldToScreenPoint(transCameraToWorldPos);
        else if (index == 5)
            elbowLeftScreenPos = camera.WorldToScreenPoint(transCameraToWorldPos);
        else if (index == 9)
            elbowRightScreenPos = camera.WorldToScreenPoint(transCameraToWorldPos);

    //OnGUI(screenPos);
    //  Debug.Log(screenPos);
    //screenPos.x = screenPos.x + 30;
    //screenPos.y = screenPos.y - 30;



}

    void OnGUI()
    {

        // text.transform.localPosition = screenPos;
        // text.text = "angle : " + cubemanController.angleInt;

        GUI.skin = angleShowSkin;
        GUI.TextField(new Rect(shoulderLeftScreenPos, new Vector2(60, 40)), cubemanController.shoulderLeftAngle.ToString());
       GUI.TextField(new Rect(shoulderRightScreenPos, new Vector2(60, 40)), cubemanController.shoulderRightAngle.ToString());
        GUI.TextField(new Rect(kneeLeftScreenPos, new Vector2(60, 40)), cubemanController.kneeLeftAngle.ToString());
        GUI.TextField(new Rect(kneeRightScreenPos, new Vector2(60, 40)), cubemanController.kneeRightAngle.ToString());
        GUI.TextField(new Rect(elbowLeftScreenPos, new Vector2(60, 40)), cubemanController.elbowLeftAngle.ToString());
        GUI.TextField(new Rect(elbowRightScreenPos, new Vector2(60, 40)), cubemanController.elbowRightAngle.ToString());
    }

}