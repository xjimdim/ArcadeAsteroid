using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


	public Transform myTarget;

	// this script is not used yet - just an idea 
	// Update is called once per frame
	void Update () {
		if (myTarget != null) {
			Vector3 targPos = myTarget.position;
			targPos.z = transform.position.z;


			transform.position = Vector3.Lerp (transform.position, targPos, 2f * Time.deltaTime);
			//transform.position = targPos;
		}
	}
}
