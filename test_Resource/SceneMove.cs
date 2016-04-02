using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneMove : MonoBehaviour {

	public GameObject KinectController;

	public static bool sflag;
	
	// Update is called once per frame
	void Update () {
		if (KinectManager.myFlag == 1) {
			KinectManager.myFlag = 0;
			sflag = true;
			KinectController.SetActive(false);
			SceneManager.LoadScene ("KinectDemos/ColliderDemo/DepthColliderDemo");
		} else if (KinectManager.myFlag == 2) {
			KinectManager.myFlag = 0;
			sflag = false;
			KinectController.SetActive(false);
			SceneManager.LoadScene ("KinectDemos/RecorderDemo/KinectRecorderDemo");
		}
	}
}