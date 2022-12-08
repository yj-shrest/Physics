using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform track;
    public Transform weight;
    public CharacterController control;
    public float speed;
    public Vector3 velocity = new Vector3(0,0,0);
    bool inSlope= false;
    public bool check,isGround = true;
    bool inrotate,rotate,inseesaw = false;
    public float movex, movey;
    float rotate2=0f;
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        rotate2 += 14f;
        rotate2 = Mathf.Clamp(rotate2, -14f, 14f);
        weight.localRotation = Quaternion.Euler(rotate2, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
          movex = Input.GetAxis("Horizontal");
          movey = Input.GetAxis("Vertical");
        move = transform.right * movex * speed * Time.deltaTime + transform.forward * movey * speed*Time.deltaTime;
        control.Move(move);

        inseesaw = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 3f, LayerMask.GetMask("Seesaw"));
        if (check)
        {
        inrotate = Physics.Raycast(transform.position , transform.TransformDirection(Vector3.down), 3f, LayerMask.GetMask("Rotate"));
        }
        rotate = Physics.Raycast(transform.position  , transform.TransformDirection(Vector3.down), 3f, LayerMask.GetMask("Rotate"));
        isGround = Physics.Raycast(transform.position  , transform.TransformDirection(Vector3.down), 3f, LayerMask.GetMask("Ground"));

        inSlope = Physics.Raycast(transform.position  , transform.TransformDirection(Vector3.down), 7f, LayerMask.GetMask("Slope"));
        Debug.DrawRay(transform.position , transform.TransformDirection(Vector3.down)*3f,Color.black);
        velocity.y += -9.86f *2 * Time.deltaTime;
        if((isGround || inseesaw) && velocity.y<0)
        {
            velocity.y = -20f;
        }
        if (Input.GetButtonDown("Jump") && (isGround || inseesaw))
        {
            velocity.y = 15f;
        }
        control.Move(velocity * Time.deltaTime);
        if(inSlope)
        {
            velocity.z = -12f;
        }
        else
        {
            velocity.z = 0f;
        }
        if (inrotate && movex == 0 && movey == 0)
        { 
            inrotate = false;
            check = false;
            track.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) ;

        }
        if(rotate && movex!=0 || movey !=0)
        {
            check = true;
        }
        if (rotate && movex==0 && movey==0)
        {
            transform.position = new Vector3(track.position.x, track.position.y, track.position.z);
        }
        if(isGround)
        {
            check = true;
        }
        if(inseesaw)
        {
            if (transform.position.z > weight.position.z)
            {
                rotate2 += (transform.position.z-weight.position.z)/100f;
                rotate2 = Mathf.Clamp(rotate2, -14f, 14f);
                weight.localRotation = Quaternion.Euler(rotate2, 0f, 0f);
            }
            else
            {
                rotate2 -=(weight.position.z-transform.position.z)/100f;
                rotate2 = Mathf.Clamp(rotate2, -14f, 14f);
                weight.localRotation = Quaternion.Euler(rotate2, 0f, 0f);
            }
        }
    }
   
}