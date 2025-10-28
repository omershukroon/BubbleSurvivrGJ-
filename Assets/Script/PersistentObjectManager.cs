using UnityEngine;

public class PersistentObjectManager : MonoBehaviour
{
    public static PersistentObjectManager Instance;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        if (Instance == null) 
        { 
            Instance = this; DontDestroyOnLoad(gameObject); 
        } 
        else 
        { 
            Destroy(gameObject); 
        }
    }
    void Start()
    {
        PlayerPrefs.DeleteAll();   // Delete all saved data
        PlayerPrefs.SetInt("SumStars", 0);
        PlayerPrefs.Save();        // Save changes to PlayerPrefs
    }
 
    public void SaveStarRating(int stageIndex, int stars)
    {
        int currentStars = PlayerPrefs.GetInt("Stage_" + stageIndex, 0);
        int currentSumStars = PlayerPrefs.GetInt("SumStars", 0);
        if (stars > currentStars)
        {
            PlayerPrefs.SetInt("Stage_" + stageIndex, stars);
            PlayerPrefs.SetInt("SumStars", currentSumStars + (stars - currentStars));
            PlayerPrefs.Save();
        }
    }

    public int GetStarRating(int stageIndex)
    {
        return PlayerPrefs.GetInt("Stage_" + stageIndex, 0);
    }
    public int GetSumStarRating()
    {
        return PlayerPrefs.GetInt("SumStars", 0);
    }
}
