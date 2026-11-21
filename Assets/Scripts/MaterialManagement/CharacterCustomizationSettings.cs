using System;
using UnityEngine;
using UnityEngine.Assertions.Must;


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

    public Texture2D imageTexture = null;

    
    public string customTextureBase64;

}
