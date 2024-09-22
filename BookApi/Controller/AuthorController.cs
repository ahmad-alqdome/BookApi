
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController :ControllerBase
    {

        private readonly IAuthorService _authorService;
        private List<string> allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;   //1 MB

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsynac()
        {
            var authors = await _authorService.GetAllAuthorsAsync(); // Corrected method name
            return Ok(authors); // Return the list of authors instead of an empty string
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsynac(int id)
        {
            var authors = await _authorService.GetAuthorAsync(id);
            return authors == null ? NotFound() : Ok(authors);
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] AuthorAddDto authorAddDto)
        {

            if (authorAddDto.AuthorPhoto == null)
            {
                return BadRequest("The Photo is Null");
            }
            if (!allowedExtenstions.Contains(Path.GetExtension(authorAddDto.AuthorPhoto.FileName).ToLower()))
            {
                return BadRequest("The invalid file");
            }

            if (_maxAllowedPosterSize < authorAddDto.AuthorPhoto.Length)
            {
                return BadRequest("The photo size is higher");
            }

            using var dataStream = new MemoryStream();
            await authorAddDto.AuthorPhoto.CopyToAsync(dataStream);

            var author = new Author()
            {
                AuthorName = authorAddDto.AuthorName
                ,
                AuthorPhoto = dataStream.ToArray()
            };


            await _authorService.AddAuthorAsnc(author);

            return Ok(author);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] AuthorAddDto authorAddDto)
        {
            var author = await _authorService.GetAuthorAsync(id);

            var newAuthor = new Author();


            if (author == null)

                return NotFound($"No author was found with ID {id}");



            // اذا تم تغيير الصورة سيدخل تحديث الصورة 

            if (authorAddDto.AuthorPhoto != null)
            {
                if (!allowedExtenstions.Contains(Path.GetExtension(authorAddDto.AuthorPhoto.FileName).ToLower()))
                {
                    return BadRequest("The invalid file");
                }

                if (_maxAllowedPosterSize < authorAddDto.AuthorPhoto.Length)
                {
                    return BadRequest("The photo size is higher");
                }
                using var dataStream = new MemoryStream();
                await authorAddDto.AuthorPhoto.CopyToAsync(dataStream);
                newAuthor.AuthorPhoto = dataStream.ToArray();

            }

            newAuthor.AuthorName = authorAddDto.AuthorName;

            _authorService.Update(author);


            return Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delet(int id)
        {
            var author = await _authorService.GetAuthorAsync(id);

            _authorService.Delete(author);
            return Ok(author);
        }


    }
}
