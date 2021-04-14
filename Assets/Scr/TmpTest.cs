using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;


public class TmpTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var stuff = (JObject)JsonConvert.DeserializeObject("{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }");
        Debug.Log(stuff["Name"]);


        //string name = stuff.Name;
        //string address = stuff.Address.City;
        //Debug.Log(address);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
