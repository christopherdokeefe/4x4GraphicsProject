using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformSectionPrefab;

    public float DistanceFromTrebuchet;
    public GameObject[] platformSections;

    private float sizeOfSection;

    enum Sides { Bottom, Top, Left, Right}

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlatformSections();
    }

    private void SpawnPlatformSections()
    {
        // make sure the prefab is not null and it has the PlatformSection script
        Debug.Assert(platformSectionPrefab && platformSectionPrefab.GetComponent<PlatformSection>());

        // get the size of each section
        sizeOfSection = platformSectionPrefab.transform.localScale.x;
        int squaresPerSide = (int)(DistanceFromTrebuchet / sizeOfSection * 2);

        // creates an array of how many gameobjects are needed
        platformSections = new GameObject[(squaresPerSide - 1) * 4];
        Debug.Log(platformSections.Length);

        // four sides to spawn
        for (int i = 0; i < 4; i++)
        {
            // 
            for (int j = 0; j < squaresPerSide; j++)
            {
                platformSections[i * j] = Instantiate(platformSectionPrefab, transform) as GameObject;

                switch (i)
                {
                    case (int)Sides.Bottom:
                        platformSections[i * j].transform.position = new Vector3(-DistanceFromTrebuchet, 0, -DistanceFromTrebuchet + (j * sizeOfSection));
                        break;
                    case (int)Sides.Top:
                        platformSections[i * j].transform.position = new Vector3(DistanceFromTrebuchet, 0, -DistanceFromTrebuchet + ((j + 1) * sizeOfSection));
                        break;
                    case (int)Sides.Left:
                        platformSections[i * j].transform.position = new Vector3(-DistanceFromTrebuchet + ((j + 1) * sizeOfSection), 0, -DistanceFromTrebuchet);
                        break;
                    case (int)Sides.Right:
                        platformSections[i * j].transform.position = new Vector3(-DistanceFromTrebuchet + (j * sizeOfSection), 0, DistanceFromTrebuchet);
                        break;
                }
            }
        }
    }

    public void Reset()
    {
        int numberOfChildren = transform.childCount;
        for (int i = 0; i < numberOfChildren; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        SpawnPlatformSections();
    }
}
