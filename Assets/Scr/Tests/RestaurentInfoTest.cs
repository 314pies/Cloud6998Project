using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;


public class RestaurentInfoTest : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        using (var httpClient = new HttpClient())
        {

            //var reqPar = "numPeople=3&time=2021_4_9_12_15_30&restaurantId="+SelectedRestaurentID;
            var reqPar = "eH0bypB-IqUH73IVgEEfPA";
            using (var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://ir5pgsnsfk.execute-api.us-west-2.amazonaws.com/v_0_0/searchbyrid?q=" + reqPar))
            {
                var response = await httpClient.SendAsync(request);
                Debug.Log(response);
                string body = await response.Content.ReadAsStringAsync();
                Debug.Log(body);

               
                var stuff = (JArray)JsonConvert.DeserializeObject(body);
                var restarName = stuff[0]["name"];
                var image_url = stuff[0]["image_url"];
                Debug.Log(restarName);
                Debug.Log(image_url);                
            }

        }
    }

}
