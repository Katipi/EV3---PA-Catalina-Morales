using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
public class Generator : MonoBehaviour
{
    public Vector2 genCount = new Vector2(100, 100);
    public GameObject prefabPasto; 
    public GameObject prefabAgua; 
    public GameObject prefabNieve;

    public float maxHeight; 
    public float noiseScale;

    private List<Vector3> highPosition = new List<Vector3>(); 

    // Start is called before the first frame update
    void Start()
    {
        Vector2 offset = new Vector2(50f, 50f); 
       
        for (int x = 0; x < genCount.x; x++)
        {
            for (int z = 0; z < genCount.y; z++)
            {
                float coordX = (x * noiseScale);
                float coordY = (z * noiseScale);

                float noise = Mathf.PerlinNoise(coordX, coordY);

                int height = Mathf.RoundToInt(noise * maxHeight);
                Vector3 position = new Vector3(x, height, z);
                highPosition.Add(position); 

                for (int y = 0; y < height; y++)
                {
                    GameObject prefab;

                    if (noise < 0.33f)
                    {
                        prefab = prefabAgua;
                    }
                    else if (noise < 0.66f)
                    {
                        prefab = prefabPasto; 
                    }
                    else 
                    {
                        prefab = prefabNieve;
                    }

                    Instantiate(prefab, new Vector3(x,y,z), Quaternion.identity);

                }

            }
        }

        GeneratorList();

    }

    void GeneratorList()
    {
        PositionList poslist = new PositionList();
        poslist.positions = highPosition;

        string json = JsonUtility.ToJson(poslist, true);

        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "ListaDePosiciones.json");

        File.WriteAllText(filePath, json);

        if (File.Exists(filePath))
        {
            json = File.ReadAllText(filePath);
            PositionList leerInfo = JsonUtility.FromJson<PositionList>(json);

            int conteo = leerInfo.positions.Count;
            for (int i = 0; i < conteo; i++)
            {
                Debug.Log($"Posición leída desde json {i}: {leerInfo.positions[i]}");
            }
        }
        else
        {
            Debug.Log("No existe archivo json"); 
        }
    }

    [System.Serializable]
    public class PositionList
    {
        public List<Vector3> positions; 
    }
}


