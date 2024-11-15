﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LaundryAPI.Controllers;
using Amazon.S3;
using LaundryAPI.Services;

namespace LaundryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase

    {
        private readonly IS3Service _s3Service;

        public FilesController(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            try
            {
                var result = await _s3Service.UploadFileAsync(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("download/{fileKey}")]
        public async Task<IActionResult> DownloadFile(string fileKey)
        {
            try
            {
                var fileBytes = await _s3Service.DownloadFileAsync(fileKey);
                return File(fileBytes, "application/octet-stream", fileKey);
            }
            catch (AmazonS3Exception ex)
            {
                return NotFound($"Error encountered on server: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
