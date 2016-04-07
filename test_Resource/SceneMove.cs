using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneMove : MonoBehaviour {

	public GameObject kincetcontroller;

	// Update is called once per frame
	void Update () {
		if (parameter.startFlag == true) {
			parameter.startFlag = false;
			DontDestroyOnLoad (kincetcontroller);
			SceneManager.LoadScene ("KinectDemos/FittingRoomDemo/KinectFittingRoom1");
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			parameter.cnt=false;
			Debug.Log (parameter.cnt);
			DontDestroyOnLoad (kincetcontroller);
			SceneManager.LoadScene ("KinectDemos/FittingRoomDemo/KinectFittingRoom1");
		}  
		if (Input.GetKey(KeyCode.RightArrow)) {
			parameter.cnt=false;
			Debug.Log (parameter.cnt);
			DontDestroyOnLoad (kincetcontroller);
			SceneManager.LoadScene ("KinectDemos/FittingRoomDemo/KinectFittingRoom2");
		}
		if (parameter.myFlag == 1) {
			parameter.myFlag = 0;
			parameter.sflag = true;
			SceneManager.LoadScene ("KinectDemos/ColliderDemo/DepthColliderDemo");
		} else if (parameter.myFlag == 2) {
			parameter.myFlag = 0;
			parameter.sflag = false;
			SceneManager.LoadScene ("KinectDemos/RecorderDemo/KinectRecorderDemo");
		}
	}
}