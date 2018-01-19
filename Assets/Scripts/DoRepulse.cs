using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoRepulse : MonoBehaviour {

	private Rigidbody thisRigidbody;

	public float sphRadius = 5;
	private float sphRadiusSqr = 25;
	public float repulseStrength = 5;

	// Use this for initialization
	void Start () {
		thisRigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		sphRadiusSqr = repulseStrength;
		sphRadiusSqr = sphRadius * sphRadius;
		doRepulse();
	}

	void doRepulse()
	{
		// test which node in within forceSphere.
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, sphRadius);

		// only apply force to nodes within forceSphere, with Falloff towards the boundary of the Sphere and no force if outside Sphere.
		foreach (Collider hitCollider in hitColliders)
		{
			Rigidbody hitRb = hitCollider.attachedRigidbody;

			if (hitRb != null && hitRb != thisRigidbody)
			{
				Vector3 direction = hitCollider.transform.position - this.transform.position;
				float distSqr = direction.sqrMagnitude;

				// Normalize the distance from forceSphere Center to node into 0..1
				float impulseExpoFalloffByDist = Mathf.Clamp( 1 - (distSqr / sphRadiusSqr), 0, 1);

				// apply normalized distance
				hitRb.AddForce(direction.normalized * repulseStrength * impulseExpoFalloffByDist);
			}
		}
	}
}
