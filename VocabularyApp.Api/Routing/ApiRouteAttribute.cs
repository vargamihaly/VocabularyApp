using Microsoft.AspNetCore.Mvc;

namespace VocabularyApp.Api.Routing;

public sealed class ApiRouteAttribute : RouteAttribute
{
    public ApiRouteAttribute() : base($"api/[controller]")
    {
    }
    public ApiRouteAttribute(string controllerName) : base($"api/{controllerName}")
    {
        ControllerName = controllerName;
    }

    public string? ControllerName { get; }
}
