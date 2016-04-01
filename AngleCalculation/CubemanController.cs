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

    public Vector3 v1;
    public Vector3 v2;
    public float v1mag1;
    public Vector3 v1norm1;
    public float v2mag1;
    public Vector3 v2norm1;
    public float res1;
    public float angleFloat;
    public int angleInt;
    public float angleRes;


    void Start () 
	{


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
            //bone.Length = 25
			for(int i = 0; i < bones.Length; i++) 
			{
				bones[i].gameObject.SetActive(true);
                //½ºÄÌ·¹Åæ bone ÃÊ±âÈ­
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
		Vector3 posPointMan = manager.GetUserPosition(userID);
		Vector3 posPointManMP = new Vector3(posPointMan.x, posPointMan.y, !mirroredMovement ? -posPointMan.z : posPointMan.z);
		
		// store the initial position
		if(initialPosUserID != userID)
		{
			initialPosUserID = userID;
			//initialPosOffset = transform.position - (verticalMovement ? posPointMan * moveRate : new Vector3(posPointMan.x, 0, posPointMan.z) * moveRate);
			initialPosOffset = posPointMan;
		}

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


                    if (i == 1)
                        Spine1 = posJoint;
                    else if (i == 4)
                        Shoulder_Left4 = posJoint;
                    else if (i == 5)
                        Elbow_Left5 = posJoint;
                    else if (i == 20)
                        Spine_Shoulder20 = posJoint;
                    else if (i == 0)
                        Hip_Center0 = posJoint;
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    v1 =new Vector3(Spine1.x - Spine_Shoulder20.x , Spine1.y - Spine_Shoulder20.y , Spine1.y - Spine_Shoulder20.y);
                    v2 =new Vector3(Elbow_Left5.x - Shoulder_Left4.x, Elbow_Left5.y - Shoulder_Left4.y, Elbow_Left5.y - Shoulder_Left4.y);

                    angleFloat = Vector3.Angle(v1, v2);
                   /* if (Vector3.Dot(Vector3.Cross(v1, v2), Vector3.up) >= 0.0)
                    {
                        angleFloat = 360.0f - angleFloat;
                        if (angleFloat > 359.9999f)
                            angleFloat -= 360.0f;
                    }*/
                    angleInt = (int)angleFloat;
                    // angleInt = (int) angleFloat - 39;

                    //Vector3 pos = cubemanController.Shoulder_Left4;
                    testText.transform.localPosition = Shoulder_Left4;
                 //   realText.transform.localPosition = Shoulder_Left4;
                 //   realText.text = "angle : " + cubemanController.angleInt;



                    /*
                                        //public float v1mag
                                         v1mag1 = ((v1.x * v1.x) + (v1.y * v1.y) + (v1.z * v1.z));
                                         v1mag1= (float)Math.Sqrt(v1mag1);
                                         v1norm1 = new Vector3(v1.x / v1mag1, v1.y / v1mag1, v1.z / v1mag1);
                                         //public float v2mag
                                         v2mag1 = ((v2.x * v2.x) + (v2.y * v2.y) + (v2.z * v2.z));
                                         v2mag1 = (float)Math.Sqrt(v2mag1);
                                         v2norm1 = new Vector3(v2.x / v2mag1, v2.y / v2mag1, v2.z / v2mag1);

                                         res1 = (v1norm1.x * v2norm1.x) + (v1norm1.y * v2norm1.y) + (v1norm1.z * v2norm1.z);
                                         angle1 = (float)Math.Acos(res1)*60;
                    */
                    //////////
                    //posJoint
                    /////////////////////////////////////////////////////////////
                    bones[i].transform.localPosition = posJoint;
                   // bones[i].transform.localPosition = Shoulder_Left4;
                    bones[i].transform.rotation = rotJoint;
                    ////////////////////////////////////////////////////////////


                    //////////////////////
                    ////////////////////////////////////
                    
                  /*  if (lines[i] == null && skeletonLine != null) 
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

}


