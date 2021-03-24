using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{

	//Platform movement speed.平台移动速度
	public float speed;

	//This is the position where the platform will move.平台移动的位置
	//public Transform MovePosition;//创建一个空物体作为移动的位置

	[Tooltip("How close to the side of the screen the cloud can get.")]
	public float sidePadding = 1f;

	private Vector3 StartPosition;
	private Vector3 EndPosition;
	private bool OnTheMove;

	// Target position used to lerp the movement.
	private Vector3 targetPosition;

	// Timer to stop the cloud
	private float stopTimer = 1f;

	// Save the original speed
	private float originalSpeed;

	// Use this for initialization
	void Start()
	{
		// Randomise seed
		System.Random randomizer = new System.Random();
		UnityEngine.Random.InitState(randomizer.Next(int.MinValue, int.MaxValue));

		//Store the start and the end position. Platform will move between these two points.储存左右两端点位置
		float posY = transform.position.y;
		float posZ = transform.position.z;
		Camera camera = Camera.main;
		float sideMovement = (camera.aspect * camera.orthographicSize) - sidePadding;
		StartPosition = new Vector3(-sideMovement, posY, posZ);
		EndPosition = new Vector3(sideMovement, posY, posZ);

		targetPosition = Vector3.Lerp(StartPosition, EndPosition, 0.5f); // Could use Random.Range(0f, 1f) instead of 0.5f
		transform.position = targetPosition;

		originalSpeed = speed;
	}

	void FixedUpdate()
	{

		// Random chance to change speed
		if (Random.Range(0, 120) == 0) speed = originalSpeed * Random.Range(0.8f, 4f);

		float step = speed * Time.fixedDeltaTime;

		// Random chance to reverse cloud direction.
		if (Random.Range(0, 120) == 0) OnTheMove = !OnTheMove;
		
		if (stopTimer > 0f)
		{
			stopTimer -= Time.fixedDeltaTime;
		}
		else
		{
			if (OnTheMove == false)
			{
				targetPosition = Vector3.MoveTowards(targetPosition, EndPosition, step);
			}
			else
			{
				targetPosition = Vector3.MoveTowards(targetPosition, StartPosition, step);
			}

			// Random chance to stop the cloud movement.
			if (Random.Range(0, 160) == 0) stopTimer = Random.Range(0.5f, 2f);
		}

		//When the platform reaches end. Start to go into other direction.
		if (targetPosition.x == EndPosition.x && targetPosition.y == EndPosition.y && OnTheMove == false)
		{
			OnTheMove = true;
		}
		else if (targetPosition.x == StartPosition.x && targetPosition.y == StartPosition.y && OnTheMove == true)
		{
			OnTheMove = false;
		}

	}

	void Update()
	{
		// Lerp the cloud movement
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 3f);
	}


}








// 您好，此秘密文本已翻译 :D
