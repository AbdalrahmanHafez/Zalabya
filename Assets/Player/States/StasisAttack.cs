using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisAttack : MonoBehaviour
{
	[Header("Reticle")]
	public GameObject attackReticle;

	[Header("Attack Properties")]
	public float attackStrength;

	// StasisController playerController;
	LayerMask stasisLayerMask;
	RaycastHit hit;
	Stasis stasis;



	void Start()
	{
		// playerController = GetComponent<StasisController>();
		stasis = GetComponent<Stasis>();
		stasisLayerMask = stasis.layerMask;
	}

	bool stEnabled = false;
	private void Update()
	{
		if (!Abilities.activeStasis)
			return;

		if (Input.GetKey(KeyCode.R))
		{
			if (stEnabled)
				OnEnd();

			OnActivate();
			stEnabled = true;
		}

		// if (stEnabled)
		OnUpdate();
	}

	public void OnActivate()
	{
		attackReticle.SetActive(true);
		//playerController.animator.SetTrigger("Attack");

		// temporary for debugging
		OnAttack();
	}

	// called via animation event when character kicks
	void OnAttack()
	{
		Camera mainCam = Camera.main;
		// Camera mainCam = playerController.MainCamera;
		if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, 30.0f, stasisLayerMask))
		{
			Vector3 closestPoint = hit.collider.ClosestPointOnBounds(transform.position);
			stasis.AddStasisForce(new StasisMomentum(mainCam.transform.forward * attackStrength, closestPoint - transform.position));
		}
		// playerController.SM.SetState<Movement>();
	}


	public void OnUpdate()
	{

	}


	public void OnEnd()
	{
		attackReticle.SetActive(false);
		//playerController.animator.SetTrigger("Attack");
	}


	/*void OnDrawGizmos() {
        if (hit.collider != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.collider.ClosestPointOnBounds(transform.position), 0.2f);
        }
    }*/
}
