using Business.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        public string FileSaveToServer(IFormFile file, string filePath)
        {
            var fileFormat = Path.GetExtension(file.FileName).ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;
            string path = Path.Combine(filePath, fileName);

            using (var stream = File.Create(path))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        public async Task<string> FileSaveToFtp(IFormFile file)
        {
            var fileFormat = Path.GetExtension(file.FileName).ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;

            using (var httpClient = new HttpClient())
            {
                var ftpAddress = "FTP Adresiniz yazılacak" + fileName;
                using (var ftpStream = await httpClient.GetStreamAsync(ftpAddress))
                {
                    await file.CopyToAsync(ftpStream);
                }
            }

            return fileName;
        }

        public byte[] FileConvertByteArrayToDatabase(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                return fileBytes;
            }
        }

        public void FileDeleteToServer(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                // Hata durumuyla ilgili bir işlem yapılabilir
            }
        }

        public void FileDeleteToFtp(string path)
        {
            try
            {
                var ftpAddress = "ftp adresi" + path;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DeleteAsync(ftpAddress).Wait();
                }
            }
            catch (Exception)
            {
                // Hata durumuyla ilgili bir işlem yapılabilir
            }
        }

    }
}
