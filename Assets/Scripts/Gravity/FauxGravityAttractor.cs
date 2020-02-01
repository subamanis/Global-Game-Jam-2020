using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {

	public static FauxGravityAttractor instance;

	private SphereCollider col;

	void Awake ()
	{
		instance = this;
		col = GetComponent<SphereCollider>();
	}

	//public float gravity = -10f;

	public void Attract (Rigidbody body)
	{
		//Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 gravityUp = (transform.position - body.position);

		var totalForce = gravityUp;
		body.AddForce(totalForce);

		Debug.DrawRay(body.position, totalForce, Color.blue);

		RotateBody(body);
	}

	public void PlaceOnSurface (Rigidbody body)
	{
		body.MovePosition((body.position - transform.position).normalized * (transform.localScale.x * col.radius));

		RotateBody(body);
	}

	void RotateBody (Rigidbody body)
	{
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Quaternion targetRotation = Quaternion.FromToRotation(body.transform.up, gravityUp) * body.rotation;
		body.MoveRotation (Quaternion.Slerp(body.rotation, targetRotation, 50f * Time.deltaTime));
	}

}
