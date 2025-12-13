using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UJAS.API.Controllers.Base;
using UJAS.Application.Common.Models;
using UJAS.Application.Profiles.Commands;
using UJAS.Application.Profiles.Dtos;
using UJAS.Application.Profiles.Queries;

namespace UJAS.API.Controllers.Profiles
{
    [ApiVersion("1.0")]
    public class ProfilesController : BaseApiController
    {
        /// <summary>
        /// Get current user's profile
        /// </summary>
        [HttpGet("my-profile")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Get my profile", Description = "Returns the current user's applicant profile.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ApplicantProfileDto>))]
        public async Task<IActionResult> GetMyProfile()
        {
            var query = new GetMyProfileQuery();
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update profile
        /// </summary>
        [HttpPut("my-profile")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Update profile", Description = "Updates the current user's applicant profile.")]
        [SwaggerResponse(200, "Profile updated", typeof(ApiResponse<ApplicantProfileDto>))]
        [SwaggerResponse(400, "Validation error")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateApplicantProfileDto profileDto)
        {
            var command = new UpdateMyProfileCommand { Profile = profileDto };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Upload resume
        /// </summary>
        [HttpPost("my-profile/resume")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Upload resume", Description = "Uploads a resume for the current user.")]
        [Consumes("multipart/form-data")]
        [SwaggerResponse(200, "Resume uploaded", typeof(ApiResponse<string>))]
        public async Task<IActionResult> UploadResume(IFormFile file)
        {
            var command = new UploadResumeCommand { File = file };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Add education history
        /// </summary>
        [HttpPost("my-profile/education")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Add education", Description = "Adds education history to profile.")]
        [SwaggerResponse(201, "Education added", typeof(ApiResponse<EducationHistoryDto>))]
        public async Task<IActionResult> AddEducation([FromBody] CreateEducationHistoryDto educationDto)
        {
            var command = new AddEducationHistoryCommand { Education = educationDto };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return CreatedAtAction(nameof(GetMyProfile), null, result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update education history
        /// </summary>
        [HttpPut("my-profile/education/{id}")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Update education", Description = "Updates education history.")]
        [SwaggerResponse(200, "Education updated", typeof(ApiResponse<EducationHistoryDto>))]
        public async Task<IActionResult> UpdateEducation(int id, [FromBody] UpdateEducationHistoryDto educationDto)
        {
            var command = new UpdateEducationHistoryCommand
            {
                EducationId = id,
                Education = educationDto
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Delete education history
        /// </summary>
        [HttpDelete("my-profile/education/{id}")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Delete education", Description = "Deletes education history.")]
        [SwaggerResponse(204, "Education deleted")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var command = new DeleteEducationHistoryCommand { EducationId = id };
            var result = await Mediator.Send(command);

            if (result.Success)
                return NoContent();

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Add work experience
        /// </summary>
        [HttpPost("my-profile/work-experience")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Add work experience", Description = "Adds work experience to profile.")]
        [SwaggerResponse(201, "Work experience added", typeof(ApiResponse<WorkExperienceDto>))]
        public async Task<IActionResult> AddWorkExperience([FromBody] CreateWorkExperienceDto workExperienceDto)
        {
            var command = new AddWorkExperienceCommand { WorkExperience = workExperienceDto };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return CreatedAtAction(nameof(GetMyProfile), null, result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Add skill
        /// </summary>
        [HttpPost("my-profile/skills")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Add skill", Description = "Adds a skill to profile.")]
        [SwaggerResponse(201, "Skill added", typeof(ApiResponse<SkillDto>))]
        public async Task<IActionResult> AddSkill([FromBody] CreateSkillDto skillDto)
        {
            var command = new AddSkillCommand { Skill = skillDto };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return CreatedAtAction(nameof(GetMyProfile), null, result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get profile completion status
        /// </summary>
        [HttpGet("my-profile/completion")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Get completion status", Description = "Returns profile completion percentage.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ProfileCompletionDto>))]
        public async Task<IActionResult> GetCompletionStatus()
        {
            var query = new GetProfileCompletionQuery();
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get applicant profile by ID (Admin/Manager view)
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "ManagerOrAbove")]
        [SwaggerOperation(Summary = "Get applicant profile", Description = "Returns applicant profile for admin/manager view.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ApplicantProfileDto>))]
        [SwaggerResponse(404, "Profile not found")]
        public async Task<IActionResult> GetApplicantProfile(int id)
        {
            var query = new GetApplicantProfileByIdQuery { ProfileId = id };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }
    }
}