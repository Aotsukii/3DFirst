using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float walkBobbingSpeed = 14f;
    public float bobbingAmount = 0.04f;
    public MainController characterController;

    float defaultPosY = 0;
    float timer = 0;

    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        if (Mathf.Abs(characterController.moveDirection.x) > 0.1f || Mathf.Abs(characterController.moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * walkBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkBobbingSpeed), transform.localPosition.z);
        }
    }
}
