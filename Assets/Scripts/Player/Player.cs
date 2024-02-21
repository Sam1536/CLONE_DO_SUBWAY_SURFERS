using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SIDE { Left,Mid,Right}

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private GameController gameController;
    
    public Animator anim;
    public bool isDead;

    public float speed = 10f;
    public float jumpHeight;
    private float jumpVelocity;
    public float gravity;
   

    public float rayRadius;
    public LayerMask layerMaskDeath;
    public LayerMask layerCoin;


    [Header("moviment Gringo")] 
    public SIDE m_Side = SIDE.Mid;
    private float newPosX = 0f;
    public bool swiperRight;
    public bool swiperLeft;
    public float valueX;
    private float x;
    public float speedDodge;
    
    
     public void Start()
     {
        //transform.position = Vector3.zero;

        controller = GetComponent<CharacterController>();
        gameController = FindObjectOfType<GameController>();
     }

    private void Update()
    {
        Vector3 direction = Vector3.forward * speed;



        if (controller.isGrounded) // verifica se eu estou tocando no chão
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = jumpHeight;
            }
        }
        else
        {
            jumpVelocity -= gravity;
        }


        swiperRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
        swiperLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);

        if (swiperRight)
        {
            if (m_Side == SIDE.Mid)
            {
                newPosX = -valueX;
                m_Side = SIDE.Right;
            }
            else if(m_Side == SIDE.Left)
            {
                newPosX = 0;
                m_Side = SIDE.Mid;

            }
        }
        else if (swiperLeft)
        {
            if (m_Side == SIDE.Mid)
            {
                newPosX = valueX;
                m_Side = SIDE.Left;

            }
            else if(m_Side == SIDE.Right)
            {
                newPosX = 0;
                m_Side = SIDE.Mid;
            }
        }
        x = Mathf.Lerp(x, newPosX, Time.deltaTime * speedDodge);
        controller.Move((x - transform.position.x) * Vector3.right);


        Oncollision();

        direction.y = jumpVelocity;

        controller.Move(direction * Time.deltaTime);

        
    }
    
    void Oncollision()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out hit, rayRadius, layerMaskDeath) && !isDead)
        {
            //chama o GameOver
            anim.SetTrigger("Death");
            speedDodge = 0;
            speed = 0;
            jumpHeight = 0;
            Invoke(nameof(GameOverShow), 5f); 
            isDead = true;
            Debug.Log("!bateu");
        }

        RaycastHit hitCoin;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(0, 1f, 0)), out hitCoin, rayRadius, layerCoin))
        {
            //bateu na moeda
            gameController.AddCoin();
            Destroy(hitCoin.transform.gameObject);
            Debug.Log("pegou a moeda");
        }
    }

    void GameOverShow()
    {
        gameController.ShowGameOver();
    }

    
}
