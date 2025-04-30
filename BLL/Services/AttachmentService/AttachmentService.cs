using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> allowedExtensions =  [".png",".jpg",".Jepg" ];
        const int maxSize = 2097152;
        public string? Upload(IFormFile file, string folderName)
        {
            // 1. check Extension
            var extension = Path.GetExtension(file.FileName);
            if(!allowedExtensions.Contains(extension))return null;
            // 2. check Size
            if(file.Length==0||file.Length>maxSize) return null;
            // 3.Get Located Folder Path
            //var folderPath = "D:\\C#\\projectroot\\projectroot\\wwwroot\\Files\\Images\\"; // local invalid
            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            // 4. Make Attachment Name Unique-- Guid
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            // 5. Get File Path
            var filePath=Path.Combine(folderPath, fileName);
            // 6. Create File Stream To Copy File[unmanaged]
            using FileStream fs = new FileStream(filePath,FileMode.Create);
            // 7. Use Stream To Copy File
            file.CopyTo(fs);
            // 8. Return File Name To Store It In Database
            return fileName;
        }
        public bool Delete(string fileName,string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName,fileName);
            if (!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }

        
    }
}
