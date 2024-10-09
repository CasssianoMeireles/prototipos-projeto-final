using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform pointA;  // Ponto inicial da patrulha
    public Transform pointB;  // Ponto final da patrulha
    public float speed = 3f;

    private Transform targetPoint;  // Próximo ponto para onde o NPC vai se mover

    void Start()
    {
        // Define o primeiro ponto de destino como o ponto A
        targetPoint = pointA;
    }

    void Update()
    {
        // Move o NPC na direção do ponto alvo
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Se o NPC atingir o ponto alvo, alterna para o outro ponto
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }
}
