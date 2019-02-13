using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tomware.Docson.Services;

namespace tomware.Docson.Pages
{
  public class DefinitionsModel : PageModel
  {
    private readonly IMessageDefinitionService _service;

    public IEnumerable<MessageDefinition> Defintions { get; set; }

    public DefinitionsModel(
      IMessageDefinitionService service
    )
    {
      _service = service;
    }

    public async Task OnGetAsync()
    {
      this.Defintions = await _service.GetTypes("./wwwroot/types/");
    }
  }
}