using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using Unity.Services.Core.Environments;
using Unity.Services.Authentication;
using System;

public struct userAttributes{
    public int characterLevel;
}

public struct appAttributes{

}

public class RemoteConfigFetcher : MonoBehaviour
{
    [SerializeField] string environmentName;
    [SerializeField] int characterLevel;
    [SerializeField] bool fetch;
    [SerializeField] float gravity;
    [SerializeField] PhoneGravity phoneGravity;
    // Start is called before the first frame update
    async void Awake()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName(environmentName);
        await UnityServices.InitializeAsync(options);

        Debug.Log("UGS Initialized");

        if(AuthenticationService.Instance.IsSignedIn==false)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        Debug.Log("Player Signed In");
        RemoteConfigService.Instance.FetchCompleted += OnFetchConfig;

        Debug.Log("Fetch Data");
        RemoteConfigService.Instance.FetchConfigs(
            new userAttributes(){characterLevel=this.characterLevel},
            new appAttributes()
        );
    }
    private void OnDestroy(){
        RemoteConfigService.Instance.FetchCompleted -= OnFetchConfig;
    }

    private void OnFetchConfig(ConfigResponse response)
    {
        Debug.Log(response.requestOrigin);
        Debug.Log(response.body);
        switch(response.requestOrigin){
            case ConfigOrigin.Default: // Jika tidak jadi bagian dari userAttributes akan mengambil nilai biasa
                break;
            case ConfigOrigin.Cached: // Ambil dari lokal jika belum ada
                break;
            case ConfigOrigin.Remote: // Sesuai dengan target
                gravity = RemoteConfigService.Instance.appConfig.GetFloat("Gravity");
                phoneGravity.SetGravityMagnitude(gravity);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fetch){
            fetch = false;
            Debug.Log("Fetch Data");
            RemoteConfigService.Instance.FetchConfigs(
                new userAttributes(){characterLevel=this.characterLevel},
                new appAttributes()
            );
        }
    }
}
