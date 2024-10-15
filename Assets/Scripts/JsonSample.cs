using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine.UI;

public class JsonSample : MonoBehaviour
{
    // Start is called before the first frame update
    private Dictionary<string, int> dic2 = new Dictionary<string, int>();
    void Start()
    {
        var character = new Sample()
        {
            characterLevel = 1,
            characterName = "Goerge",
            chosenClass = CharacterClass.Warrior,
            dateCreated = DateTime.Now
        };
        
        Debug.Log(JsonConvert.SerializeObject(character));
        
        // var dic1 = "{\"A\":3, \"B\":4}";
        // dic2 = JsonConvert.DeserializeObject<Dictionary<string, int>>(dic1);
        //
        // string text = string.Empty;
        //
        // foreach (var kvp in dic2)
        // {
        //     text = $"{kvp.Key} : {kvp.Value}";
        //     Debug.Log(text);
        // }
        
        
    }

}

public enum CharacterClass
{
    Warrior,
    Magician,
    Assassin
}

[Serializable]
public struct Sample
{
    [JsonConverter(typeof(StringEnumConverter))]
    public CharacterClass chosenClass;
    public int characterLevel;
    public string characterName;
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime dateCreated;
}

[Serializable]
public struct Enemy
{
   
    [JsonProperty] private float _resistance;
    [JsonProperty] private float _blockChance;

    public float Resistance => _resistance;
    public float BlockChance => _blockChance;
}

[Serializable]
public struct FreeIpAPIResponse
{
    public int ipVersion;
    public string ipAddress;
    public float latitude;
    public float longitude;
    public string countryName;
    public string countryCode;
}

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString("yy-MM-dd"));
    }

    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        return DateTime.ParseExact(reader.Value.ToString(), "yy-MM-dd", null);
    }
}
