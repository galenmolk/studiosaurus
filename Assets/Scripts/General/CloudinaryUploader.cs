using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using UnityEngine;

public static class CloudinaryUploader
{
    private static Cloudinary cloudinary;
    private static Cloudinary audioCloudinaryInstance;

    private const string API_KEY = "292789348116618";
    private const string API_SECRET = "9ZS8Nq3R1TqoBwxACibF5mmeQiM";
    private const string CLOUD_NAME = "cloudkeeper";

    public static string UploadImage(string filePath)
    {
        if (cloudinary == null)
            cloudinary = new Cloudinary(new Account(CLOUD_NAME, API_KEY, API_SECRET));

        ImageUploadParams uploadParams = new ImageUploadParams
        {
            File = new FileDescription(filePath)
        };

        var result = cloudinary.Upload(uploadParams);
        if (result.Error != null)
        {
            Debug.Log(result.Error.Message);
            return string.Empty;
        }

        return result.PublicId;
    }

    public static string UploadAudioClip(string filePath)
    {
        if (audioCloudinaryInstance == null)
            audioCloudinaryInstance = new Cloudinary(new Account(CLOUD_NAME, API_KEY, API_SECRET));

        RawUploadParams uploadParams = new RawUploadParams
        {
            File = new FileDescription(filePath)
        };

        var result = audioCloudinaryInstance.Upload(uploadParams, "raw");
        if (result.Error != null)
        {
            Debug.Log(result.Error.Message);
            return string.Empty;
        }

        return result.PublicId;
    }
}
