using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
	public int health = 3;
	public float invincTime = 1f;
	public Vector2 restartPos;
	private GameObject[] healthImages = new GameObject[3];
	private Player move;
	private Animator anim;
	private SpriteRenderer sprite;
	private Rigidbody2D rig;

	private MovementController moveCont;

	public bool dying = false;
	private bool canBeDamaged = true;

	private Collider2D box;
	private Collider2D deathBox;

	void Start ()
	{
		restartPos = new Vector2 (transform.position.x, transform.position.y);
		move = this.GetComponent<Player> ();
		anim = this.GetComponent<Animator> ();
		sprite = this.GetComponent<SpriteRenderer> ();
		box = this.GetComponent<Collider2D> ();
		deathBox = GameObject.Find ("Monster").GetComponent<Collider2D> ();
		moveCont = this.GetComponent<MovementController> ();

		//Debug.Log (box + " and " + deathBox);

		for (int i = 1; i <= 3; i++)
			healthImages [i - 1] = GameObject.Find ("Canvas/Panel/GameObject/Health" + i);
		rig = this.GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		//StartCoroutine(delayedPositionSet (new Vector2 (transform.position.x, transform.position.y)));
		//Debug.Log (new Vector2 (transform.position.x, transform.position.y));

		if (transform.position.y > 3f || transform.position.y < -0.5f)
			StartCoroutine(Die ());

		//Animation handling:
		if (move.input < 0)
		{
			Vector3 temp = transform.localScale;
			temp.x = -1;
			transform.localScale = temp;
		}
		else if (move.input > 0)
		{
			Vector3 temp = transform.localScale;
			temp.x = 1;
			transform.localScale = temp;
		}

		anim.SetFloat("Speed", (move.speed01 > 0.5) ? 1 : 0);

		//anim.SetBool("MovingUp", moveCont.collisionInfo.movingUp);
		anim.SetBool("Grounded", moveCont.collisionInfo.below);

        //CollisionChecks ();
        //Debug.Log(1.0f / Time.deltaTime);
	}

	IEnumerator delayedPositionSet(Vector2 newPos)
	{
		yield return new WaitForSeconds (1);
		restartPos = newPos;
	}

	/*
	private void CollisionChecks()
	{
		if (box.IsTouching (deathBox))
		{
			Debug.Log ("death");
			Die ();
		}
	}*/

	public void Hit ()
	{
		health -= 1;

		if (health == 2)
			healthImages[2].SetActive(false);
		else if (health == 1)
			healthImages[1].SetActive(false);
		else
		{
			healthImages[0].SetActive(false);
			StartCoroutine(Die ());
		}

		StartCoroutine(Flicker ());

		//Vector2 temp = transform.position;
		//temp.x = restartPos.x;
		//temp.y = restartPos.y;
		//transform.position = temp;
	}

	IEnumerator Flicker ()
	{
		for (int i = 0; i < 3; i++)
		{
			sprite.color = Color.clear;
			yield return new WaitForSeconds (invincTime / 8f);

			sprite.color = Color.gray;
			yield return new WaitForSeconds (invincTime / 8f);
		}

		sprite.color = Color.clear;
		yield return new WaitForSeconds (invincTime / 8f);

		sprite.color = Color.white;
	}

	IEnumerator delayedInvincibility ()
	{
		yield return new WaitForSeconds (invincTime);
		canBeDamaged = true;
	}


	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Heal") {
			Heal ();
			Destroy (col.gameObject);
		}
		else if (col.tag == "Enemy" && canBeDamaged)
		{
			Vector3 dir = new Vector3 (transform.position.x - col.transform.position.x,
				              transform.position.y - col.transform.position.y, 0).normalized * 7;
            if(dir.y > 6)
            {
                dir.y = 6;
            }
            if(dir.y < -2)
            {
                dir.y = -2;
            } 
			Vector3 temp = move.velocity;
			temp += dir;
			//temp.y += 4;
			move.velocity = temp;

			canBeDamaged = false;
			Hit ();
			//Create vector with player's position and monster's position
			StartCoroutine (delayedInvincibility ());
		}
		else if (col.tag == "Death")
		{
			StartCoroutine(Die ());
		}
	}

	public void Heal ()
	{
		if (health == 3)
			return;
		else if (health == 2)
			healthImages [2].SetActive (true);
		else if (health == 1)
			healthImages [1].SetActive (true);

		health += 1;
	}

	IEnumerator Die ()
	{
		//Set variable & play sound
		dying = true;
		anim.SetTrigger ("Die");
		yield return new WaitForSeconds(0.75f);
		//yield return new WaitForSeconds (1);
		Time.timeScale = 0;
		GameObject.Find ("Manager").GetComponent<UserInterface> ().gameOver.SetActive (true);
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			
	}
}
