using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    //Variable para guardar el nombre del estado de destruccion
    public string destroyState;

    // Variable con los segundos a esperar antes de desactivar la colision

    public float timeForDisable;

    // Animador para controlar la animacion
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    //Detectamos la colision con una corrutina
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
         // si es un ataque
         if(collision.tag == "Attack")
        {
            // Reproducimos la animacion de destruccion y esperamos
            anim.Play(destroyState);
            yield return new WaitForSeconds(timeForDisable);

            // Pasados los segundos de espera desactivamos los colliders 2D

            foreach(Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
        }
    }






    // Update is called once per frame
    void Update()
    {
        // "Destruir" el objeto al finalizar la animacion de destruccion
        // el estado debe tener el atributo 'loop' a false para no repetirse
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.IsName(destroyState) && stateInfo.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}
