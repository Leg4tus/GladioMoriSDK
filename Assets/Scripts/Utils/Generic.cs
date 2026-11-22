using System;
using UnityEngine;

public static class Generic
{
    public const int customPlayerTextureMaxBytes = 150000;
    public static byte[] Texture2DToJpgEncodedByteArray(Texture2D tex)
    {
        byte[] bytes = null;
        try
        {
            int quality = 90;
            while ((bytes == null || bytes.Length > customPlayerTextureMaxBytes) && quality > 10)
            {
                bytes = tex.EncodeToJPG(quality);
                quality -= 10;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        return bytes;
    }
}
