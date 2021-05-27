using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 4f;

    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;


    CircleCollider2D attackCollider;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //Recuperamos el collider de ataque del primer hijo
        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        // Lo desactivamos desde el principio
        attackCollider.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        // Detectamos el movimiento en un vector 2D
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
         );

        /*
        Vector3 mov = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + mov,
            speed * Time.deltaTime
        );*/


        if(mov!= Vector2.zero)
        {
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("walking", true);
        } else
        {
            anim.SetBool("walking", false);
        }

        //Buscamos el estado actual mirando la informacion del animador
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Player_Attack");


        //Detectamos el ataque, tiene prioridad por lo que va abajo de todo
        if ((Input.GetKeyDown("space") || Input.GetKeyDown("y")) && !attacking)
        {
            anim.SetTrigger("attacking");
        }


        // Vamos actualizando la posicion de la colision de ataque
        if (mov != Vector2.zero)
        {
            attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);
        }

        //Actvivamos el collider a la mita de la animacion de ataque
        if (attacking)
        {
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > 0.72 && playbackTime < 0.92)
            {
                attackCollider.enabled = true;
            } else 
            {
                attackCollider.enabled = false;
            }
        }
            
    }

    private void FixedUpdate()
    {
        //Nos movemos en el fixed por las fisicas
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
    }

}
