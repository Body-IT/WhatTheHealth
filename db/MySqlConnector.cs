using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MySqlConnector : MonoBehaviour {

	public string id;
	public int pwd;
	public string[] top10Scores;
	public string db_url="http://203.246.112.69/bodyIT/";      // this is the path to our xampp database folder
	public Text text;
	//public float _timeCnt=0;
	//public string strTime;

	IEnumerator Load()
	{
		WWWForm form = new WWWForm();

		id = "010";

		form.AddField ("newId", id);
		// we don't need to store any variable in this, just run the php file
		WWW webRequest = new WWW(db_url + "load_db.php",form);

		// now we wait again for the feedback of the command
		yield return webRequest;

		// this is a GUIText that will display the scores in game.
		text.text = webRequest.text;
		Debug.Log (webRequest.text);
	}
	IEnumerator Save()
	{
		// first we create a new WWWForm, that means a "post" command goes out to our database (for futher information just google "post" and "get" commands for html/php
		WWWForm form = new WWWForm();

		//id = System.DateTime.Now.ToString("yyMMdd.hhmm");
		id = "1";
		pwd = 432;
		// with this line we will give a new name and save our score into that name
		// those "" indicate a string and attach the score after the comma to it
		form.AddField("newId", id);
		form.AddField("newPwd", pwd);

		// the next line will start our php file that saves the Score and attaches the saved values from the "form" to it
		// For this tutorial I've used a new variable "db_url" that stores the path
		WWW webRequest = new WWW(db_url + "save_db.php", form);

		// with this line we'll wait until we get an info back
		yield return webRequest;
	}

	void Start(){
		//StartCoroutine (Load ());

		StartCoroutine (Save ());
		//text.text = id;
	}
		/*
		_timeCnt += Time.deltaTime;
		int min = (int)(_timeCnt / 60) % 60;
		float printTime = _timeCnt;
		if (printTime >= 60)
			printTime -= 60;
		strTime = min.ToString ("00") + ":" + printTime.ToString ("00.00");
		text.text = strTime;
		*/
	
}