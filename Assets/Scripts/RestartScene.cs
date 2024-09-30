using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    [SerializeField] GameObject player;

    public void Restart()
    {
        if (player == null)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
