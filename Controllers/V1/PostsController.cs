using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MyFirstWebApplication1.Contracts;
using MyFirstWebApplication1.Domain;
using MyFirstWebApplication1.Controllers.v1.Requests;
using MyFirstWebApplication1.Controllers.v1.Responses;
using MyFirstWebApplication1.Services;
using MyFirstWebApplication1.Extensions;

namespace MyFirstWebApplication1.Controllers.V1
{
    /** Roles = "Admin, Poster" - requires Admin or Poster role from User
     * [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Poster")]
     */

    /** Roles - requires both roles assigned to User
     * Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
     * Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Poster")]
     */
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetALL()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(_mapper.Map<List<PostResponse>>(posts));
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)
        {
            var post = _postService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PostResponse>(post));
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody]CreatePostRequest postRequest)
        {
            var post = new Post {Name = postRequest.Name};

            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            return Created(locationUri, _mapper.Map<PostResponse>(post));
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody]UpdatePostRequest updatePostRequest)
        {
            /** we need to make that this User has actually owns this post */
        var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var post = await _postService.GetPostByIdAsync(postId);
            post.Name = updatePostRequest.Name;

            var updatedPost = await _postService.UpdatePostAsync(post);

            if (updatedPost)
            {
                return Ok(_mapper.Map<PostResponse>(post));
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }

            var deletedPost = await _postService.DeletePostAsync(postId);

            if (deletedPost)
            {
                return NoContent();
            }

            return NotFound();

        }
    }
}
