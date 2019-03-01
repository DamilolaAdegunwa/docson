using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tomware.Docson.Services;

namespace tomware.Docson.Pages
{
  public class DefinitionsDetailModel : PageModel
  {
    private readonly IMessageDefinitionService _service;

    public MessageDefinition Defintion { get; set; }

    public DefinitionsDetailModel(
      IMessageDefinitionService service
    )
    {
      _service = service;
    }

    public void OnGet(string id)
    {
      this.Defintion = _service.GetByKey(id);
    }
  }
}