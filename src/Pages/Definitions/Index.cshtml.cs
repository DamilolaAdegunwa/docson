using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using tomware.Docson.Services;

namespace tomware.Docson.Pages
{
  public class DefinitionsModel : PageModel
  {
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly IMessageDefinitionService _service;

    public IEnumerable<MessageDefinition> Defintions { get; set; }

    public SelectList Names
    {
      get
      {
        var items = this.Defintions
          .Select(d => d.Name)
          .Distinct();

        return new SelectList(items);
      }
    }

    public SelectList Versions
    {
      get
      {
        var items = this.Defintions
          .Select(d => d.Version.ToString())
          .Distinct();

        return new SelectList(items);
      }
    }

    public SelectList Producers
    {
      get
      {
        var items = this.Defintions
          .Select(d => d.Producer)
          .Distinct();

        return new SelectList(items);
      }
    }

    public SelectList Tags
    {
      get
      {
        var tags = new List<string>();
        foreach (var d in this.Defintions)
        {
          tags.AddRange(d.Tags);
        }

        var items = tags.Distinct();

        return new SelectList(items);
      }
    }

    [BindProperty]
    public string Name { get; set; }
    [BindProperty]
    public string Version { get; set; }
    [BindProperty]
    public string Producer { get; set; }
    [BindProperty]
    public string Tag { get; set; }

    public DefinitionsModel(
      IHostingEnvironment hostingEnvironment,
      IMessageDefinitionService service
    )
    {
      this._hostingEnvironment = hostingEnvironment;
      this._service = service;
    }

    public async Task OnGetAsync()
    {
      this.Defintions = await this._service.GetTypes(this.GetPath());
    }

    public async Task OnPostAsync()
    {
      var definitions = await this._service.GetTypes(this.GetPath());

      if (!string.IsNullOrWhiteSpace(this.Name))
      {
        definitions = definitions
          .Where(d => d.Name == this.Name);
      }

      if (!string.IsNullOrWhiteSpace(this.Version))
      {
        definitions = definitions
          .Where(d => d.Version.ToString() == this.Version);
      }

      if (!string.IsNullOrWhiteSpace(this.Producer))
      {
        definitions = definitions
           .Where(d => d.Producer == this.Producer);
      }

      if (!string.IsNullOrWhiteSpace(this.Tag))
      {
        definitions = definitions
          .Where(d => d.Tags.Contains(this.Tag));
      }

      this.Defintions = definitions;
    }

    private string GetPath()
    {
      return Path.Combine(this._hostingEnvironment.WebRootPath, "data/types");
    }
  }
}