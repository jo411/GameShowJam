using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class RigidBodyFpsController : MonoBehaviour
{

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
	public float mouseVerticalSensitivity = 100f;	


	public Transform PlayerCamera;

	private bool grounded = false;
	private bool isLockedOut = false;
	private Rigidbody rb;


	public Rigidbody spinningPlatform;

	public LayerMask groundedLayers;

	float rayYoffset = .5f;

    void Awake()
    {
		rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void FixedUpdate()
	{
		checkGrounded();
		//rigidBody.rotation.SetLookRotation(PlayerCamera.rotation.eulerAngles, Vector3.up);
		if (grounded && !isLockedOut)
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			Quaternion cameraRotation = Quaternion.Euler(0, PlayerCamera.rotation.eulerAngles.y, 0);

			rb.MoveRotation(cameraRotation);
		
			Vector3 targetVelocity = new Vector3(h, 0, v).normalized;

			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;	
			
			Vector3 velocity = rb.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);



			if (spinningPlatform != null)
			{
				float r = (spinningPlatform.gameObject.transform.position - transform.position).magnitude;
				Vector3 tangentialVelocity = spinningPlatform.angularVelocity;
				tangentialVelocity.Scale(new Vector3(r, r, r));
				//rb.angularVelocity= tangentialVelocity;				
				Debug.Log(tangentialVelocity);

			}


			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rb.AddForce(velocityChange, ForceMode.VelocityChange);			

			if (canJump && Input.GetButton("Jump"))
			{
				rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}



		}
		
		
		rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));
	
		grounded = false;
	}


	void checkGrounded()
	{
		Ray ray = new Ray(transform.position+new Vector3(0,rayYoffset,0), Vector3.down);
		RaycastHit hitinfo;
		if (Physics.Raycast(ray, out hitinfo, 100, groundedLayers))
		{
			float dist = ray.origin.y - hitinfo.point.y;
			Debug.Log(dist);
			Debug.DrawLine(ray.origin,hitinfo.point);
			if (dist < .6)
			{
				grounded = true;
			}
			else
			{
				grounded = false;
			}
		}
		
	}

	void OnCollisionStay()
	{
		//grounded = true;
	}

	float CalculateJumpVerticalSpeed()
	{	
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	void UnlockControls()
    {
		isLockedOut = false;
    }

	public void LockoutControls(float seconds=1)
    {
		isLockedOut = true;
		Invoke("UnlockControls", seconds);
    }

}
