using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public bool canMove = true;

    [SerializeField] float speed;
    public int startPoint;
    public Transform[] points;
    private int i;
    public bool reverse;

    [SerializeField] float waitTime = 6f; // Tempo de espera em segundos

    void Start()
    {
        transform.position = points[startPoint].position;
        i = startPoint;

        // Começar a lógica do movimento
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            // Mover a plataforma até o próximo ponto
            while (Vector3.Distance(transform.position, points[i].position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
                yield return null; // Esperar até o próximo frame
            }

            // Chegou no ponto de destino
            transform.position = points[i].position; // Garantir a precisão

            // Decidir próximo ponto ou inverter direção
            if (i == points.Length - 1)
            {
                reverse = true;
            }
            else if (i == 0)
            {
                reverse = false;
            }

            i += reverse ? -1 : 1;

            // Esperar antes de continuar
            yield return new WaitForSeconds(waitTime);
        }
    }
}
