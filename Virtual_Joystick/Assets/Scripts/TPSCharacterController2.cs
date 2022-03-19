using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController2 : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private VirtualJoystick02 moveJoystick;
    [SerializeField]
    private VirtualJoystick02 cameraJoystick;


    public Animator animator;

    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    void Update()
    {
        LookAround();
        Move();
    }

    private void Move()
    {
        //Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 moveInput = new Vector2(moveJoystick.horizontal, moveJoystick.vertical);
        bool isMove = moveInput.magnitude != 0;
        animator.SetBool("isMove", isMove);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            //characterBody.forward = lookForward; // 캐릭터가 바라보는 정면은 카메라가 바라보는 정면
            characterBody.forward = moveDir; // 캐릭터가 바라보는 정면은 입력된 방향에 맞춰 바라본다. 
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }

    private void LookAround()
    {
        //Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 mouseDelta = new Vector2(cameraJoystick.horizontal*-1, cameraJoystick.vertical*-1);
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
