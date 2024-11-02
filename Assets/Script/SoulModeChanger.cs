using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulModeChanger : MonoBehaviour
{
    PlayerController _playerState;
    [SerializeField]PlayerController.SoulMode _changeMode;
    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerState = _player.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player.
        }
    }
}
