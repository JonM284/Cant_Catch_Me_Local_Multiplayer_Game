using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Behaviour : MonoBehaviour
{

    [HideInInspector]public float speed;
    public float original_Speed, run_Speed; 
    public Transform[] bounds;
    public Vector3[] bounds_Positions;
    public Transform[] points_Of_Interest;

   

    //private
    //npc rigidbody
    private Rigidbody rb;
    private Vector3 vel;
    private int current_Path_Point = 0;
    private float m_Horizontal_Input, m_Vertical_Input;
    private float wait_Timer = 0, m_Random_Timer;
    [SerializeField]
    private bool m_Path_Usable = false;
    [SerializeField]
    private bool m_Can_Walk = false;
    private bool m_Is_Alive = true;
    private NavMeshAgent self;
    private NavMeshPath path;
    private Vector3 custom_Path;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < bounds_Positions.Length; i++)
        {
            bounds_Positions[i] = bounds[i].position;
        }
        Pick_New_Point();
    }

    // all NPC's will be updated from an NPC manager, this way we reduce the amount of update calls per frame.
    
    public void Custom_Update()
    {
        if (m_Is_Alive) {
            if (m_Path_Usable && m_Can_Walk)
            {
                Movement();
            }

            if (!self.hasPath)
            {
                Debug.Log("Finished Path");
                if (m_Can_Walk)
                {
                    m_Random_Timer = Random.Range(0.5f, 4f);
                    m_Can_Walk = false;
                }
                Wait_For_New_Path();
            }
        }
    }


    void Movement()
    {
        

        if (self.hasPath)
        {
            Vector3 _dir = self.steeringTarget - transform.position;
            m_Horizontal_Input = _dir.x * speed;
            m_Vertical_Input = _dir.z * speed;

            vel.x = m_Horizontal_Input;
            vel.z = m_Vertical_Input;

            rb.MovePosition(rb.position + Vector3.ClampMagnitude(vel, speed) * Time.deltaTime);
            
        }
        
        

    }

    void Pick_New_Point()
    {
        int _random_Choice = Random.Range(0,2);
        int _run_Choice = Random.Range(0,2);
        Vector3 _new_Pos;
        if (_random_Choice == 0) {
            _new_Pos = new Vector3(Random.Range(bounds_Positions[0].x, bounds_Positions[1].x)
                , transform.position.y, Random.Range(bounds_Positions[2].z, bounds_Positions[3].z));
        }else
        {
            int _random_POI = Random.Range(0, points_Of_Interest.Length);
            _new_Pos = new Vector3(points_Of_Interest[_random_POI].position.x + Random.insideUnitCircle.x, transform.position.y,
               points_Of_Interest[_random_POI].position.z + Random.insideUnitCircle.y);
        }

        speed = _random_Choice == 0 ? original_Speed : run_Speed;
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = _random_Choice == 0 ? Color.white : Color.blue;
        Get_Path(_new_Pos);
    }

    void Get_Path(Vector3 _destination)
    {
        path = new NavMeshPath();
        if (self.CalculatePath(_destination, path))
        {
            self.SetPath(path);
            m_Path_Usable = true;
            m_Can_Walk = true;
            
            Debug.Log("Can begin moving");
        }else
        {
            Pick_New_Point();
        }
    }

    void Wait_For_New_Path()
    {
        wait_Timer += Time.deltaTime;
        if (wait_Timer >= m_Random_Timer)
        {
            Pick_New_Point();
            wait_Timer = 0;
        }
    }

    void Set_Path_Points_To_Move()
    {

    }
}
