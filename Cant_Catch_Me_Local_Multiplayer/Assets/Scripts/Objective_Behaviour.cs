using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective_Behaviour : MonoBehaviour
{
    //public
    //How long it will take to complete the task in seconds.
    public float max_Task_Amount;


    //private
    //Is the current task being worked on?
    private bool m_Task_Being_Completed = false;
    //Is the current task being completed
    private bool m_Task_Completed = false;
    //Has this task been set to regress?
    private bool m_Task_Regression = false;
    //Current state of task, amount that has been completed.
    private float m_Current_Task_Amount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Objective_Update()
    {
        if (m_Task_Being_Completed && m_Current_Task_Amount < max_Task_Amount && !m_Task_Completed)
        {
            m_Current_Task_Amount += Time.deltaTime;
        }

        if (m_Current_Task_Amount >= max_Task_Amount)
        {
            Send_Completion_Data();
        }

        if (m_Task_Regression && m_Current_Task_Amount > 0 && !m_Task_Completed && !m_Task_Being_Completed)
        {
            m_Current_Task_Amount -= Time.deltaTime;
        }
    }

    void Send_Completion_Data()
    {
        m_Task_Completed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<Player_Behaviour>().is_Using)
            {
                m_Task_Being_Completed = true;
            }
        }
    }
}
