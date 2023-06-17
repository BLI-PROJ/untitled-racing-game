using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
	public float speed;

	[Header("Drive Settings")]
	public float driveForce = 17f;			
	public float slowingVelFactor = .99f;   
	public float brakingVelFactor = .95f;   
	public float angleOfRoll = 30f;			

	[Header("Hover Settings")]
	public float hoverHeight = 1.5f;        
	public float maxGroundDist = 5f;        
	public float hoverForce = 300f;			
	public LayerMask whatIsGround;			
	public PIDController hoverPID;			

	[Header("Physics Settings")]
	public Transform shipBody;				
	public float terminalVelocity = 100f;   
	public float hoverGravity = 20f;        
	public float fallGravity = 80f;			
	
	private float drag;								
	private bool isOnGround;
	
	private Rigidbody rigidBody;
	private PhysicsScene physicsScene;

    void Start()
	{
		rigidBody = GetComponent<Rigidbody>();

		drag = driveForce / terminalVelocity;
		physicsScene = gameObject.scene.GetPhysicsScene();
	}

	void FixedUpdate()
	{
		speed = Vector3.Dot(rigidBody.velocity, transform.forward);
		
		CalculateHover();
		CalculatePropulsion();
	}

	void CalculateHover()
	{
		Vector3 groundNormal;
		
		Ray ray = new Ray(transform.position, -transform.up);
		
		RaycastHit hitInfo;
		
        isOnGround = physicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxGroundDist, whatIsGround);
        
        if (isOnGround)
		{
			float height = hitInfo.distance;
			groundNormal = hitInfo.normal.normalized;
			float forcePercent = hoverPID.Seek(hoverHeight, height);
			
			Vector3 force = groundNormal * (hoverForce * forcePercent);
			Vector3 gravity = -groundNormal * (hoverGravity * height);
			
			rigidBody.AddForce(force, ForceMode.Acceleration);
			rigidBody.AddForce(gravity, ForceMode.Acceleration);
		}
        else
		{
			groundNormal = Vector3.up;
			
			Vector3 gravity = -groundNormal * fallGravity;
			rigidBody.AddForce(gravity, ForceMode.Acceleration);
		}
        
		Vector3 projection = Vector3.ProjectOnPlane(transform.forward, groundNormal);
		Quaternion rotation = Quaternion.LookRotation(projection, groundNormal);
		
		rigidBody.MoveRotation(Quaternion.Lerp(rigidBody.rotation, rotation, Time.deltaTime * 10f));
		
		float angle = angleOfRoll * -InputManager.Instance.Rudder; //TODO verify
		
		Quaternion bodyRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
		shipBody.rotation = Quaternion.Lerp(shipBody.rotation, bodyRotation, Time.deltaTime * 10f);
	}

	void CalculatePropulsion()
	{
		float rotationTorque = InputManager.Instance.Rudder - rigidBody.angularVelocity.y; //TODO verify
		rigidBody.AddRelativeTorque(0f, rotationTorque, 0f, ForceMode.VelocityChange);
		float sidewaysSpeed = Vector3.Dot(rigidBody.velocity, transform.right);
		
		Vector3 sideFriction = -transform.right * (sidewaysSpeed / Time.fixedDeltaTime); 
		
		rigidBody.AddForce(sideFriction, ForceMode.Acceleration);
		
		if (InputManager.Instance.Thruster <= 0f) //TODO verify
			rigidBody.velocity *= slowingVelFactor;
		
		if (!isOnGround)
			return;
		
		if (InputManager.Instance.IsBreaking) //TODO verify
			rigidBody.velocity *= brakingVelFactor;
		
		float propulsion = driveForce * InputManager.Instance.Thruster - drag * Mathf.Clamp(speed, 0f, terminalVelocity); //TODO verify
		rigidBody.AddForce(transform.forward * propulsion, ForceMode.Acceleration);
	}

	public float GetSpeedPercentage()
	{
		return rigidBody.velocity.magnitude / terminalVelocity;
	}
}
