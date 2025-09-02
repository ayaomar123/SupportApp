using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

using SupportApp.Api.Requests.Categories;
using SupportApp.Application.Features.Categories.Commands.CreateCategory;
using SupportApp.Application.Features.Categories.Commands.RemoveCategory;
using SupportApp.Application.Features.Categories.Commands.UpdateCategory;
using SupportApp.Application.Features.Categories.Commands.UpdateCategoryStatus;
using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Application.Features.Categories.Queries.GetCategories;

namespace SupportApp.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "EmployeeOnly")]
    [ApiController]
    public class CategoriesController(ISender sender) : ApiController
    {
        [HttpGet(Name = "GetCategories")]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrieves a list of Categories.")]
        [EndpointDescription("Returns all Categories")]
        [ProducesDefaultResponseType]
        [OutputCache(Duration = 60)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetCategoriesQuery(), ct);

            return result.Match(
                response => Ok(response),
                Problem);
        }

        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new category.")]
        [EndpointDescription("Adds a new category to the system.")]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryRequest request, CancellationToken ct)
        {
            if (request.Image is null || request.Image.Length == 0)
            {
                return BadRequest("Image is required.");
            }

            var appFile = BuildFileUpload(request);

            var result = await sender.Send(
                new CreateCategoryCommand(
                    request.Title,
                    appFile,
                    request.Priority),
                ct);

            return result.Match(
                response => CreatedAtRoute(
                    routeName: "GetCategories",
                    value: response),
                Problem);
        }

        [HttpPut("{categoryId:guid}", Name = "UpdateCategory")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing category.")]
        [EndpointDescription("Updates a category.")]
        public async Task<IActionResult> UpdateCategory(
            [FromRoute] Guid categoryId,
            [FromForm] CategoryRequest request,
            CancellationToken ct)
        {
            if (request.Image is null || request.Image.Length == 0)
            {
                return BadRequest("Image is required.");
            }

            var appFile = BuildFileUpload(request);

            var command = new UpdateCategoryCommand(
                categoryId,
                request.Title,
                appFile,
                request.Priority
            );

            var result = await sender.Send(command, ct);

            return result.Match(response => Ok(response), Problem);
        }

        [HttpPut("status/{categoryId:guid}", Name = "UpdateCategoryStatus")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing category status")]
        [EndpointDescription("Updates a category status.")]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute] Guid categoryId,
            CancellationToken ct)
        {
            var result = await sender.Send(new UpdateCategoryStatusCommand(categoryId), ct);

            return result.Match(_ => NoContent(), Problem);
        }

        [HttpDelete("{categoryId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Removes a category.")]
        [EndpointDescription("Deletes the specified category from the system.")]
        [EndpointName("RemoveCategory")]
        public async Task<IActionResult> Delete(Guid categoryId, CancellationToken ct)
        {
            var result = await sender.Send(new RemoveCategoryCommand(categoryId), ct);

            return result.Match(_ => NoContent(), Problem);
        }

        private static FileUpload BuildFileUpload(CategoryRequest request) =>
            new FileUpload
            {
                FileName = request.Image!.FileName,
                ContentType = request.Image.ContentType,
                Length = request.Image.Length,
                Content = request.Image.OpenReadStream()
            };
    }
}
