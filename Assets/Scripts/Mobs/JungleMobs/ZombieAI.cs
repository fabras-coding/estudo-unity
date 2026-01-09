using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Mobs.JungleMobs
{
	public class MobAI : MonoBehaviour
	{

		public Transform player;
		private float chaseDistance = 8f;
		private float attackDistance = 1.5f;
		public float speedMovement = 2f;

		private bool _isRunning = false;
		private bool _isWalking= true;

		public Animator animator;

		private enum State { Idle, Walk, Run, Jump, Attack }
		private State _state = State.Idle;

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

	

		private void Update()
		{

			float distance = Vector3.Distance(transform.position, player.position);
			ApplyMovement(distance);
			UpdateAnimation();
			
			
			
		}

		private void ApplyMovement(float distance)
		{

			if (distance <= chaseDistance)
			{

				_isWalking = false;
				_isRunning = true; 
				//animator.SetBool("walking", false);
				//animator.SetBool("running", true);
				//animator.SetBool("idle", false);
				//animator.SetBool("attacking", false);

				_state = State.Run;

				transform.position = Vector3.MoveTowards(transform.position, player.position, speedMovement * Time.deltaTime);
				transform.LookAt(player);


				if (distance <= attackDistance)
				{
					_state = State.Attack;
					Console.WriteLine(":.:.:.:.:.:.:.:.:.:.:.:.:.:.:.ESTOU ATACANDO HEIN. ATAQUES LOUCOS :.:.:.:.:.:.:.:.:.:.:.:.:.:.:.");
					//animator.SetBool("attacking", true);
					//animator.SetBool("idle", false);
					//animator.SetBool("walking", false);
					//animator.SetBool("running", true);
				}
			}			
			else
			{
				_isWalking = true;
				_isRunning = false;

				_state = State.Idle;

				//animator.SetBool("idle", true);
				//animator.SetBool("walking", false);
				//animator.SetBool("ataccking", false);
				//animator.SetBool("running", false);
			}


		}

		private void UpdateAnimation()
		{
//			animator.speed = _isRunning ? 2.5f : 2f;
			animator.SetBool("walking", _state == State.Walk);
			animator.SetBool("idle", _state == State.Idle);
			animator.SetBool("attacking", _state == State.Attack);
			animator.SetBool("running", _state == State.Run);

		}

	}

	
}
