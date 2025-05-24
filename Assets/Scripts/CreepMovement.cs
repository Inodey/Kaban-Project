using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepMovement : MonoBehaviour
{
    public Transform campPos;
    private NavMeshAgent neutralAgent;
    private Transform playerPos;
    private Vector3 normalScale;
    private Vector3 puffedScale;
    public float scaleSpeed = 1f;
    private bool puffed = false;
    public ParticleSystem walkParticles;    
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalScale = transform.localScale;
        puffedScale = new Vector3(2f, 2f, 2f);
        neutralAgent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.Find("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerPos.position, transform.position);
        float distanceToCamp = Vector3.Distance(campPos.position, transform.position);
        if(distanceToPlayer > 5f || distanceToCamp > 20f){
            GoCampBehaviour();
            transform.localScale = Vector3.Lerp(transform.localScale, normalScale, Time.deltaTime * scaleSpeed);
        }
        else if(distanceToPlayer <= 5f){
            StalkBehaviour();
            transform.localScale = Vector3.Lerp(transform.localScale, puffedScale, Time.deltaTime * scaleSpeed);
        }
        
    }
    private void GoCampBehaviour(){
        neutralAgent.destination = campPos.position;
        if (walkParticles.isPlaying)
        {
            walkParticles.Stop();
        }
    }
    private void StalkBehaviour(){
        neutralAgent.destination = playerPos.position;
        float angle = Mathf.Clamp(rb.velocity.x, -1, 1) * -90f;
        walkParticles.transform.rotation = Quaternion.Euler(0, angle, 90);
        walkParticles.startSpeed = Mathf.Abs(rb.velocity.x);

        if (!walkParticles.isPlaying)
        {
            walkParticles.Play();
        }
    }
}
