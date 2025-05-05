using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class PatrollIDLE : MonoBehaviour
{
    public GameObject Patrolpoint;
    public Transform[] patrolPoints;
    public float radio = 3f;
    private int currentPoint = 0;
    public Transform player;
    private NavMeshAgent agent;
    private float distanceTolerance = 1.0f;
    public bool isPatrolSearching = false;
    public float rotationStep = 5f;
    private float rotation = 0f;  


    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isPatrolSearching)
        {
            GenerateRandomPoints();
        }
    }

    public void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < distanceTolerance) 
        {
            currentPoint++; 
            if (currentPoint >= patrolPoints.Length) 
            {
                currentPoint = 0; 
            }
        }
        if (NavMesh.SamplePosition(patrolPoints[currentPoint].position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) 
        {
            agent.SetDestination(patrolPoints[currentPoint].position); 
        }
    }

    void GenerateRandomPoints()
    {
        bool pointsGenerated = false;  // Para asegurarnos de que se generen puntos correctamente
        int validPointsCount = 0;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized * radio;
            Vector2 randomPoint = (Vector2)player.position + randomDirection;
            Vector3 point3D = new Vector3(randomPoint.x, 1.21f, randomPoint.y);

            // Verificar si el punto está dentro del NavMesh
            if (NavMesh.SamplePosition(point3D, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                patrolPoints[i].position = new Vector3(hit.position.x, 1.21f, hit.position.z);  // Actualizamos la posición del punto de patrullaje
                validPointsCount++;
            }
        }

        if (validPointsCount < 2)
        {
            // Si no hay suficientes puntos válidos, rotamos el objeto para intentar nuevamente
            rotation += rotationStep;
            transform.Rotate(0, rotationStep, 0);  // Rotamos el objeto en pequeños pasos

            // Intentamos generar los puntos nuevamente después de rotar
            if (rotation >= 360f)
            {
                rotation = 0f;  // Reiniciar la rotación después de una vuelta completa
            }
        }
        else
        {
            pointsGenerated = true;
        }

        if (!pointsGenerated)
        {
            Debug.LogWarning("No se generaron puntos válidos dentro del NavMesh. Intentando rotar el objeto.");
        }

        isPatrolSearching = false;  // Terminamos de generar puntos
    }


}
