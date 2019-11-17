using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public string sceneName;
    //private BoxCollider2D endLevelCollider;

    // Start is called before the first frame update
    void Start()
    {
        //endLevelCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player entered end level collider");

            // Todo?:Save player stats.
            SpongeBehavior.rockyUnlocked = false;
            SpongeBehavior.liamUnlocked = false;
            SceneManager.LoadScene(sceneName);
        }
    }
}
