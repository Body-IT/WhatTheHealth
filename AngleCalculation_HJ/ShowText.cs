using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowText : MonoBehaviour {

    //각도 텍스트 띄우는 위치
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

    private int sortingNum = 1;

    //  public void ShowAngle();

    // Use this for initialization
    void Start() {
        cubemanController = cubeman.GetComponent<CubemanController>();
    }

    // Update is called once per frame
    void Update() {

        spineScreenPos = ChangeCoordinate(spine);
        shoulderLeftScreenPos = ChangeCoordinate(shoulderLeft);
        shoulderRightScreenPos = ChangeCoordinate(shoulderRight);
        kneeLeftScreenPos = ChangeCoordinate(kneeLeft);
        kneeRightScreenPos = ChangeCoordinate(kneeRight);
        elbowLeftScreenPos = ChangeCoordinate(elbowLeft);
        elbowRightScreenPos = ChangeCoordinate(elbowRight);

    }


    /// <summary>
    /// Change kinect's camera coordinate to Screen coordinate.
    /// </summary>
    /// <returns>Screen coordinate of point</returns>
    /// <param name="angleBetweenTwoVector">angleBetweenTwoVector</param>
    public Vector2 ChangeCoordinate(GameObject angleBetweenTwoVector)
    {
        Camera camera = mainCamera.GetComponent<Camera>();
        Vector3 cameraPos = angleBetweenTwoVector.transform.position;
        Vector3 worldPos = new Vector3();
        Vector3 screenPos = new Vector3();

        //Change camera coordinate to world coordinate.
        worldPos = transCameraToWorldPos(cameraPos);

        //Chage world position to screen position.
        screenPos = camera.WorldToScreenPoint(worldPos);

        return screenPos;
    }

    /// <summary>
    /// Change camera position to world position
    /// </summary>
    /// <returns>world position</returns>
    /// <param name="cameraPos">cameraPos</param>
    public Vector3 transCameraToWorldPos(Vector3 cameraPos)
    {
        Vector3 worldPosition;
        worldPosition.x = -cameraPos.x;
        worldPosition.y = cameraPos.y;
        worldPosition.z = -cameraPos.z;
        return worldPosition;
    }

    void OnGUI()
    {
        GUISkinChange();
        GUIPrintText();
        //if (cubemanController.shoulderLeftAngle>70)
       // {
       //    GUI.TextField.
       // }
    }

    public void GUISkinChange()
    {
        GUI.skin = angleShowSkin;
        GUI.skin.textField.normal.background = null;
        GUI.skin.textField.hover.background = null;
        GUI.skin.textField.focused.background = null;
    }
    public void GUIPrintText()
    {
        GUI.TextField(new Rect(spineScreenPos, new Vector2(60, 40)), cubemanController.backAngle.ToString());
        GUI.TextField(new Rect(shoulderLeftScreenPos, new Vector2(60, 40)), cubemanController.shoulderLeftAngle.ToString());
        GUI.TextField(new Rect(shoulderRightScreenPos, new Vector2(60, 40)), cubemanController.shoulderRightAngle.ToString());
        GUI.TextField(new Rect(kneeLeftScreenPos, new Vector2(60, 40)), cubemanController.kneeLeftAngle.ToString());
        GUI.TextField(new Rect(kneeRightScreenPos, new Vector2(60, 40)), cubemanController.kneeRightAngle.ToString());
        GUI.TextField(new Rect(elbowLeftScreenPos, new Vector2(60, 40)), cubemanController.elbowLeftAngle.ToString());
        GUI.TextField(new Rect(elbowRightScreenPos, new Vector2(60, 40)), cubemanController.elbowRightAngle.ToString());
    }
    public void GUIFeedbackColor()
    {
        if (cubemanController.shoulderLeftAngle > 70)
            GUI.skin.textField.normal.textColor = new Color(0, 80, 0, (float)0.9);
        else
            GUI.skin.textField.normal.textColor = Color.white;
    }
}