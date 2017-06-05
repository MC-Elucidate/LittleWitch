
using UnityEngine;
using UnityEngine.UI;

public class TimerUpdate : MonoBehaviour {

    private Text time;
    // Use this for initialization
    void Start () {
        time = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        var currentTime = Time.timeSinceLevelLoad;

        int seconds = (int)(currentTime % 60);
        int minutes = (int)((currentTime / 60) % 60);
        int hours = (int)((currentTime / 3600) % 24);
        int days = (int)(currentTime / 86400);

        if (days > 0)
            time.text = System.String.Format("{0}:{1}:{2}:{3}", days, hours.ToString("00.##"), minutes.ToString("00.##"), seconds.ToString("00.##"));
        else if (hours > 0)
            time.text = System.String.Format("{0}:{1}:{2}", hours, minutes.ToString("00.##"), seconds.ToString("00.##"));
        else
            time.text = System.String.Format("{0}:{1}", minutes, seconds.ToString("00.##"));
    }
}
