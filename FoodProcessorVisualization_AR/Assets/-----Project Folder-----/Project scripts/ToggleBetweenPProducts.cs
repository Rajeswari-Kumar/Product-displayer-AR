using UnityEngine;

public class ToggleBetweenProducts : MonoBehaviour
{

    public GameObject[] Products;

    public void ToggleProducts(string ProductName)
    {
        foreach (var p in Products)
        {
            if (p.name == ProductName)
            {
                p.SetActive(true);
            }
            else if (p.name != ProductName)
            {
                p.SetActive(false);
            }
        }
    }
}
