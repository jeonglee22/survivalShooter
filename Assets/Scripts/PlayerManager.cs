using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public UIManager uIManager;

    public float exp;
    private float expMax;
    public int level;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exp = 0f;
        level = 1;
        expMax = level * 50 + 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExp(float exp)
    {
        this.exp += exp;
        while (this.exp >= expMax)
        {
            this.exp -= expMax;
            level++;
            expMax = level * 50 + 100;
        }

        var expPercent = this.exp / expMax;
        uIManager.SetExpSlider(expPercent);
        uIManager.SetLevelText(level);
    }
}
