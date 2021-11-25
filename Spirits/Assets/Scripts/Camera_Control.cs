using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    public float mapBoundX = 5.40f;
    public float mapBoundMaxY = -1f;
    public float mapBoundMinY = -5.25f;

    // Update is called once per frame
    void Update()
    {   
        if (GameObject.Find("Bartender") == null) return;
        Vector3 playerTracker = GameObject.Find("Bartender").transform.position;

        if (Mathf.Abs(playerTracker.x) <= mapBoundX)
            this.transform.position = new Vector3(playerTracker.x, this.transform.position.y, -10);
        if (playerTracker.y <= mapBoundMaxY && playerTracker.y >= mapBoundMinY)
            this.transform.position = new Vector3(this.transform.position.x, playerTracker.y, -10);
    }
}