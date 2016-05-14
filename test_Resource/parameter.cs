
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class parameter : MonoBehaviour {
	//start
	public static bool startFlag = false;
	//difine scene
	public static int myFlag=0;
	//distinguish scene 1->2, 3->2
	public static bool sflag;
	//count scene number
	public static bool cnt=true;
	
	//db array
	public static Text[] text = new Text[20];
	public static string[] ss = new string[20];
	public static int cnt;
}
