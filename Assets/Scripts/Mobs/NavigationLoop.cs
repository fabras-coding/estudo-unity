using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavigationLoop : MonoBehaviour
{
	public Transform[] goals;
	private int m_NextGoal = 0;

	private NavMeshAgent m_Agent;
	private Animator m_Animator;

	[Header("Blend Tree thresholds (0 = Idle, 1 = Walk, 2 = Run)")]
	public float walkThreshold = 0.5f;   // percentual (0..1) para começar a considerar walk
	public float runThreshold = 0.9f;    // percentual (0..1) para considerar run

	[Header("Animator smoothing")]
	public float animSmoothTime = 6f;

	private float animSpeedValue = 0f; // valor atual enviado ao Animator

	void Start()
	{
		m_Agent = GetComponent<NavMeshAgent>();
		m_Animator = GetComponent<Animator>();

		if (goals == null || goals.Length == 0)
			Debug.LogWarning("NavigationLoop: nenhum goal atribuído.");

		if (m_Animator != null)
			m_Animator.applyRootMotion = false;
	}

	void Update()
	{
		if (goals == null || goals.Length == 0) return;

		// Navegação
		if (!m_Agent.pathPending && m_Agent.remainingDistance <= m_Agent.stoppingDistance)
			m_NextGoal = (m_NextGoal + 1) % goals.Length;

		m_Agent.destination = goals[m_NextGoal].position;

		// Velocidade atual do agente (m/s)
		float currentSpeed = m_Agent.velocity.magnitude;

		// Normaliza em relação à velocidade máxima do agente (0..1)
		float speedPercent = 0f;
		if (m_Agent.speed > 0.001f)
			speedPercent = Mathf.Clamp01(currentSpeed / m_Agent.speed);

		// Mapear percentual para os valores do Blend Tree com thresholds 0,1,2
		// 0 -> Idle, 1 -> Walk, 2 -> Run
		float targetAnimValue;
		if (speedPercent <= 0.01f)
		{
			targetAnimValue = 0f; // Idle
		}
		else if (speedPercent < walkThreshold)
		{
			// pequeno movimento, ainda considerar como Walk leve
			targetAnimValue = 1f;
		}
		else if (speedPercent >= runThreshold)
		{
			targetAnimValue = 2f; // Run
		}
		else
		{
			// Entre walkThreshold e runThreshold, interpolar entre 1 e 2
			float t = Mathf.InverseLerp(walkThreshold, runThreshold, speedPercent);
			targetAnimValue = Mathf.Lerp(1f, 2f, t);
		}

		// Suavizar para evitar pulos no Blend Tree
		animSpeedValue = Mathf.Lerp(animSpeedValue, targetAnimValue, Time.deltaTime * animSmoothTime);

		// Enviar para o Animator (verifique o nome exato do parâmetro)
		if (m_Animator != null)
			m_Animator.SetFloat("Speed", animSpeedValue);

		// Opcional: bool para movimento
		if (m_Animator != null)
			m_Animator.SetBool("isMoving", speedPercent > 0.01f);
	}
}
