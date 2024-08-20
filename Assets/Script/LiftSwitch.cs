using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiftSwitch : MonoBehaviour
{
    [Tooltip("スイッチのトリガーに Player が接触した時に呼ぶ関数を登録する。")]
    [SerializeField] UnityEvent _actions;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("タグは反応はしてるぞ");
            if (Input.GetKey(KeyCode.C))
            {
                Debug.Log("反応はしてるぞ");
                // 登録した関数を呼び出す。
                _actions.Invoke();

            }
        }
        
        
    }
}