using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;


[RequireComponent(typeof(Rigidbody))]
public class Player_Behaviour : MonoBehaviour
{

    //public

    [Tooltip("How fast the player will move (make this the same speed as NPCs)")]
    public float speed;
    [Tooltip("Player ID number")]
    public int player_Num;
    [Tooltip("Input pick up and drop off speed")]
    public float input_Speed;


    //private

    //player rigidbody
    private Rigidbody rb;
    //player horizontal and vertical axis, joystick input.
    private float m_Horizontal_Input, m_Vertical_Input;
    //player velocity
    private Vector3 vel;
    //desired forward direction of the player
    private Vector3 m_Ray_Dir;
    //modified smooth player input
    private float m_Input_X, m_Input_Y;
    //player input reciever speed 
    private float m_Input_Modifier = 1;
    //rewired reference
    private Player m_Player;


    //Serialized fields
    [SerializeField]
    [Tooltip("Player Character rotational speed")]
    private float m_Rot_Mod;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_Player = ReInput.players.GetPlayer(player_Num);
    }

    // Update is called once per frame
    void Update()
    {
        Player_Inputs();
    }

    private void FixedUpdate()
    {
        Player_Movement();
    }

    void Player_Movement()
    {
        m_Input_X = Mathf.Lerp(m_Input_X, m_Horizontal_Input, Time.deltaTime * input_Speed);
        m_Input_Y = Mathf.Lerp(m_Input_Y, m_Vertical_Input, Time.deltaTime * input_Speed);


        vel.x = (m_Input_X * speed) * m_Input_Modifier;
        vel.z = (m_Input_Y * speed) * m_Input_Modifier;


        float _Player_Forward_X = m_Input_X * m_Input_Modifier;
        float _Player_Forward_Z = m_Input_Y * m_Input_Modifier;
        ///change forward direction
        Vector3 tempDir = new Vector3(_Player_Forward_X, 0, _Player_Forward_Z);


        if (tempDir.magnitude > 0.1f)
        {
            m_Ray_Dir = tempDir.normalized;
            transform.forward = Vector3.Slerp(transform.forward, m_Ray_Dir, Time.deltaTime * m_Rot_Mod);
        }

        rb.MovePosition(rb.position + Vector3.ClampMagnitude(vel, speed) * Time.deltaTime);
    }

    void Player_Inputs()
    {
        m_Horizontal_Input = m_Player.GetAxis("Horizontal");
        m_Vertical_Input = m_Player.GetAxis("Vertical");



    }
}
