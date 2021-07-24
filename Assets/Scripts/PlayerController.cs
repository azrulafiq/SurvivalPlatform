using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour, IPausable
{
    public UnityEvent OnPlayerLost;
    private Rigidbody m_Rb;
    private GameObject m_Elevator;
    public Camera followCamera;
    private float m_ElevatorOffsetY;
    public float speed ; //set speed for player, set via unity editor
    private float m_SpeedModifier;
    private Vector3 m_CameraPos;

    void Awake() 
    {
        m_Rb = GetComponent<Rigidbody>();
        m_ElevatorOffsetY = 0;
        m_CameraPos = followCamera.transform.position - m_Rb.position;
        m_SpeedModifier = 1;

        enabled = false;
    }
    void FixedUpdate()
    {
        //condition to restart game if player lose
        if (transform.position.y <= -15.0f)
        {
            OnPlayerLost.Invoke();
        }

        // Get Input from user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 playerPos = m_Rb.position;

        // Set movement with 3 coordinate based on user input.
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (movement == Vector3.zero)
        {
            return;
        }


        Quaternion targetRotation = Quaternion.LookRotation(movement);

        if (m_Elevator != null)
        {
            playerPos.y = m_Elevator.transform.position.y + m_ElevatorOffsetY;
        }

        //rotation
        targetRotation = Quaternion.RotateTowards(
            transform.rotation, 
            targetRotation, 
            360*Time.fixedDeltaTime);

        // Set position of player
        //transform.Translate(movement * speed * Time.deltaTime);

        m_Rb.MovePosition(playerPos + movement * m_SpeedModifier * speed * Time.fixedDeltaTime);
        m_Rb.MoveRotation(targetRotation);
    }

    private void LateUpdate() 
    {
        followCamera.transform.position = m_Rb.position + m_CameraPos;    
    }

    public void OnGameStart()
    {
        enabled = true;
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            m_SpeedModifier = 2;
            StartCoroutine(BonusSpeedCountDown());
        }

        if (other.gameObject.CompareTag("Enemy") && m_SpeedModifier > 1)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.transform.position - this.transform.position;
            enemyRb.AddForce(awayFromPlayer * 20.0f, ForceMode.Impulse);
        }
    }

    private IEnumerator BonusSpeedCountDown()
    {
        yield return new WaitForSeconds(10.0f);
        m_SpeedModifier = 1;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Elevator"))
        {
            m_Elevator = other.gameObject;
            m_ElevatorOffsetY = this.transform.position.y - m_Elevator.transform.position.y;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Elevator"))
        {
            m_Elevator = null;
            m_ElevatorOffsetY = 0;
        }
    }
}