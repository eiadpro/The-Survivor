using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotmove : MonoBehaviour
{
    [SerializeField] Transform[] way;
	[SerializeField] Transform avatar;
	int current = 0;
    bool ready = false;
    Vector3 rot = Vector3.zero;
	// Start is called before the first frame update
	float rotSpeed = 40f;
	Animator anim;
	bool scene1 = false;
	float time = 0;


	// Use this for initialization
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rot;
	}

	// Update is called once per frame
	void Update()
	{
		CheckKey();
		if(time<5.1f)
		time += 0.01f;

		if (scene1 && Mathf.Abs(transform.rotation.y - avatar.rotation.y)>0.05)
		{
			if (transform.rotation.y> avatar.rotation.y)
			{
				rot[1] -= 40f * Time.fixedDeltaTime;
			}
            else
            {
				rot[1] += 40f * Time.fixedDeltaTime;
			}
		}
		if (time>5&&!scene1&&transform.rotation.y < 0.7)
		{
			rot[1] += 40f * Time.fixedDeltaTime;

		}
		else if(time > 5)
		{
			ready = true;
		}

		if (ready && !scene1)
		{
			anim.SetBool("Walk_Anim", true);
			transform.position = Vector3.MoveTowards(transform.position, way[current].position, 1.3f * Time.deltaTime);
		}
		if (transform.position.x >= 9)
        {
			anim.SetBool("Walk_Anim", false);
			ready = false;
			scene1 = true;
		}

		gameObject.transform.eulerAngles = rot;
	}

	void CheckKey()
	{
		// Walk
		if (Input.GetKey(KeyCode.UpArrow))
		{
			anim.SetBool("Walk_Anim", true);
		}
		else if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (anim.GetBool("Roll_Anim"))
			{
				anim.SetBool("Roll_Anim", false);
			}
			else
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

		// Close
		if (Input.GetKeyDown(KeyCode.RightShift))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
			}
		}
	}

	// Update is called once per frame

}
