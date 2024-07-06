﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Route.C41.PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file, string folderName)
        {
            //1-Get Located Folde Path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            
            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //2-Get File Name And Make It Unique
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            //3-Get File Path
            string filePath=Path.Combine(folderPath, fileName);

            //4-Save File As a Streams [Data Per Time]
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return fileName;

        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);

        }
    }
}
