using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class depth_test : MonoBehaviour {
	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("Camera that will be used to overlay the 3D-objects over the background.")]
	public Camera foregroundCamera;

	// the KinectManager instance
	private KinectManager manager;

	// the foreground texture
	private Texture2D foregroundTex;

	// rectangle taken by the foreground texture (in pixels)
	private Rect foregroundGuiRect;
	private Rect foregroundImgRect;

	// game objects to contain the joint colliders
	private GameObject[] jointColliders = null;
	private int numColliders = 0;

	private int depthImageWidth;
	private int depthImageHeight;


	private Vector3 depthImageWidth1;
	private int depthImageHeight1;

	public Text xText;
	public Text depthText;
	public Text _Time;
	public float _timeCnt=0;
	public string strTime;

	public int temp;
	// Use this for initialization
	void Start () {
		temp = 0;
		manager = KinectManager.Instance;

		if(manager && manager.IsInitialized())
		{
			KinectInterop.SensorData sensorData = manager.GetSensorData();

			if(sensorData != null && sensorData.sensorInterface != null && foregroundCamera != null)
			{
				// get depth image size
				depthImageWidth = sensorData.depthImageWidth;
				depthImageHeight = sensorData.depthImageHeight;

				// calculate the foreground rectangles
				Rect cameraRect = foregroundCamera.pixelRect;
				float rectHeight = cameraRect.height;
				float rectWidth = cameraRect.width;

				if(rectWidth > rectHeight)
					rectWidth = rectHeight * depthImageWidth / depthImageHeight;
				else
					rectHeight = rectWidth * depthImageHeight / depthImageWidth;

				float foregroundOfsX = (cameraRect.width - rectWidth) / 2;
				float foregroundOfsY = (cameraRect.height - rectHeight) / 2;
				foregroundImgRect = new Rect(foregroundOfsX, foregroundOfsY, rectWidth, rectHeight);
				foregroundGuiRect = new Rect(foregroundOfsX, cameraRect.height - foregroundOfsY, rectWidth, -rectHeight);

				// create joint colliders
				numColliders = sensorData.jointCount;
				jointColliders = new GameObject[numColliders];

				for(int i = 0; i < numColliders; i++)
				{
					string sColObjectName = ((KinectInterop.JointType)i).ToString() + "Collider";
					jointColliders[i] = new GameObject(sColObjectName);
					jointColliders[i].transform.parent = transform;

					SphereCollider collider = jointColliders[i].AddComponent<SphereCollider>();
					collider.radius = 0.2f;
				}
			}
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (manager && manager.IsInitialized ()) {
			foregroundTex = manager.GetUsersLblTex ();
		}

		if (manager && manager.IsUserDetected () && foregroundCamera) {
			long userId = manager.GetUserIdByIndex (playerIndex);  // manager.GetPrimaryUserID();
			Vector3 posUser = manager.GetUserPosition (userId);
			//Vector3 posUser = manager.GetJointKinectPosition(userId,24);
			//KinectInterop.DepthSensorPlatform sensor = manager.GetRawDepthMap ();
			float posZ = posUser.z * 100;
			float posX = posUser.x * 100;


			if (posZ >= 100 && posZ <= 200 && posX <= 50 && posX >= -50) {
				if (SceneMove.sflag == true) {
					KinectManager.myFlag = 2;
				}
				_timeCnt += Time.deltaTime;
				int min = (int)(_timeCnt / 60) % 60;
				float printTime = _timeCnt;
				if (printTime >= 60)
					printTime -= 60;
				strTime = min.ToString ("00") + ":" + printTime.ToString("00.00");
				_Time.text = "Time : " + strTime;
				depthText.text = "depth: " + posZ.ToString ();
				xText.text = "X : " + posX.ToString ();
				temp = 1;
					
			} else {
				depthText.text = "depth: X";
				xText.text = "X : X";
				if (temp == 1) {
					KinectManager.myFlag = 1;
				}
			}

		}

	}
}