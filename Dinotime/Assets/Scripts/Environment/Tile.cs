using UnityEngine;

public  class Tile : MonoBehaviour
{
    public string blockName;
    public int mineLifetime;

    private int lifeLeft;

    public virtual void Collect()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifeLeft = mineLifetime;
    }
}
