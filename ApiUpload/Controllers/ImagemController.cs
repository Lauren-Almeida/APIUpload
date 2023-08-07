using Microsoft.AspNetCore.Mvc;

namespace ApiUpload.Controllers;

[Route("[controller]")]
[ApiController]

public class ImagemController : ControllerBase
{
    public static IWebHostEnvironment _environment;
    public ImagemController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpGet]
    public string Get()
    {
        string texto = "Web Api - UploadController em execução:" + DateTime.Now.ToLongTimeString();
        texto += $"\n Ambiente: {_environment.EnvironmentName}";
        return texto;
    }

    [HttpPost("upload")]
    public async Task<string> EnviaImagem([FromForm] IFormFile imagem)
    {
        if (imagem.Length > 0)
        {
            try
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\imagens\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\imagens\\");
                }
                var noimg = imagem.FileName;
                var file = Path.Combine($"{_environment.WebRootPath}/imagens/{noimg}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await imagem.CopyToAsync(fileStream);
                    // fileStream.Flush();
                    return "Salvou";
                }

            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }
        }
        else
        {
            return "Erro ao salvar imagem.";
        }
    }
}