using UnityEngine;
//using Windows.Kinect;
using System.Collections;
using UnityEngine.UI;
using System;


public class CubemanController : MonoBehaviour 
{
	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("Whether the cubeman is allowed to move vertically or not.")]
	public bool verticalMovement = true;

	[Tooltip("Whether the cubeman is facing the player or not.")]
	public bool mirroredMovement = false;

	[Tooltip("Rate at which the cubeman will move through the scene.")]
	public float moveRate = 1f;

    //public GameObject debugText;







    public GameObject Hip_Center;
	public GameObject Spine;
	public GameObject Neck;
	public GameObject Head;
	public GameObject Shoulder_Left;
	public GameObject Elbow_Left;
	public GameObject Wrist_Left;
	public GameObject Hand_Left;
	public GameObject Shoulder_Right;
	public GameObject Elbow_Right;
	public GameObject Wrist_Right;
	public GameObject Hand_Right;
	public GameObject Hip_Left;
	public GameObject Knee_Left;
	public GameObject Ankle_Left;
	public GameObject Foot_Left;
	public GameObject Hip_Right;
	public GameObject Knee_Right;
	public GameObject Ankle_Right;
	public GameObject Foot_Right;
	public GameObject Spine_Shoulder;
    public GameObject Hand_Tip_Left;
    public GameObject Thumb_Left;
    public GameObject Hand_Tip_Right;
    public GameObject Thumb_Right;
    public GameObject testText;
    public Text realText;
	
	public LineRenderer skeletonLine;
	public LineRenderer debugLine;

	private GameObject[] bones;
	private LineRenderer[] lines;

	private LineRenderer lineTLeft;
	private LineRenderer lineTRight;
	private LineRenderer lineFLeft;
	private LineRenderer lineFRight;

	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private Vector3 initialPosOffset = Vector3.zero;
	private Int64 initialPosUserID = 0;
    public Vector3 test = Vector3.zero;


    public Vector3 Hip_Center0;
    public Vector3 Spine1;
    public Vector3 Neck2;
    public Vector3 Head3;
    public Vector3 Shoulder_Left4;
    public Vector3 Elbow_Left5;
    public Vector3 Wrist_Left6;
    public Vector3 Hand_Left7;
    public Vector3 Shoulder_Right8;
    public Vector3 Elbow_Right9;
    public Vector3 Wrist_Right10;
    public Vector3 Hand_Right11;
    public Vector3 Hip_Left12;
    public Vector3 Knee_Left13;
    public Vector3 Ankle_Left14;
    public Vector3 Foot_Left15;
    public Vector3 Hip_Right16;
    public Vector3 Knee_Right17;
    public Vector3 Ankle_Right18;
    public Vector3 Foot_Right19;
    public Vector3 Spine_Shoulder20;
    public Vector3 Hand_Tip_Left21;
    public Vector3 Thumb_Left22;
    public Vector3 Hand_Tip_Right23;
    public Vector3 Thumb_Right24;

    public float v1mag1;
    public Vector3 v1norm1;
    public float v2mag1;
    public Vector3 v2norm1;
    public float res1;


    public int shoulderLeftAngle=0;
    public int shoulderRightAngle = 0;
    public int kneeLeftAngle = 0;
    public int kneeRightAngle = 0;
    public int elbowLeftAngle = 0;
    public int elbowRightAngle = 0;
    public int backAngle = 0;

    public float angleRes;


    void Start () 
	{
       // a = 0;
        //store bones in a list for easier access
        bones = new GameObject[] {
			Hip_Center,
            Spine,
            Neck,
            Head,
            Shoulder_Left,
            Elbow_Left,
            Wrist_Left,
            Hand_Left,
            Shoulder_Right,
            Elbow_Right,
            Wrist_Right,
            Hand_Right,
            Hip_Left,
            Knee_Left,
            Ankle_Left,
            Foot_Left,
            Hip_Right,
            Knee_Right,
            Ankle_Right,
            Foot_Right,
            Spine_Shoulder,
            Hand_Tip_Left,
            Thumb_Left,
            Hand_Tip_Right,
            Thumb_Right,
      
        };



        // array holding the skeleton lines
        lines = new LineRenderer[bones.Length];
		
//		if(skeletonLine)
//		{
//			for(int i = 0; i < lines.Length; i++)
//			{
//				Debug.Log ("Line: " + i + " instantiate started.");
//
//				if((i == 22 || i == 24) && debugLine)
//					lines[i] = Instantiate(debugLine) as LineRenderer;
//				else
//					lines[i] = Instantiate(skeletonLine) as LineRenderer;
//
//				lines[i].transform.parent = transform;
//			}
//		}

		initialPosition = transform.position;
		initialRotation = transform.rotation;
		//transform.rotation = Quaternion.identity;
	}
	

	void Update ()
	{

		KinectManager manager = KinectManager.Instance;
		
		// get 1st player
        //HJ//user id is person count.
		Int64 userID = manager ? manager.GetUserIdByIndex(playerIndex) : 0;
		
		if(userID <= 0)
		{
			initialPosUserID = 0;
			initialPosOffset = Vector3.zero;

			// reset the pointman position and rotation
			if(transform.position != initialPosition)
			{
				transform.position = initialPosition;
			}
			
			if(transform.rotation != initialRotation)
			{
				transform.rotation = initialRotation;
			}
           
            //initial print bone
            
			for(int i = 0; i < bones.Length; i++) 
			{
				bones[i].gameObject.SetActive(true);
                //스켈레톤 bone 초기화
				bones[i].transform.localPosition = Vector3.zero;
				bones[i].transform.localRotation = Quaternion.identity;
				
				if(lines[i] != null)
				{
					lines[i].gameObject.SetActive(false);
				}
			}
            

			return;
		}
		
		// set the position in space
        //bodyFrame.bodyData[index].position
		Vector3 posPointMan = manager.GetUserPosition(userID);
		Vector3 posPointManMP = new Vector3(posPointMan.x, posPointMan.y, !mirroredMovement ? -posPointMan.z : posPointMan.z);
		
		// store the initial position
		if(initialPosUserID != userID)
		{
			initialPosUserID = userID;
			//initialPosOffset = transform.position - (verticalMovement ? posPointMan * moveRate : new Vector3(posPointMan.x, 0, posPointMan.z) * moveRate);
			initialPosOffset = posPointMan;
		}

        //posPointMan 과 relPosUser의 차이는 무엇인가.
		Vector3 relPosUser = (posPointMan - initialPosOffset);
		relPosUser.z =!mirroredMovement ? -relPosUser.z : relPosUser.z;

		transform.position = initialPosOffset + 
			(verticalMovement ? relPosUser * moveRate : new Vector3(relPosUser.x, 0, relPosUser.z) * moveRate);
		
		// update the local positions of the bones
		for(int i = 0; i < bones.Length; i++) 
		{
			if(bones[i] != null)
			{
				int joint = !mirroredMovement ? i : (int)KinectInterop.GetMirrorJoint((KinectInterop.JointType)i);
				if(joint < 0)
					continue;
				
				if(manager.IsJointTracked(userID, joint))
				{
					bones[i].gameObject.SetActive(true);

                   
                    //getJointJinectPosition과 getJointPosition의 차이는?
                    Vector3 posJoint = manager.GetJointPosition(userID, joint);
					posJoint.z = !mirroredMovement ? -posJoint.z : posJoint.z;
					
					Quaternion rotJoint = manager.GetJointOrientation(userID, joint, !mirroredMovement);
					rotJoint = initialRotation * rotJoint;

					posJoint -= posPointManMP;
					
					if(mirroredMovement)
					{
						posJoint.x = -posJoint.x;
						posJoint.z = -posJoint.z;
					}
                

                    if (i == 0)
                        Hip_Center0 = posJoint;
                    else if (i == 1)
                        Spine1 = posJoint;
                    else if (i == 2)
                        Neck2 = posJoint;
                    else if (i == 3)
                        Head3 = posJoint;
                    else if (i == 4)
                        Shoulder_Left4 = posJoint;
                    else if (i == 5)
                        Elbow_Left5 = posJoint;
                    else if (i == 6)
                        Wrist_Left6 = posJoint;
                    else if (i == 7)
                        Hand_Left7 = posJoint;
                    else if (i == 8)
                        Shoulder_Right8 = posJoint;
                    else if (i == 9)
                        Elbow_Right9= posJoint;
                    else if (i == 10)
                        Wrist_Right10 = posJoint;
                    else if (i == 11)
                        Hand_Right11 = posJoint;
                    else if (i == 12)
                        Hip_Left12 = posJoint;
                    else if (i == 13)
                        Knee_Left13 = posJoint;
                    else if (i == 14)
                        Ankle_Left14 = posJoint;
                    else if (i == 15)
                        Foot_Left15 = posJoint;
                    else if (i == 16)
                        Hip_Right16 = posJoint;
                    else if (i == 17)
                        Knee_Right17 = posJoint;
                    else if (i == 18)
                        Ankle_Right18 = posJoint;
                    else if (i == 19)
                        Foot_Right19 = posJoint;
                    else if (i == 20)
                        Spine_Shoulder20 = posJoint;

                    //Calculate joint angle using 4 coordinate of point.
                    shoulderLeftAngle       =   angleCal(Spine_Shoulder20,Spine1, Shoulder_Left4, Elbow_Left5);
                    shoulderRightAngle      =   angleCal(Spine_Shoulder20, Spine1, Shoulder_Right8, Elbow_Right9);
                    kneeLeftAngle           =   angleCal(Knee_Left13, Hip_Left12, Knee_Left13, Ankle_Left14);
                    kneeRightAngle          =   angleCal(Knee_Right17, Hip_Right16, Knee_Right17, Ankle_Right18);
                    elbowLeftAngle          =   angleCal(Elbow_Left5, Shoulder_Left4, Elbow_Left5, Wrist_Left6);
                    elbowRightAngle         =   angleCal(Elbow_Right9, Shoulder_Right8, Elbow_Right9, Wrist_Right10);
                    backAngle               =   180-angleCal(Spine1, Spine_Shoulder20, Spine1, Hip_Center0);

                    bones[i].transform.localPosition = posJoint;
                    bones[i].transform.rotation = rotJoint;
   
                    
                    /*
                    // print skeleton line to sceen 
                    
                    if (lines[i] == null && skeletonLine != null) 
					{
						lines[i] = Instantiate((i == 22 || i == 24) && debugLine ? debugLine : skeletonLine) as LineRenderer;
						lines[i].transform.parent = transform;
					}

					if(lines[i] != null)
					{
						lines[i].gameObject.SetActive(true);
						Vector3 posJoint2 = bones[i].transform.position;
						
						Vector3 dirFromParent = manager.GetJointDirection(userID, joint, false, false);
						dirFromParent.z = !mirroredMovement ? -dirFromParent.z : dirFromParent.z;
						Vector3 posParent = posJoint2 - dirFromParent;
						
						//lines[i].SetVertexCount(2);
						lines[i].SetPosition(0, posParent);
						lines[i].SetPosition(1, posJoint2);
					}
                    */

				}
				else
				{
					bones[i].gameObject.SetActive(false);
					
					if(lines[i] != null)
					{
						lines[i].gameObject.SetActive(false);
					}
				}
			}	
		}
	}



    /// <summary>
    /// Calculate joint angle using 4 coordinate of point.
    /// </summary>
    /// <returns>Joint angle</returns>
    /// <param name="a">coordinate of point a</param>
    /// <param name="b">coordinate of point b</param>
    /// <param name="c">coordinate of point c</param>
    /// <param name="d">coordinate of point d</param>
    int angleCal(Vector3 a, Vector3 b, Vector3 c , Vector3 d )
    {
       
        Vector3 v1 = new Vector3((b.x - a.x), (b.y - a.y), (b.z - a.z));
        Vector3 v2 = new Vector3((d.x - c.x), (d.y - c.y), (d.z - c.z));
        float angleFloat = Vector3.Angle(v1, v2);

        return (int)angleFloat;
    }

}


