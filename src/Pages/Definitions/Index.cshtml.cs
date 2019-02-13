using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tomware.Docson.Services;

namespace tomware.Docson.Pages
{
  public class DefinitionsModel : PageModel
  {
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly IMessageDefinitionService _service;

    public IEnumerable<MessageDefinition> Defintions { get; set; }

    public IEnumerable<string> Versions
    {
      get
      {
        return this.Defintions
          .Select(d => d.Version.ToString())
          .Distinct();
      }
    }

    public IEnumerable<string> Producers
    {
      get
      {
        return this.Defintions
          .Select(d => d.Producer)
          .Distinct();
      }
    }

    public IEnumerable<string> Tags
    {
      get
      {
        var tags = new List<string>();
        foreach (var d in this.Defintions)
        {
          tags.AddRange(d.Tags);
        }

        return tags.Distinct();
      }
    }

    public DefinitionsModel(
      IHostingEnvironment hostingEnvironment,
      IMessageDefinitionService service
    )
    {
      _hostingEnvironment = hostingEnvironment;
      _service = service;
    }

    public async Task OnGetAsync()
    {
      var wwwRoot = _hostingEnvironment.WebRootPath;
      var path = Path.Combine(wwwRoot, "types");

      this.Defintions = await _service.GetTypes(path);
    }
  }
}