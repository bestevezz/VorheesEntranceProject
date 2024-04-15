using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public KeyCode createKey = KeyCode.Alpha1;
    public KeyCode toggleSpawnKey = KeyCode.Space;
    public KeyCode rotateKey = KeyCode.R;
    public KeyCode opacityKey = KeyCode.O;
    public int maxSpawnedCubes = 10;
    public float rotationSpeed = 50f;
    public float fadeInOutSpeed = 0.5f;
    private GameObject initialCube;
    private GameObject[] spawnedCubes;
    private bool cubesSpawned = false;
    private bool isRotating = false;
    private bool isFading = false;

    void Start()
    {
        // Instantiate initial cube
        initialCube = Instantiate(cubePrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(createKey))
        {
            CreateCube();
        }

        if (Input.GetKeyDown(toggleSpawnKey))
        {
            ToggleSpawnCubes();
        }

        if (Input.GetKeyDown(rotateKey))
        {
            isRotating = !isRotating;
        }

        if (Input.GetKeyDown(opacityKey))
        {
            isFading = !isFading;
        }

        if (isRotating)
        {
            RotateCubes();
        }

        if (isFading)
        {
            ToggleOpacity();
        }
    }

    void CreateCube()
    {
        // Instantiate cube at specified location
        Instantiate(cubePrefab, transform.position, Quaternion.identity);
    }

    void ToggleSpawnCubes()
    {
        if (!cubesSpawned)
        {
            // Spawn cubes
            spawnedCubes = new GameObject[maxSpawnedCubes];
            for (int i = 0; i < maxSpawnedCubes; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
                spawnedCubes[i] = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            }
            cubesSpawned = true;
        }
        else
        {
            // Delete spawned cubes
            foreach (GameObject cube in spawnedCubes)
            {
                Destroy(cube);
            }
            cubesSpawned = false;
        }
    }

    void RotateCubes()
    {
        // Rotate all spawned cubes
        foreach (GameObject cube in spawnedCubes)
        {
            cube.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void ToggleOpacity()
    {
        // Toggle opacity of all spawned cubes
        foreach (GameObject cube in spawnedCubes)
        {
            Renderer renderer = cube.GetComponent<Renderer>();
            Color currentColor = renderer.material.color;
            Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, isFading ? Mathf.PingPong(Time.time * fadeInOutSpeed, 1f) : 1f);
            renderer.material.color = targetColor;
        }
    }
}



