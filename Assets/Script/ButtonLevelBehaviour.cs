using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro

public class buttonLevelBehaviour : MonoBehaviour
{
    private const int MAX_LEVEL = 11; // Change this to the last available level

    int numOfFullStars = 0;
    int numOfEmptyStars = 3;
    public GameObject[] FullStarsArr = new GameObject[3];
    public GameObject[] EmptyStarsArr = new GameObject[3];
    public GameObject LockImage;
    public TextMeshProUGUI ComingSoonText; // Reference to the TextMeshProUGUI component
    public bool IsLock;
    public int MinStarToUnLock;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        int stageIndex = transform.GetSiblingIndex() + 1;

        if (PersistentObjectManager.Instance != null)
        {
            int savedStars = PersistentObjectManager.Instance.GetStarRating(stageIndex);
            SetStars(savedStars, 3 - savedStars);
        }

        if (stageIndex <= 2)
        {
            MinStarToUnLock = 0;
        }
        else if (stageIndex > 2 && stageIndex < 10)
        {
            MinStarToUnLock = (int)((stageIndex - 1) * 3 - stageIndex / 2);
        }
        else
        {
            MinStarToUnLock = (int)((stageIndex - 1) * 3 - stageIndex / 3);
        }

        if (PersistentObjectManager.Instance != null)
        {
            int SumStars = PersistentObjectManager.Instance.GetSumStarRating();
            if (SumStars >= MinStarToUnLock)
            {
                LockImage.gameObject.SetActive(false);
                IsLock = false;
            }
            else
            {
                LockImage.gameObject.SetActive(true);
                IsLock = true;
            }
        }

        if (IsLock && button != null)
        {
            button.interactable = false;
        }

        // Check if this stage is coming soon
        if (stageIndex > MAX_LEVEL)
        {
            if (ComingSoonText != null)
            {
                ComingSoonText.gameObject.SetActive(true);
            }
        }
        else
        {
            if (ComingSoonText != null)
            {
                ComingSoonText.gameObject.SetActive(false);
            }
        }
    }

    public void SetStars(int FullStars, int EmptyStars)
    {
        for (int i = 0; i < FullStars; i++)
        {
            FullStarsArr[i].gameObject.SetActive(true);
            EmptyStarsArr[i].gameObject.SetActive(false);
        }
        numOfFullStars = FullStars;
        numOfEmptyStars = EmptyStars;
    }
}
