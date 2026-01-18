using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Mobs.JungleMobs
{
	public class ZombieAI : MonoBehaviour
	{

		public Transform player;
		private float chaseDistance = 8f;
		private float attackDistance = 1.5f;
		private float speedWalking = 1f;
		private float speedRunning = 2f;
		private float distance;

		private bool _isRunning = false;
		private bool _isWalking= true;

		private float idleTimer = 0f;
		private Vector3 wanderDirection; 
		private float wanderTime = 0f;


		public Animator animator;

		private enum State { Idle, Walk, Run, Jump, Attack }
		private State _state = State.Idle;

		void Awake()
		{
			animator = GetComponent<Animator>();
			_state = State.Idle;
		}

	

		void Update()
		{
			RandomWalking();
			distance = Vector3.Distance(transform.position, player.position);
			ApplyMovement(distance);
			UpdateAnimation();
			
			Debug.Log("STATE: " + _state);

		}

		private void ApplyMovement(float distance)
		{

			if (distance <= chaseDistance)
			{
				Console.WriteLine("A DISTANCIA É: " + distance);
				_isWalking = false;
				_isRunning = true; 

				_state = State.Run;

				transform.position = Vector3.MoveTowards(transform.position, player.position, speedRunning * Time.deltaTime);
				transform.LookAt(player);


				if (distance <= attackDistance)
				{
					_state = State.Attack;
					Console.WriteLine(":.:.:.:.:.:.:.:.:.:.:.:.:.:.:.ESTOU ATACANDO HEIN. ATAQUES LOUCOS :.:.:.:.:.:.:.:.:.:.:.:.:.:.:.");
					
				}
			}
			else if(distance > chaseDistance && _state == State.Run)
			{
				_isRunning = false;
				_isWalking = true;
				_state = State.Idle;
			}




		}

		private void UpdateAnimation()
		{
			animator.SetBool("walking", _state == State.Walk);
			animator.SetBool("idle", _state == State.Idle);
			animator.SetBool("attacking", _state == State.Attack);
			animator.SetBool("running", _state == State.Run);

		}

		
		private void RandomWalking()
		{
			if (_state == State.Idle)
			{
				idleTimer += Time.deltaTime;

				if (idleTimer >= 5f)
				{
					idleTimer = 0f;

					// gira aleatoriamente
					float randomAngle = UnityEngine.Random.Range(-180f, 180f);
					transform.Rotate(0, randomAngle, 0);

					// começa a andar
					_state = State.Walk;
					wanderTime = 3f;
				}
			}

			if (_state == State.Walk && wanderTime > 0f)
			{
				wanderTime -= Time.deltaTime;

				// anda pra frente
				transform.position += transform.forward * speedWalking * Time.deltaTime;

				if (wanderTime <= 0f)
					_state = State.Idle;
			}
		}



	}


}
