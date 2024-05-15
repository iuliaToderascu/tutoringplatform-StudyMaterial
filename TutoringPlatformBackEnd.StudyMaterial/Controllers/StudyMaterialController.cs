using Microsoft.AspNetCore.Mvc;
using TutoringPlatformBackEnd.StudyMaterial.Services;
using TutoringPlatformBackEnd.StudyMaterial.Model;
using TutoringPlatformBackEnd.StudyMaterial.Actor;

namespace TutoringPlatformBackEnd.StudyMaterial.Controllers
{
    [ApiController]
    [Route("studymaterials")]
    public class StudyMaterialController : ControllerBase
    {
        private readonly IStudyMaterialService _studyMaterialService;

        public StudyMaterialController(IStudyMaterialService studyMaterialService)
        {
            _studyMaterialService = studyMaterialService ?? throw new ArgumentNullException(nameof(studyMaterialService));
        }

        [HttpGet]
        public async Task<ActionResult<List<StudyMaterialModel>>> GetAllStudyMaterials()
        {
            var studyMaterials = await _studyMaterialService.GetAllStudyMaterialsAsync();
            return Ok(studyMaterials);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudyMaterialModel>> GetStudyMaterialById(string id)
        {
            var studyMaterial = await _studyMaterialService.GetStudyMaterialByIdAsync(id);
            if (studyMaterial == null)
            {
                return NotFound();
            }
            return Ok(studyMaterial);
        }

        [HttpGet("tutor/{tutorId}")]
        public async Task<ActionResult<List<StudyMaterialModel>>> GetStudyMaterialsByTutorId(string tutorId)
        {
            var studyMaterials = await _studyMaterialService.GetStudyMaterialsByTutorIdAsync(tutorId);
            return Ok(studyMaterials);
        }

        [HttpPost]
        public async Task<ActionResult<StudyMaterialModel>> CreateStudyMaterial([FromForm] StudyMaterialUploadModel studyMaterialUpload)
        {
            if (studyMaterialUpload == null || studyMaterialUpload.StudyMaterial == null)
            {
                return BadRequest("Invalid request payload");
            }

            // Process cover image upload
            if (studyMaterialUpload.CoverImage != null && studyMaterialUpload.CoverImage.Length > 0)
            {
                studyMaterialUpload.StudyMaterial.CoverImage = await GetFileBytes(studyMaterialUpload.CoverImage);
            }

            // Process content upload
            if (studyMaterialUpload.Content != null && studyMaterialUpload.Content.Length > 0)
            {
                studyMaterialUpload.StudyMaterial.Content = await GetFileBytes(studyMaterialUpload.Content);
            }

            var createdStudyMaterial = await _studyMaterialService.CreateStudyMaterialAsync(studyMaterialUpload.StudyMaterial);
            return CreatedAtAction(nameof(GetStudyMaterialById), new { id = createdStudyMaterial.Id }, createdStudyMaterial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudyMaterial(string id, StudyMaterialModel studyMaterial)
        {
            try
            {
                await _studyMaterialService.UpdateStudyMaterialAsync(id, studyMaterial);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating study material: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudyMaterial(string id)
        {
            try
            {
                await _studyMaterialService.DeleteStudyMaterialAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting study material: {ex.Message}");
            }
        }

        private async Task<byte[]> GetFileBytes(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public class StudyMaterialUploadModel
        {
            public StudyMaterialModel StudyMaterial { get; set; }
            public IFormFile CoverImage { get; set; }
            public IFormFile Content { get; set; }
        }
    }
}
