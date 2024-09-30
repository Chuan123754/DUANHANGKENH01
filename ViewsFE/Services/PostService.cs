using ViewsFE.IServices;
using ViewsFE.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ViewsFE.Services
{
    public class PostService : IPostService
    {
        private readonly HttpClient _httpClient;

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Posts>> GetAll()
        {
            // Lấy danh sách các bài viết
            string requestURL = "https://localhost:7011/api/Posts/posts-get";
            var response = await _httpClient.GetStringAsync(requestURL);
            var posts = JsonConvert.DeserializeObject<List<Posts>>(response);

            // Lấy danh sách các Post_tags cho mỗi bài viết
            foreach (var post in posts)
            {
                string tagsRequestURL = $"https://localhost:7011/api/Posts/get-categories-by-post-id?postId={post.Id}";
                var tagsResponse = await _httpClient.GetStringAsync(tagsRequestURL);
                post.Categories = JsonConvert.DeserializeObject<List<Categories>>(tagsResponse);
            }

            // Lấy danh sách các Post_tags cho mỗi bài viết
            foreach (var post in posts)
            {
                string tagsRequestURL = $"https://localhost:7011/api/Posts/get-tags-by-post-id?postId={post.Id}";
                var tagsResponse = await _httpClient.GetStringAsync(tagsRequestURL);
                post.Tags = JsonConvert.DeserializeObject<List<Tags>>(tagsResponse);
            }

            return posts;
        }

        public async Task<List<Posts>> SearchPosts(string keyword)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7011/api/Posts/search?keyword={Uri.EscapeDataString(keyword)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Posts>>();
            }
            return new List<Posts>();
        }

        public async Task<Posts> GetById(long postId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7011/api/Posts/posts-get-id?id={postId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Posts>();
            }
            return null;
        }

        public async Task<List<Categories>> GetCategoriesByPostId(long postId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7011/api/Posts/get-categories-by-post-id?postId={postId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Categories>>();
            }
            return new List<Categories>();
        }

        public async Task<List<Tags>> GetTagsByPostId(long postId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7011/api/Posts/get-tags-by-post-id?postId={postId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Tags>>();
            }
            return new List<Tags>();
        }

        public async Task Update(Posts post)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7011/api/Posts/{post.Id}", post);
            response.EnsureSuccessStatusCode();
        }

        public async Task<long> UploadFile(IFormFile file)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
                string requestURL = "https://localhost:7011/api/Files/upload";
                var response = await _httpClient.PostAsync(requestURL, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var uploadedFile = JsonConvert.DeserializeObject<Files>(responseData);
                    return uploadedFile.Id;
                }
                else
                {
                    throw new HttpRequestException("Upload thất bại.");
                }
            }
        }




        public async Task Create(Posts post)
        {
            //// Tải ảnh lên nếu có file và nhận ID của ảnh sau khi upload
            //long? imageId = null;
            //if (PostImage != null && PostImage.Length > 0)
            //{
            //    var uploadResult = await UploadFile(PostImage);
            //    imageId = uploadResult;
            //}

            post.Id = 0;
            post.AuthorId = 0;
            post.Post_date = DateTime.Now;
            post.Created_by = 0;
            post.Updated_by = 0;
            post.Created_at = DateTime.Now;
            post.Updated_at = DateTime.Now;
            post.Deleted_at = null;

            // Tạo bài viết
            string postRequestURL = "https://localhost:7011/api/Posts/posts-post";
            var postJsonContent = JsonConvert.SerializeObject(post);
            var postContent = new StringContent(postJsonContent, Encoding.UTF8, "application/json");
            var postResponse = await _httpClient.PostAsync(postRequestURL, postContent);

            if (!postResponse.IsSuccessStatusCode)
            {
                var errorContent = await postResponse.Content.ReadAsStringAsync();
                throw new Exception($"API call failed with status code {postResponse.StatusCode} and message: {errorContent}");
            }

            var createdPostJson = await postResponse.Content.ReadAsStringAsync();
            var createdPost = JsonConvert.DeserializeObject<Posts>(createdPostJson);


            //// Thêm dữ liệu cho Post_metas nếu có ID ảnh
            //if (imageId.HasValue)
            //{
            //    var postMeta = new Post_metas
            //    {
            //        Id = 0,
            //        Post_Id = createdPost.Id,
            //        Meta_key = imageId.Value.ToString(),
            //        Meta_value = "Image"
            //    };

            //    string postMetaRequestURL = "https://localhost:7011/api/PostMetas/postmetas-post";
            //    var postMetaJsonContent = JsonConvert.SerializeObject(postMeta);
            //    var postMetaContent = new StringContent(postMetaJsonContent, Encoding.UTF8, "application/json");

            //    var postMetaResponse = await _httpClient.PostAsync(postMetaRequestURL, postMetaContent);
            //    if (!postMetaResponse.IsSuccessStatusCode)
            //    {
            //        var errorContent = await postMetaResponse.Content.ReadAsStringAsync();
            //        throw new Exception($"API call failed with status code {postMetaResponse.StatusCode} and message: {errorContent}");
            //    }
            //}

            // Thêm dữ liệu cho Post_categories nếu SelectedCategoryIds có giá trị
            if (post.SelectedCategoryId != null && post.SelectedCategoryId.Any())
            {
                foreach (var categoryId in post.SelectedCategoryId)
                {
                    var postCategory = new Post_categories
                    {
                        Id = 0,
                        Post_Id = createdPost.Id,
                        Category_Id = categoryId
                    };

                    string postCategoryRequestURL = "https://localhost:7011/api/PostCategories/postcategories-post";
                    var postCategoryJsonContent = JsonConvert.SerializeObject(postCategory);
                    var postCategoryContent = new StringContent(postCategoryJsonContent, Encoding.UTF8, "application/json");

                    var postCategoryResponse = await _httpClient.PostAsync(postCategoryRequestURL, postCategoryContent);
                    if (!postCategoryResponse.IsSuccessStatusCode)
                    {
                        var errorContent = await postCategoryResponse.Content.ReadAsStringAsync();
                        throw new Exception($"API call failed with status code {postCategoryResponse.StatusCode} and message: {errorContent}");
                    }
                }
            }

            // Thêm dữ liệu cho Post_tags nếu SelectedTagIds có giá trị
            if (post.SelectedTagId != null && post.SelectedTagId.Any())
            {
                foreach (var tagId in post.SelectedTagId)
                {
                    var postTag = new Post_tags
                    {
                        Id = 0,
                        Post_Id = createdPost.Id,
                        Tag_Id = tagId
                    };

                    string postTagRequestURL = "https://localhost:7011/api/PostTags/posttags-post";
                    var postTagJsonContent = JsonConvert.SerializeObject(postTag);
                    var postTagContent = new StringContent(postTagJsonContent, Encoding.UTF8, "application/json");

                    var postTagResponse = await _httpClient.PostAsync(postTagRequestURL, postTagContent);
                    if (!postTagResponse.IsSuccessStatusCode)
                    {
                        var errorContent = await postTagResponse.Content.ReadAsStringAsync();
                        throw new Exception($"API call failed with status code {postTagResponse.StatusCode} and message: {errorContent}");
                    }
                }
            }


        }


        


        public async Task Delete(long id)
        {
            string requestURL = $"https://localhost:7011/api/Posts/posts-delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }
    }
}