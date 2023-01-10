
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    public float slowdownFactor = 0.05f;
    public float normaliseAgain = 1f;
    public bool activate = false;

    public void Slowmotion()
    {
        if (activate == false)
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.2f;
            activate = true;
        }
        else
        {
            Time.timeScale = normaliseAgain;
            Time.fixedDeltaTime = Time.timeScale;
            activate = false;
        }
    }
}
