using Newtonsoft.Json;
using System;
using UnityEngine;


[System.Serializable]
public class CharacterCustomizationSettings
{
    public PlayerMaterialSettings materialSettings = new PlayerMaterialSettings
    {
        selectedMaterial = 0,
        useMaterialDiffuse = false
    };
    public string customTexture = "";
    public CustomTextureType customTextureType = CustomTextureType.None;

    [JsonIgnore]
    public Texture2D imageTexture = null;

    [HideInInspector]
    public string customTextureBase64;


    public void GenerateBase64FromImageTexture()
    {
        customTextureBase64 = null;
        if(imageTexture == null)
        {
            return;
        }
        byte[] imageBytes = Generic.Texture2DToJpgEncodedByteArray(imageTexture);
        customTextureBase64 = Convert.ToBase64String(imageBytes);
    }

}
