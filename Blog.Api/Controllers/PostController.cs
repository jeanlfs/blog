using AutoMapper;
using Blog.Api.Responses;
using Blog.Domain.DAOs;
using Blog.Domain.DTOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : CustomControllerBase
    {
        private readonly IPostService _postService;
        private readonly IVisualizationService _visualizationService;
        private readonly ILikeService _likeService;
        private readonly IMapper _mapper;

        public PostController(
            IPostService postService,
            IVisualizationService visualizationService,
            ILikeService likeService,
            IMapper mapper)
        {
            _postService = postService;
            _visualizationService = visualizationService;
            _likeService = likeService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            IEnumerable<PostDao> posts = new List<PostDao>();

            if (IsAdmin)
                posts = await _postService.GetPosts();
            else if (IsWriter)
                posts = await _postService.GetPosts(CurrentUserId);
            else if (IsReader)
                return Forbid();

            var response = new ApiResponse<IEnumerable<PostDao>>(posts);
            return Ok(response);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetPosts();
            var response = new ApiResponse<IEnumerable<PostDao>>(posts);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            var post = await _postService.GetPost(id);
            var response = new ApiResponse<PostDao>(post);
            return Ok(response);
        }

        [HttpGet("{id}/Comment")]
        public async Task<IActionResult> GetComments([FromRoute] int id)
        {
            var comments = await _postService.GetComments(id);
            var response = new ApiResponse<IEnumerable<CommentDao>>(comments);
            return Ok(response);
        }

        [HttpGet("{postId}/Visualization")]
        public async Task<IActionResult> GetVisualization([FromRoute] int postId, [FromQuery] int userId, [FromQuery] string sessionId)
        {
            var visualization = new VisualizationDao();

            if (userId > 0)
                visualization = await _visualizationService.GetVisualization(postId, userId);
            else if (!string.IsNullOrEmpty(sessionId))
                visualization = await _visualizationService.GetVisualization(postId, sessionId);

            var response = new ApiResponse<VisualizationDao>(visualization);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{postId}/Like")]
        public async Task<IActionResult> GetLike([FromRoute] int postId)
        {
            var like = await _likeService.GetLike(postId, CurrentUserId);
            var response = new ApiResponse<LikeDao>(like);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertPost([FromBody] PostDto postDto)
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            var post = _mapper.Map<Post>(postDto);
            await _postService.InsertPost(post);
            var response = new ApiResponse<Post>(post);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromBody] Post post)
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            var result = await _postService.UpdatePost(post, IsAdmin, CurrentUserId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            var result = await _postService.DeletePost(id, IsAdmin, CurrentUserId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
