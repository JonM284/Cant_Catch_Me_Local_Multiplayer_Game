using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class Sniper_Behaviour : MonoBehaviour
{

    //public 
    [HideInInspector]public float speed;
    public float original_Speed, amped_Speed;
    public int Player_ID;
    [HideInInspector]
    public Image player_Cross_Hair;

    //private
    private float m_Horizontal_Input, m_Vertical_Input;
    private Vector3 vel;
    private Camera cam;
    private Player rw_Sniper_Player;

    // Start is called before the first frame update
    void Start()
    {
        player_Cross_Hair = GetComponent<Image>();
        rw_Sniper_Player = Rewired.ReInput.players.GetPlayer(Player_ID);
        cam = Camera.main;
    }


    private void Update()
    {
        Check_Inputs();
    }

    void LateUpdate()
    {
        Movement();
    }

    void Movement()
    {
        m_Horizontal_Input = rw_Sniper_Player.GetAxisRaw("Horizontal");
        m_Vertical_Input = rw_Sniper_Player.GetAxisRaw("Vertical");

        vel.x = m_Horizontal_Input * speed;
        vel.y = m_Vertical_Input * speed;

        player_Cross_Hair.rectTransform.position = player_Cross_Hair.rectTransform.position + Vector3.ClampMagnitude(vel, speed) * Time.deltaTime;
    }

    void Check_Inputs()
    {
        speed = rw_Sniper_Player.GetButton("Sprint") ? amped_Speed : original_Speed;

        if (rw_Sniper_Player.GetButtonDown("Interact_Shoot"))
        {
            Ray ray = cam.ScreenPointToRay(player_Cross_Hair.GetComponent<Image>().rectTransform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                Debug.Log($"Shooting {name} at: {hit.point}");
            }
        }
    }
}
