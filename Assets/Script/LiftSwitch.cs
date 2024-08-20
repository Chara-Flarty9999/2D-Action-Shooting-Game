using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiftSwitch : MonoBehaviour
{
    [Tooltip("�X�C�b�`�̃g���K�[�� Player ���ڐG�������ɌĂԊ֐���o�^����B")]
    [SerializeField] UnityEvent _actions;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("�����͂��Ă邼");
                // �o�^�����֐����Ăяo���B
                _actions.Invoke();

            }
        }
        
        
    }
}