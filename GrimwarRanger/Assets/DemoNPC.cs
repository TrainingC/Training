using UnityEngine;
using System.Collections;

public class DemoNPC : MonoBehaviour {
    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        /*      if (controller.isGrounded)
              { //地面についているか判定
                  if (Input.GetMouseButtonDown(0))
                  {
                      moveDirection.y = 3; //ジャンプするベクトルの代入
                  }
              }

              moveDirection.y -= 10 * Time.deltaTime; //重力計算
              controller.Move(moveDirection * Time.deltaTime); //cubeを動かす処理*/
    }

    public void Jump()
    {
        if (controller.isGrounded)
        { //地面についているか判定
            if (Input.GetMouseButtonDown(0))
            {
                moveDirection.y = 5; //ジャンプするベクトルの代入
            }
        }

        moveDirection.y -= 10 * Time.deltaTime; //重力計算
        controller.Move(moveDirection * Time.deltaTime); //cubeを動かす処理
    }
}
