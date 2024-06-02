using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public float platformGap = 10f;
    public float maxYOffset = 2f; // ������������ �������� ������������ �� ��� Y
    public float maxChangeInY = 0.1f; // ������������ ��������� ������� �� Y ����� �����������
    public GameObject[] platformPrefabs;
    public Transform player;
    public float distanceToGenerate = 300f;

    private float minY = 5f;
    private float maxY = 5f;
    private List<GameObject> activePlatforms = new List<GameObject>();
    private float lastEndPositionX;
    private float lastEndPositionY;
    private GameObject lastPlatformPrefab;

    void Start()
    {
        lastEndPositionY = player.position.y - 2;
        lastEndPositionX = player.position.x;
        SpawnInitialPlatforms();
    }

    void Update()
    {
        if (player.position.x + distanceToGenerate > lastEndPositionX)
        {
            SpawnPlatform();
        }

        if (activePlatforms.Count > 0 && activePlatforms[0].transform.position.x < player.position.x - distanceToGenerate)
        {
            DestroyPlatform();
        }
    }

    void SpawnInitialPlatforms()
    {
        for (int i = 0; i < 3; i++)
            SpawnPlatform();
    }

    void SpawnPlatform()
    {
        GameObject prefab; 
        do
        {
            prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        } while (prefab == lastPlatformPrefab);

        lastPlatformPrefab = prefab;

        var newPlatform = Instantiate(prefab);

        var numberOfLevelPlatform = Random.Range(2, 2);
        var startTransform = newPlatform.transform.Find($"Start{numberOfLevelPlatform}");
        var endTransform = newPlatform.transform.Find($"End{numberOfLevelPlatform}");

        var currentSpeed = player.GetComponent<RandKeyboard>().CurrentSpeed;
        var dynamicPlatformGap = platformGap + currentSpeed * 0.1f;

        var offsetX = activePlatforms.Count > 0 ? dynamicPlatformGap : 0;
        var newXPosition = lastEndPositionX + offsetX - startTransform.localPosition.x;

        var newYPosition = lastEndPositionY;
        if (activePlatforms.Count > 0)
        {
            var randomYOffset = Random.Range(-maxYOffset, maxYOffset);
            newYPosition = lastEndPositionY + randomYOffset;

            if (Mathf.Abs(newYPosition - lastEndPositionY) > maxChangeInY)
            {
                newYPosition = lastEndPositionY + Mathf.Sign(randomYOffset) * maxChangeInY;
            }

            newYPosition = Mathf.Clamp(newYPosition, minY, maxY);
        }

        newPlatform.transform.position = new Vector3(newXPosition, newYPosition - endTransform.localPosition.y, 0);

        lastEndPositionX = newXPosition + endTransform.localPosition.x - startTransform.localPosition.x;
        lastEndPositionY = newYPosition;

        activePlatforms.Add(newPlatform);
    }

    void DestroyPlatform()
    {
        var oldPlatform = activePlatforms[0];
        activePlatforms.RemoveAt(0);
        Destroy(oldPlatform);
    }
}
