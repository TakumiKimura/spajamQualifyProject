﻿using UnityEngine;
using System.Collections;

public class DeleteZoneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            Destroy(col.gameObject);
        }
    }
}
