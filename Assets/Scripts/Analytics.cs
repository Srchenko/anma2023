using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class Analytics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /* try
        {
            await UnityServices.InitializeAsync();
            List<string> id = await AnalyticsService.Instance.CheckForRequiredConsents();
            Debug.Log("se inicia");
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e);
            throw;
        } */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
