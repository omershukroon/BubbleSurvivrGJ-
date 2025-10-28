using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffManagarBehaviour : MonoBehaviour
{
    public static BuffManagarBehaviour Instance;

    public GameObject BuffGem3Prefab;
    public GameObject BuffGem4Prefab;
    public GameObject BuffGem7Prefab;
    public GameObject BuffGem10Prefab;
    public GameObject HealBuffTimerPrefab;
    public GameObject RocksBuffTimerPrefab;
    public GameObject SlowdownBuffTimerPrefab;
    public GameObject SpeedBuffTimerPrefab;
    public GameObject BlessBuffTimerPrefab;
    public GameObject CurseBuffTimerPrefab;
    public ParticleSystem HealParticle;
    public ParticleSystem SlowDownParticle;
    public ParticleSystem SpeedParticle;
    public ParticleSystem CurseParticle;
    public GameObject RocksBuff;
    public PlayerBehaviourScript PlayerBehaviour;
    public WeaponThrower WeaponBehaviour;
    public Transform contentTransform;

    public BuffObject currentBuffObject;
    public BuffType currentBuffType;
    private BuffType previousBuffType;

    float minX = -330f;
    float maxX = 350f;
    float minY = -200f;
    float maxY = 195f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (HealParticle != null) HealParticle.gameObject.SetActive(true); HealParticle.Stop();
        if (SlowDownParticle != null) SlowDownParticle.gameObject.SetActive(true); SlowDownParticle.Stop();
        if (SpeedParticle != null) SpeedParticle.gameObject.SetActive(true); SpeedParticle.Stop();
        if (CurseParticle != null) CurseParticle.gameObject.SetActive(true); CurseParticle.Stop();

        // Initialize previousBuffType to an invalid value
        previousBuffType = (BuffType)(-1);
    }

    void Start()
    {
        Invoke("StartRandomBuffs", 20f);
    }

    void StartRandomBuffs()
    {
        InvokeRepeating("SpawnRandomBuff", 0f, Random.Range(8f, 18f));
    }

    void SpawnRandomBuff()
    {
        if (PlayerBehaviour != null && PlayerBehaviour.isGameOver) return;
        currentBuffObject = GetRandomBuffObject();
        currentBuffType = GetRandomBuffType();

        Vector3 randomPosition = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0f
        );

        GameObject buffObject = Instantiate(GetBuffPrefab(currentBuffObject), randomPosition, Quaternion.identity);
        BuffsBehaviour buffObjectTrigger = buffObject.GetComponent<BuffsBehaviour>();
        buffObjectTrigger.buffType = currentBuffType;

        Destroy(buffObject, 5f);
    }

    GameObject GetBuffPrefab(BuffObject buffObject)
    {
        switch (buffObject)
        {
            case BuffObject.Gem3: return BuffGem3Prefab;
            case BuffObject.Gem4: return BuffGem4Prefab;
            case BuffObject.Gem7: return BuffGem7Prefab;
            case BuffObject.Gem10: return BuffGem10Prefab;
            default: return null;
        }
    }

    BuffObject GetRandomBuffObject()
    {
        return (BuffObject)Random.Range(0, System.Enum.GetValues(typeof(BuffObject)).Length);
    }

    BuffType GetRandomBuffType()
    {
        int attempts = 0;
        BuffType randomBuffType;

        do
        {
            randomBuffType = (BuffType)Random.Range(0, System.Enum.GetValues(typeof(BuffType)).Length);

            // Handle special cases
 /*           if (randomBuffType == BuffType.IncreaseHealth2)
            {
                randomBuffType = BuffType.IncreaseHealth;
            }
            else if (randomBuffType == BuffType.IncreaseSpeed2)
            {
                randomBuffType = BuffType.IncreaseSpeed;
            }
 */
            attempts++;
            // Prevent infinite loop - if we've tried many times, just accept the result
            if (attempts > 20)
                break;

        } while (randomBuffType == previousBuffType);

        // Store the current buff type as the previous one for next time
        previousBuffType = randomBuffType;
        return randomBuffType;
    }

    public void ActivateBuff(BuffType buffType)
    {
        switch (buffType)
        {            
            case BuffType.DecreaseSpeed:
                PlayerBehaviour.SetSpeed(-30f);
                CreateBuffTimer(SlowdownBuffTimerPrefab);
                StartParticleEffect(SlowDownParticle, 10f);
                StartCoroutine(RevertSpeedAfterDelay(10f, -30f));
                break;
            
            case BuffType.IncreaseSpeed:
                PlayerBehaviour.SetSpeed(30f);
                CreateBuffTimer(SpeedBuffTimerPrefab);
                StartParticleEffect(SpeedParticle, 10f);
                StartCoroutine(RevertSpeedAfterDelay(10f, 30f));
                break;

            case BuffType.IncreaseHealth:
                PlayerBehaviour.IncreasingHealth(30f, 10f);
                CreateBuffTimer(HealBuffTimerPrefab);
                StartParticleEffect(HealParticle, 10f);
                break;



            case BuffType.ActivateRocks:
                RocksBuff.SetActive(true);
                CreateBuffTimer(RocksBuffTimerPrefab);
                StartCoroutine(DeactivateRocksAfterDelay(10f));
                break;

            case BuffType.Bless:
                WeaponBehaviour.SetIsBless(true);
                CreateBuffTimer(BlessBuffTimerPrefab);
                StartCoroutine(RemoveBlessAfterDelay(10f));
                break;

            case BuffType.Curse:
                PlayerBehaviour.setIsCurse(true);
                CreateBuffTimer(CurseBuffTimerPrefab);
                StartParticleEffect(CurseParticle, 10f);
                StartCoroutine(RemoveCurseAfterDelay(10f, false));
                break;

        }
    }

    void StartParticleEffect(ParticleSystem particleSystem, float duration)
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
            StartCoroutine(StopParticleEffectAfterDelay(particleSystem, duration));
        }
    }

    IEnumerator StopParticleEffectAfterDelay(ParticleSystem particleSystem, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
    }

    IEnumerator RemoveBlessAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        WeaponBehaviour.SetIsBless(false);
        WeaponBehaviour.SetWeaponCount(6); // Reset weapon count after bless ends
    }

    IEnumerator RemoveCurseAfterDelay(float delay, bool curse)
    {
        yield return new WaitForSeconds(delay);
        PlayerBehaviour.setIsCurse(curse);
    }

    IEnumerator RevertSpeedAfterDelay(float delay, float speedChange)
    {
        yield return new WaitForSeconds(delay);
        PlayerBehaviour.SetSpeed(-speedChange);
    }

    IEnumerator DeactivateRocksAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RocksBuff.SetActive(false);
    }

    void CreateBuffTimer(GameObject buffTimerPrefab)
    {
        if (buffTimerPrefab == null || contentTransform == null)
            return;

        GameObject newBuffTimer = Instantiate(buffTimerPrefab, contentTransform);
        newBuffTimer.transform.localScale = Vector3.one;

        Image bubbleTimerImage = newBuffTimer.transform.Find("BubbleTimer_IM")?.GetComponent<Image>();
        if (bubbleTimerImage != null && bubbleTimerImage.type == Image.Type.Filled)
        {
            bubbleTimerImage.fillAmount = 0f;
            StartCoroutine(FillImageOverTime(bubbleTimerImage, 10f));
        }

        Destroy(newBuffTimer, 10f);
    }

    IEnumerator FillImageOverTime(Image image, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            image.fillAmount = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
    }
}

public enum BuffObject { Gem3, Gem4, Gem7, Gem10 }
public enum BuffType { ActivateRocks, Bless, Curse, IncreaseHealth, DecreaseSpeed, IncreaseSpeed }
// IncreaseHealth2, IncreaseSpeed2,