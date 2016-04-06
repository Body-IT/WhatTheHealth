using UnityEngine;
using System.Collections;

public class angleMouseOn : MonoBehaviour {


    public GameObject movieView ;


    void Update() {
       
       
    }

    void onMouseOver()
    {
        movieView.SetActive(false);
    }

}
