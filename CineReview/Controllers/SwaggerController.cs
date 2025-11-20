namespace CineReview.Controllers;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller para facilitar acesso à documentação Swagger.
/// </summary>
[Route("swagger")]
public class SwaggerController : Controller
{
    /// <summary>
    /// Redireciona para a interface do Swagger UI em /docs.
    /// </summary>
    [HttpGet]
    public IActionResult Index() => Redirect("/docs");
}