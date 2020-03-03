using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    private Game game;
    private bool jump;
    [SerializeField]
    private int coins;
    [SerializeField]
    private TextMeshProUGUI coinText;
    private GameObject sword;

    private Vector3 inputVector;

    void Start()
    {
        sword = transform.GetChild(0).gameObject;
        game = FindObjectOfType<Game>();
        rb = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal") * 10f, rb.velocity.y, Input.GetAxis("Vertical") * 10f);
        transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Attack"))
        {
            PerformAttack();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = inputVector;
        if (jump && IsGrounded())
        {
            rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
            jump = false;
        }
    }

    private void PerformAttack()
    {
        if (!sword.activeSelf)
        {
            sword.SetActive(true);
        }
    }

    bool IsGrounded()
    {
        float distance = GetComponent<Collider>().bounds.extents.y + 0.01f;
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.Raycast(ray, distance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            game.ReloadCurrentLevel();   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Diamond":
                coins++;
                Destroy(other.gameObject);
                coinText.text = string.Format("Coins\n{0}", coins);
                break;
            case "Goal":
                other.GetComponent<Goal>().CheckForCompletetion(coins);
                break;
            default:
                break;
        }
    }
}
