using System.Collections;
using UnityEngine;

public class StarDisplayController : MonoBehaviour
{
    private StarHolder[] starHolders;
    [SerializeField] private float timeBetweenStars;
    private bool skipStars = false;

    public static StarDisplayController Instance;

    private void Awake()
    {
        #region Singleton

        if (Instance != null)
        {
            Debug.Log($"There are two DisplayController in the scene. Deleting {this.name}.");
            Destroy(this);
            return;
        }
        Instance = this;

        #endregion


        starHolders = GetComponentsInChildren<StarHolder>();
    }

    private void Start()
    {
        StarSpawnStart(successfullStarCount);
    }

    public void StarSpawnStart(int brightStarCount)
    {
        StartCoroutine(SpawnStarts(brightStarCount));
    }

    private IEnumerator SpawnStarts(int brightStartCount)
    {
        float elapsed = timeBetweenStars;
        int spawnedStars = 0;
        while (spawnedStars < brightStartCount)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timeBetweenStars)
            {
                elapsed = skipStars ? elapsed : 0f;
                starHolders[spawnedStars].Spawn(true);
                spawnedStars++;
            }

            if (!skipStars)
            {
                skipStars = Input.GetKeyDown(KeyCode.Space);
                yield return null;
            }
        }
        while (spawnedStars < starHolders.Length)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= timeBetweenStars)
            {
                elapsed = skipStars ? elapsed : 0f;
                starHolders[spawnedStars].Spawn(false);
                spawnedStars++;
            }

            if (!skipStars)
            {
                skipStars = Input.GetKeyDown(KeyCode.Space);
                yield return null;
            }
        }
        skipStars = false;

        //display sucess/failure text
    }






    #region Testing

    [SerializeField] private int successfullStarCount;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipStars = true;
        }
    }

    #endregion

}
