using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, IDamageable
{
    [SerializeField] int hp = 1;
    [SerializeField] Transform bridge;
    [SerializeField] float destroyDistance = 20;
    [SerializeField] GameObject connectedObject;
    
    private HingeJoint2D joint;
    private HingeJoint2D connectedJoint;

    public void TakeDamage(int damage)
    {       
        hp -= damage;
        if (hp <= 0)
        {
            BreakComponent();
        }        
    }

    private void BreakComponent()
    {
        if (connectedJoint != null)
        {
            joint.enabled = false;
            connectedJoint.enabled = false;    
        }
    }

    private bool IsVisible() => Vector3.Distance(transform.position, bridge.position) > destroyDistance;

    private void Start()
    {    
        joint = GetComponent<HingeJoint2D>();
        connectedJoint = connectedObject.GetComponent<HingeJoint2D>();
    }

    private void Update()
    {      
        if (IsVisible())
        {
            Destroy(gameObject); 
        }
    }
}
