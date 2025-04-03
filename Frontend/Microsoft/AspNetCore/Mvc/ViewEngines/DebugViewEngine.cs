using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;

public class DebugViewEngine : IViewEngine
{
    public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
    {
        Console.WriteLine($"Searching for view: {viewName}");
        return ViewEngineResult.NotFound(viewName, new[] { "Views/Home/", "Views/Shared/" });
    }
    public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        => FindView(null, viewPath, isMainPage);
}