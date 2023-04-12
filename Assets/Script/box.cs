using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public float jumpForce;
    public bool isUp;
    // cria uma vida para a caixa 
    public int health = 5;

    //criar uma variavel para o efeito da caixa explodindo
    public GameObject effect;
    public Animator anim;
    void Update()
    {
        if (health <= 0)
        {
            //destruo a caixa
            Instantiate(effect, transform.position, transform.rotation);
            //destruo o objeto pai que e o box.
            Destroy(transform.parent.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // quando o player bater na caixa algo acontece.
        if (collision.gameObject.tag == "Player")
        {
            if (isUp)
            {
                // chamar a animação da caixa quebrando
                anim.SetTrigger("hit");
                //decrementa uma vida da caixa a cada batida.
                health--;
                // o personagem é arremessado em alguma direção positiva.
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                anim.SetTrigger("hit");
                //decrementa uma vida da caixa a cada batida.
                health--;
                // o personagem é arremessado em alguma direção negativa.
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}
