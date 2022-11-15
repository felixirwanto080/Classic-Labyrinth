using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

public class CustomAnalyticsEvent : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] string level;
    [SerializeField] float score;
    [SerializeField] bool isAlive;

    // Start is called before the first frame update
    async void Start()
    {
        InitializationOptions options = new InitializationOptions();
        options.SetEnvironmentName("development");
        
        await UnityServices.InitializeAsync(options);

        await AnalyticsService.Instance.CheckForRequiredConsents();

        // Debug.Log("user id: " + AnalyticsService.Instance.GetAnalyticsUserID());

    }
    [SerializeField] float timer=61;
    private void Update(){
        if(timer > 60){
            timer += Time.deltaTime;
            // Cara 1
            // var parameters = new Dictionary<string, object>(){
            //     {

            //     }
            // };

            // Cara 2
            var parameters = new Dictionary<string, object>();
            parameters["Level"] = level;
            parameters["Health"] = health;
            parameters["Score"] = score;
            parameters["Alive"] = isAlive;

            AnalyticsService.Instance.CustomData("PlayerStats", parameters);

            health -= 1;
            score += 1;
            isAlive = !isAlive;

            timer = 0;
        }
    }
}
