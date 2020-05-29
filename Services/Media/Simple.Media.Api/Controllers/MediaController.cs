﻿namespace Simple.Media.Api.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Simple.Core.Models.Common;
    using Simple.Media.Api.Helpers;

    [Route("api/media")]
    [Authorize]
    public class MediaController : Controller
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(string folder, IFormFile file)
        {
            if (file == null)
            {
                return NotFound(CommonConstants.Messages.FILE_REQUIRED);
            }

            var fileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));

            var task = Task.Run(() => FileHelper.SaveFile(folder, fileName, file));

            await Task.WhenAll(task);

            return Ok(Url.Action("GetFile", "Media", new { folder, fileName }, Request.Scheme));
        }

        [HttpPost("uploads")]
        public async Task<IActionResult> UploadFiles(string folder, IFormFile[] files)
        {
            if (files == null || files.Length <= 0)
            {
                return NotFound(CommonConstants.Messages.FILE_REQUIRED);
            }
            else
            {
                var result = string.Empty;

                foreach (var file in files)
                {
                    var fileName = string.Concat(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));

                    var task = Task.Run(() => FileHelper.SaveFile(folder, fileName, file));

                    await Task.WhenAll(task);

                    var link = Url.Action("GetFile", "Media", new { folder, fileName = string.Format(fileName) }, Request.Scheme);

                    result = string.Concat(result, "|", link);
                }
                return Ok(result.TrimStart('|'));
            }
        }

        [HttpGet("{folder}/{fileName}")]
        public async Task<IActionResult> GetFile(string folder, string fileName)
        {
            var task = Task.Run(() => FileHelper.GetFile(folder, fileName));

            await Task.WhenAll(task);

            var fileStream = System.IO.File.OpenRead(task.Result);

            string contentType = GetMineType(fileName);

            return base.File(fileStream, contentType);
        }

        private string GetMineType(string fileName)
        {
            string mineType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mineType = regKey.GetValue("Content Type").ToString();
            return mineType;
        }
    }
}
