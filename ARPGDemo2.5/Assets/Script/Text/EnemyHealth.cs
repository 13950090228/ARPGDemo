using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class EnemyHealth : MonoBehaviour 
{
    public int HP;
    public int SP;
    public GameObject obj;
    private Renderer rend;
    public double distance;


    public void Awake()
    {

        HP = Random.Range(0, 50);
        SP = Random.Range(0, 50);

    }

    public void Start()
    {
        rend = GetComponent<Renderer>();

    }

    public void Update()
    {
        distance = Vector3.Distance(transform.position, obj.transform.position);
        //if (Vector3.Distance(transform.position, obj.transform.position) < 10)
        //{
        //    rend.material.color = Color.red;
        //}
    }
}
