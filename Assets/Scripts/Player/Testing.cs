using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private FishData fishData;

    private void Start()
    {
        Debug.Log(fishData.habitat);
        Debug.Log(fishData.fishName);
    }
}
