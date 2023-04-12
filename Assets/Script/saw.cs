using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saw : MonoBehaviour
{

    public float speed;
    public float moveTime;
    public bool dirRight = true;
    private float timer;


    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
                // se verdadeiro a serra vai para direita
                transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            // se falso a serra vai para esquerda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
        // incremetando a variavel timer que é 0. e soma o valor real do tempo, que é o deltatime.
        timer += Time.deltaTime;

        //quando o time passar o valor do movetime que vamos dar no jogo ele executa a condiçao.
        if(timer >= moveTime)
        {
            //quando a condiçao for executada o boleano sera trocado ao final do movetimerm ele troca de lado.
            dirRight = !dirRight;
            timer = 0f;
        }
    }
}
