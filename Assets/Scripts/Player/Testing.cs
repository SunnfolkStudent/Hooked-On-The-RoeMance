using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private JarkData fishData;

    private void Start()
    {
        Debug.Log(fishData.habitat);
        Debug.Log(fishData.fishName);
    }

    private void Update()
    {
        print(PlayerController.OceanEntryNumber);
    }
}
