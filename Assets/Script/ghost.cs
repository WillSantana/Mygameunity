using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    // Componentes do inimigo
    private Rigidbody2D rig;
    private Animator anim;
    // dentificar se o inimigo ta batendo na parede.
    private bool colliding;

    // Configurações do inimigo
    public float speed;
    public Transform rightCol;
    public Transform leftCol;
    public Transform headPoint;
    public LayerMask layer;
    
    //referenciar as variaveis dentro do espetros, parte grafica.
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            //rotacionar o eixo x do personagem, usando -1 para inverter o eixo
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    //foi criado um booleano para checar se o booleano é falso, para que o inimigo não pare de andar para um lado e para outro.
    // Estado do inimigo
    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //checar se o personagem ta batendo na cabeça do inimigo usando o headpoint como base, subtraindo o local onde ele bateu no y
            float height = col.contacts[0].point.y - headPoint.position.y;

            // checar se a altura é maior do que zero (É) se o booleano é falso.
            if (height > 0 && !playerDestroyed)
            {
                //fazer o personagem dar um pulo ao bater no inimigo.
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                //para o inimigo parar ao receber o contato
                speed = 0;
                // chama animação da morte.
                anim.SetTrigger("die");
                //desabilitar o boxCollider2d e o circleCollider2D
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                //trocar o rigidbody para kinematic
                rig.bodyType = RigidbodyType2D.Kinematic;
                //destrói o inimigo depois de um tempo.
                Destroy(gameObject, 0.33f);
            }
            else
            {
                // caso o inimigo bata no player, o playerdestroyed muda para verdadeiro 
                playerDestroyed = true;
                // o else coloca uma opção para destruir o personagem e dar o game over.
                //gamecontroller chama a instância do ShowGameOver.
                GameController.instance.ShowGameOver();
                Destroy(col.gameObject);
            }
        }
    }
}