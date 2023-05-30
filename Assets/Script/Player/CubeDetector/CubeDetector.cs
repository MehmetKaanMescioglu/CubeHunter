using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDetector : MonoBehaviour
{
    AudioSource audioSource;
    
    public CubeSound music;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Debug.Log($"Cube {collision.gameObject.name}");

            var cubeBehaviour = collision.gameObject.GetComponent<CubeBehaviour>();
            
            if (!cubeBehaviour.isStacked)
            {
                // TODO : GetCube 

                PlayerCubeManager.Instance.GetCube(cubeBehaviour);
                audioSource.Play();
            }
        }

        if (collision.gameObject.CompareTag("Final"))
        {
            Debug.Log("Finish! oyun bitti");

            var cubeBehaviour = collision.gameObject.GetComponent<CubeBehaviour>();
            PlayerCubeManager.Instance.FinishGame(cubeBehaviour);
            // TODO : GetCube 
            
            //PlayerCubeManager.Instance.GetCube(cubeBehaviour);
            
        }

        if (collision.gameObject.CompareTag("Diamond"))
        {
            Debug.Log("You Get Point!");

            var cubeBehaviour = collision.gameObject.GetComponent<CubeBehaviour>();
            PlayerCubeManager.Instance.GetPoint(cubeBehaviour);
            Destroy(collision.gameObject);
            
            // TODO : GetCube 

            //PlayerCubeManager.Instance.GetCube(cubeBehaviour);

        }

        if(collision.gameObject.CompareTag("Accelerator"))
        {
            Debug.Log("Hýzlaniciya Girildi!");
            var cubeBehaviour = collision.gameObject.GetComponent<CubeBehaviour>();
            PlayerCubeManager.Instance.AcceleratePlayer(cubeBehaviour);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Slowdown"))
        {
            Debug.Log("Yavaslaticiya Girildi!");
            var cubeBehaviour = collision.gameObject.GetComponent<CubeBehaviour>();
            PlayerCubeManager.Instance.SlowdownPlayer(cubeBehaviour);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("NormalSpeed"))
        {
            Debug.Log("Normal Hiza Girildi!");
            var cubeBehaviour = collision.gameObject.GetComponent<CubeBehaviour>();
            PlayerCubeManager.Instance.NormalSpeedPlayer(cubeBehaviour);
            Destroy(collision.gameObject);
        }

        /*if (collision.gameObject.CompareTag("Volcanic"))
        {
            Debug.Log("Volkana Girildi!");
            var cubeBehaviour = collision.gameObject.GetComponent<CubeBehaviour>();
            PlayerCubeManager.Instance.DropCube(cubeBehaviour);
            Destroy(collision.gameObject);
            //Destroy(collision.gameObject.GetComponent<CubeBehaviour>());
        }*/
    }
}
