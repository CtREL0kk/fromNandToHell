using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private float platformGap = 10f;
    [SerializeField] private float maxYOffset = 2f; // Максимальный диапазон рандомизации по оси Y
    [SerializeField] private float maxChangeInY = 0.1f; // Максимальное изменение позиции по Y между платформами
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private float distanceToDelete = 100f;
    [SerializeField] private GameObject firstPlatformPrefab;
    [SerializeField] private GameObject distanceTablePrefab;

    private float minY = 5f;
    private float maxY = 5f;
    private List<GameObject> activePlatforms = new List<GameObject>();
    private float lastEndPositionX;
    private GameObject lastPlatformPrefab;
    private float lastStartPosition;
    private DistanceCounter distanceCounter;
    private int distanceLastTable = 1;

    private void Awake()
    {
        distanceCounter = GameObject.Find("Random").GetComponent<DistanceCounter>();
    }

    private void Start()
    {
        lastEndPositionX = player.position.x;
        SpawnFirstPlatform();
        SpawnPlatform();
    }

    void Update()
    {
        if (player.position.x >= lastStartPosition && player.position.x <= lastEndPositionX)
        {
            SpawnPlatform();
        }

        if (activePlatforms.Count > 0 && activePlatforms[0].transform.position.x < player.position.x - distanceToDelete)
        {
            DestroyPlatform();
        }
    }

    private void SpawnFirstPlatform()
    { 
        var firstPlatform = Instantiate(firstPlatformPrefab);

        var endTransform = firstPlatform.transform.Find("End1");

        var xPos = player.position.x + endTransform.localPosition.x - 20;
        var yPos = player.position.y - endTransform.localPosition.y;
        firstPlatform.transform.position = new Vector3(xPos, yPos, 0);
        
        lastEndPositionX = endTransform.position.x;
        activePlatforms.Add(firstPlatform);
    }

    void SpawnPlatform(GameObject takePrefab = null)
    {
        GameObject prefab;
        if (takePrefab is null)
        {
            do
            {
                prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            } while (prefab == lastPlatformPrefab);
        }
        else
            prefab = takePrefab;
        
        lastPlatformPrefab = prefab;
        var newPlatform = Instantiate(prefab);

        var currentPlatformEndTransform = GetCurrentPlatformEndTransform();
        //Debug.Log("current end: " + currentPlatformEndTransform.position.x);
        Transform startTransform;
        Transform endTransform;
        var number = 0;
        while (true)
        {
            var newNumber = Random.Range(1, 4);
            if (number == newNumber) continue;
            number = newNumber;
            startTransform = newPlatform.transform.Find($"Start{number}");
            endTransform = newPlatform.transform.Find($"End{number}");
            if (startTransform != null) break;
        }

        var currentSpeed = player.GetComponent<RandKeyboard>().CurrentSpeed;
        var dynamicPlatformGap = platformGap + currentSpeed * 0.1f;

        var offsetX = dynamicPlatformGap;
        var newXPosition = currentPlatformEndTransform.position.x + offsetX - startTransform.localPosition.x;
        //Debug.Log("center x: " + newXPosition);

        var newYPosition = currentPlatformEndTransform.position.y;
        if (activePlatforms.Count > 0)
        {
            var randomYOffset = Random.Range(-maxYOffset, maxYOffset);
            newYPosition += randomYOffset;

            if (Mathf.Abs(newYPosition - currentPlatformEndTransform.position.y) > maxChangeInY)
            {
                newYPosition = currentPlatformEndTransform.position.y + Mathf.Sign(randomYOffset) * maxChangeInY;
            }

            newYPosition = Mathf.Clamp(newYPosition, minY, maxY);
        }

        newPlatform.transform.position = new Vector3(newXPosition, newYPosition - endTransform.localPosition.y, 0);

        lastEndPositionX = newXPosition + endTransform.localPosition.x;
        //Debug.Log("lastEndPos: " + lastEndPositionX);
        lastStartPosition = startTransform.position.x;

        activePlatforms.Add(newPlatform);
    }

    Transform GetCurrentPlatformEndTransform()
    {
        var playerX = player.position.x;
        var playerY = player.position.y;

        foreach (var platform in activePlatforms)
        {
            var start1 = platform.transform.Find("Start1");
            var end1 = platform.transform.Find("End1");
            //Debug.Log("X:" + "player:" + playerX + "coord:" + start1.position.x + " " + end1.position.x);
            if (playerX >= start1.position.x && playerX <= end1.position.x)
            {
                //Debug.Log("In bounds");
                for (var i = 3; i >= 1; i--)
                {
                    var endTransform = platform.transform.Find($"End{i}");
                    if (endTransform is null) continue;
                    //Debug.Log("Y:" + "player:" + playerY + "platform:" + endTransform.position.y);
                    //Debug.Log(endTransform != null && playerY >= endTransform.position.y);
                    if (endTransform != null && playerY >= endTransform.position.y)
                    {
                        return endTransform;
                    }
                }
            }
        }

        Debug.Log("Platform under stickman not found");
        var lastPlatform = activePlatforms[activePlatforms.Count - 1];
        return lastPlatform.transform.Find("End1");
    }
    void DestroyPlatform()
    {
        var oldPlatform = activePlatforms[0];
        activePlatforms.RemoveAt(0);
        Destroy(oldPlatform);
    }
}
